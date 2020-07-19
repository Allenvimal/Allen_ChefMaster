using UnityEngine;

/// <summary>
/// 
/// - Player can take displyed vegetable
/// - Or place the picked vegetable back into the basket (the vegetable type has to be the same)
/// 
/// </summary>
public class VegetableBasket : MonoBehaviour,InterfaceFunctions
{
    public GameObject vegetablePrefab;

    public bool OnItemDrop(PlayerController playerScript)
    {
        if (playerScript.vegetables.Count > 0)
        {
            var currentVegetable = playerScript.vegetables[0].GetComponent<Vegetable>();
            if (currentVegetable.vegetableType == vegetablePrefab.GetComponent<Vegetable>().vegetableType)
            {
                playerScript.vegetables.Remove(currentVegetable.gameObject);
                Destroy(currentVegetable.gameObject);
                playerScript.pointer--;
            }
        }
        return true;
    }

    public bool OnItemPickup(PlayerController playerScript)
    {
        if (playerScript.pointer < 2)
        {
            var itemPicked = Instantiate(vegetablePrefab, playerScript.placeHolders[playerScript.pointer]);
            itemPicked.transform.localPosition = Vector3.zero;
            playerScript.vegetables.Add(itemPicked);
            playerScript.RearrangeOrder();
            playerScript.pointer++;
        }
        return true;
    }
}
