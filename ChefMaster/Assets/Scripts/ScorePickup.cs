﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickup : Pickup
{

    public override void ChangeValue()
    {
        playerController.score += value;
    }
}
