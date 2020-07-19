using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Controls the actions when an item is place or picked from the chopping board
/// - Player places the vegetable on the chopping board
/// - Player cant do any action when the vegetable is being chopped
/// - Wait time depends on the vegetable
/// - Player can pick the chopped vegetables(Bowl) to give it to customer or throw it in trash
/// 
/// </summary>
public class ChoppingBoard : MonoBehaviour,InterfaceFunctions
{

    public Transform[] placeHolders;

    public Vegetable currentVegetable;

    public List<Vegetable> placedVegetables;

    public List<InventoryEnum> combinationList;

    public int pointer;

    public GameObject bowlPrefab;

    public bool OnItemPickup(PlayerController playerScript)         //Instantiate Bowl and populate the list of chopped vegetables
    {
        if (combinationList.Count>0 && playerScript.vegetables.Count==0)
        {
            playerScript.gotChoppedItem = true;
            playerScript.combinationList = new List<InventoryEnum>(combinationList);

            for (int i = placedVegetables.Count; i > 0; i--)
            {

                var go = placedVegetables[i - 1];
                Debug.Log(go);
                placedVegetables.Remove(go);
                Destroy(go.gameObject);
            }

            combinationList.Clear();

            playerScript.bowl = Instantiate(bowlPrefab, playerScript.placeHolders[0]);
            playerScript.bowl.transform.localPosition = Vector3.zero;
        }
        return true;
    }


    public bool OnItemDrop(PlayerController playerScript)           //Place the vegetable to be chopped
    {
        if (playerScript.vegetables.Count > 0)
        {
            currentVegetable = playerScript.vegetables[0].GetComponent<Vegetable>();
            placedVegetables.Add(currentVegetable);
            pointer++;

            playerScript.isPlayerInteractable = false;
            StartCoroutine(StartChopping(currentVegetable.choppingTime, playerScript));
            playerScript.vegetables.Remove(playerScript.vegetables[0]);
            playerScript.pointer--;
        }
        return true;
    }

    IEnumerator StartChopping(float waitTime, PlayerController playerScript)           //Wait while vegetable is being chopped
    {
        currentVegetable.transform.parent = placeHolders[0];
        currentVegetable.transform.localPosition = Vector3.zero;

        yield return new WaitForSeconds(waitTime);                                  // Wait time changes with respect to the vegetable

        currentVegetable.transform.parent = placeHolders[1];
        currentVegetable.transform.localPosition = Vector3.zero;
        combinationList.Add(currentVegetable.GetComponent<Vegetable>().vegetableType);
        currentVegetable.GetComponent<Vegetable>().model.SetActive(false);
        currentVegetable.GetComponent<Vegetable>().ChoppedModel.SetActive(true);

        playerScript.isPlayerInteractable = true;
    }

}
