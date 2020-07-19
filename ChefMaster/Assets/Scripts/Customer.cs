using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// 
/// This class has all the control action of a customer
/// - Validating the salad served by the player
///
/// Actions to be implemented
/// - Generats current customer order
/// - Rewards the player with pickups
/// - Controls the customer wait time 
/// 
/// </summary>

public class Customer : MonoBehaviour,InterfaceFunctions
{
    public SaladClass currentList;

    public List<InventoryEnum> dummyList;

    public bool OnItemDrop(PlayerController playerScript)
    {
        if (playerScript.gotChoppedItem)
        {

            playerScript.gotChoppedItem = false;

            Destroy(playerScript.bowl);

            if (currentList.vegetables.Length == playerScript.combinationList.Count)
            {
                /*   eaqualItems = saladClass.vegetables.Intersect(combinationList);
                  var repeatValue = saladClass.vegetables.Distinct();
                  if(eaqualItems.Count() == saladClass.vegetables.Length-repeatValue.Count())
                  {
                      correctItem = true;
                      Debug.Log("Correct item ");
                  }
                  else
                  {
                      correctItem = false;
                      Debug.Log("Incorrect item ");

                  }*/

                dummyList = new List<InventoryEnum>(playerScript.combinationList);

                for (int i = 0; i < currentList.vegetables.Length; i++)

                {
                    for (int j = 0; j < dummyList.Count; j++)
                    {
                        if (currentList.vegetables[i] == dummyList[j])
                        {
                            playerScript.correctItem = true;
                            dummyList.Remove(dummyList[j]);
                            break;
                        }
                        
                    }

                }
                
            }
            else
            {
                playerScript.correctItem = false;
            }
           
        }


        playerScript.combinationList = new List<InventoryEnum>();
        return true;
    }

    public bool OnItemPickup(PlayerController playerScript)
    {
        return false;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentList = GameManager.Instance.saladList[1];
    }




}
