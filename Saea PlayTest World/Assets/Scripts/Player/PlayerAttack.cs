using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform sword;
    [SerializeField] private Transform axe;

    private PlayerControls controls;

    private bool isAttacking = false;

    private Transform activeWeapon;
    private int activeWeaponIndex = 0;

    private Transform weapons;
    private Transform weaponRotation;

    private Vector3 weaponPos;

    private List<Transform> unlockedWeapons = new List<Transform>();

    [SerializeField] private float cooldown = 15.0f;
    private float activeCooldown;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        weapons = transform.Find("WeaponRotation").Find("Weapons");
        weaponRotation = transform.Find("WeaponRotation");

        UnlockSword();

        activeCooldown = 0f;

        NextWeapon();
    }

    void Update()
    {
        Rotate();

        if (activeCooldown > 0)
        {
            activeCooldown -= Time.deltaTime;
        }

        if (controls.Gameplay.SwapWeaponR.WasPressedThisFrame())
        {
            NextWeapon();
        }
        if (controls.Gameplay.SwapWeaponL.WasPressedThisFrame())
        {
            PreviousWeapon();
        }
    }

    private void EnableWeapon(Transform Enable)
    {
        activeWeapon = Enable;
        sword.gameObject.SetActive(false);
        axe.gameObject.SetActive(false);
        activeWeapon.gameObject.SetActive(true);
        BroadcastMessage(activeWeapon.name);
    }

    private void NextWeapon()
    {
        activeWeaponIndex++;
        if (activeWeaponIndex >= unlockedWeapons.Count)
        {
            activeWeaponIndex = 0;
        }
        EnableWeapon(unlockedWeapons[activeWeaponIndex]);
    }

    private void PreviousWeapon()
    {
        activeWeaponIndex--;
        if (activeWeaponIndex < 0)
        {
            activeWeaponIndex = unlockedWeapons.Count - 1;
        }
        EnableWeapon(unlockedWeapons[activeWeaponIndex]);
    }

    private void UnlockAxe()
    {
        unlockedWeapons.Add(axe);
    }

    /*private void Axe()
    {
        EnableWeapon(axe);
    }*/

    private void UnlockSword()
    {
        unlockedWeapons.Add(sword);
    }

    /*private void Sword()
    {
        EnableWeapon(sword);
    }*/

    public void Attack()
    {
        if (activeCooldown <= 0)
        {
            if (isAttacking == false)
            {
                weaponPos = activeWeapon.localPosition;
                weapons.gameObject.SetActive(true);
                isAttacking = true;
                transform.GetComponent<Animator>().SetBool("Attacking", true);
                activeCooldown = cooldown;
            }
        }
    }

    public void EndAttack()
    {
        isAttacking = false;
        weapons.gameObject.SetActive(false);
        activeWeapon.localPosition = weaponPos;
    }

    public void SetBool()
    {
        transform.GetComponent<Animator>().SetBool("Attacking", false);
    }

    public void Rotate()
    {
        if (isAttacking == true)
        {
            return;
        }
        float x = 0f;
        float y = 0f;
        float offsetX = 0.321f;
        float offsetY = 0f;
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            y = 1f;
            offsetX = 0f;
            offsetY = 0.24f;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            y = -1f;
            offsetX = 0f;
            offsetY = -0.24f;
        }
        if (Input.GetAxisRaw("Horizontal") != 0 && y != 0)
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                y = 0.5f;
                offsetX = 0.194f;
                offsetY = 0.3f;
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                y = -0.5f;
                offsetX = 0.194f;
                offsetY = -0.3f;
            }
        }
        weaponRotation.localPosition = new Vector3(offsetX, offsetY, 0);
        var target = Quaternion.Euler(new Vector3(x * 69.23f, 0f, y * 69.23f * transform.localScale.x));
        weaponRotation.rotation = target;
    }
}
