using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePickup : Pickup
{

    public override void ChangeValue()
    {
        playerController.playerTime += value;
    }
}

