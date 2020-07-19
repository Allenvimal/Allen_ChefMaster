using UnityEngine;

/// <summary>
/// 
/// One vegetable can be placed on the plate 
/// 
/// </summary>

public class Plate : MonoBehaviour,InterfaceFunctions
{
    public Transform placeHolder;
    public GameObject vegetablePlaced;

    private void Start()
    {
        if (placeHolder == null)                        //Default setting: Make the current object transform as vegetable placement point
            placeHolder = this.gameObject.transform;        
    }

    public bool OnItemPickup(PlayerController playerScript)
    {
        if (playerScript.pointer < 2 && vegetablePlaced != null)
        {
            var itemPicked = vegetablePlaced;
            itemPicked.transform.parent = playerScript.placeHolders[playerScript.pointer];
            itemPicked.transform.localPosition = Vector3.zero;
            playerScript.vegetables.Add(itemPicked);
            playerScript.RearrangeOrder();
            playerScript.pointer++;
        }
        return true;
    }

    public bool OnItemDrop(PlayerController playerScript)
    {
        if (playerScript.vegetables.Count > 0)
        {
            if (vegetablePlaced == null)
            {
                vegetablePlaced = playerScript.vegetables[0];
                vegetablePlaced.transform.parent = placeHolder;
                vegetablePlaced.transform.localPosition = Vector3.zero;

                playerScript.vegetables.Remove(playerScript.vegetables[0]);
                playerScript.pointer--;

            }
            
        }
        
        return true;
    }

}
