using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHandler : MonoBehaviour
{
    private UI UI;

    private void Awake()
    {
        UI = GameObject.Find("InGame").GetComponent<UI>();
    }

    public void Pickup(int id)
    {
        switch(id)
        {
            case 0:
                BroadcastMessage("UnlockAirDash");
                UI.EnableAirDash();
                break;
            case 1:
                BroadcastMessage("UnlockDoubleJump");
                UI.EnableDoubleJump();
                break;
            case 2:
                BroadcastMessage("UnlockWallSlide");
                UI.EnableWallSlide();
                break;
            case 3:
                BroadcastMessage("UnlockWallJump");
                UI.EnableWallJump();
                break;
            case 4:
                BroadcastMessage("UnlockAxe");
                UI.EnableAxe();
                break;
            case 5:
                BroadcastMessage("UnlockSword");
                break;
            case 6:
                BroadcastMessage("Heal", 1);
                break;
        }
    }
}
