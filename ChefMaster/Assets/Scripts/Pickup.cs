using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Base class for PickupItems
/// 
/// </summary>

public abstract class Pickup : MonoBehaviour
{
    public int value;
    [HideInInspector]
    public string playerTag;
    public PlayerController playerController;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == playerTag)
        {
            playerController = other.GetComponent<PlayerController>();
            ChangeValue();
            Destroy(this.gameObject);
        }
    }

    public virtual void ChangeValue()
    {

        

    }


}
