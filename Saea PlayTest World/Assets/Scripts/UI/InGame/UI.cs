using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private Transform doubleJump;
    [SerializeField] private Transform airDash;
    [SerializeField] private Transform wallJump;
    [SerializeField] private Transform wallSlide;
    [SerializeField] private Transform controls;
    [SerializeField] private Transform axe;

    private void Awake()
    {
        DisableAll();
        controls.gameObject.SetActive(true);
    }

    public void EnableDoubleJump()
    {
        DisableAll();
        doubleJump.gameObject.SetActive(true);
    }

    public void EnableAirDash()
    {
        DisableAll();
        airDash.gameObject.SetActive(true);
    }

    public void EnableWallJump()
    {
        DisableAll();
        wallJump.gameObject.SetActive(true);
    }

    public void EnableWallSlide()
    {
        DisableAll();
        wallSlide.gameObject.SetActive(true);
    }

    public void EnableAxe()
    {
        DisableAll();
        axe.gameObject.SetActive(true);
    }

    private void DisableAll()
    {
        if (doubleJump.gameObject.activeInHierarchy == true)
        {
            doubleJump.gameObject.SetActive(false);
        }
        if (airDash.gameObject.activeInHierarchy == true)
        {
            airDash.gameObject.SetActive(false);
        }
        if (wallJump.gameObject.activeInHierarchy == true)
        {
            wallJump.gameObject.SetActive(false);
        }
        if (wallSlide.gameObject.activeInHierarchy == true)
        {
            wallSlide.gameObject.SetActive(false);
        }
        if (controls.gameObject.activeInHierarchy == true)
        {
            controls.gameObject.SetActive(false);
        }
    }
}
