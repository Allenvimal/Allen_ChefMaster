using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour,InterfaceFunctions
{
    public bool OnItemDrop(PlayerController playerScript)
    {
        if (playerScript.gotChoppedItem)
        {
            playerScript.gotChoppedItem = false;
            Destroy(playerScript.bowl);
        }
        return true;
    }

    public bool OnItemPickup(PlayerController playerScript)
    {
        return false;
    }


}
