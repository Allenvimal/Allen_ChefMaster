using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : Pickup
{

    public override void ChangeValue()
    {
        playerController.extraSpeed = value;
        
    }
}
