using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] private SpriteLibraryAsset regularSprite;
    [SerializeField] private SpriteLibraryAsset springSprite;
    [SerializeField] private SpriteLibraryAsset summerSprite;
    [SerializeField] private SpriteLibraryAsset autumnSprite;
    [SerializeField] private SpriteLibraryAsset winterSprite;

    [SerializeField] private SpriteLibraryAsset swordSprite;
    [SerializeField] private SpriteLibraryAsset axeSprite;

    private SpriteLibrary seasonalSprite;
    private SpriteLibrary weaponSprite;

    private void Awake()
    {
        seasonalSprite = transform.Find("Sprite").Find("Seasonal").GetComponent<SpriteLibrary>();
        weaponSprite = transform.Find("Sprite").Find("Weapon").GetComponent<SpriteLibrary>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Sword();
    }

    private void ChangeSeasonalSprite(SpriteLibraryAsset sprite)
    {
        seasonalSprite.spriteLibraryAsset = sprite;
    }

    private void ChangeWeaponSprite(SpriteLibraryAsset sprite)
    {
        weaponSprite.spriteLibraryAsset = sprite;
    }

    public void Regular()
    {
        ChangeSeasonalSprite(regularSprite);
    }

    public void Spring()
    {
        ChangeSeasonalSprite(springSprite);
    }

    public void Summer()
    {
        ChangeSeasonalSprite(summerSprite);
    }

    public void Autumn()
    {
        ChangeSeasonalSprite(autumnSprite);
    }

    public void Winter()
    {
        ChangeSeasonalSprite(winterSprite);
    }

    public void Sword()
    {
        ChangeWeaponSprite(swordSprite);
    }

    public void Axe()
    {
        ChangeWeaponSprite(axeSprite);
    }
}
