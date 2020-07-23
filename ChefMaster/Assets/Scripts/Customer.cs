using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Random = UnityEngine.Random;

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

    public Image waitTimeIndicator;
    public float customerWaitTime;
    public float currentWaitTime;
    public float bufferTime;

    public Transform[] orderIndicator;
    public List<Vegetable> currentOrder;

    public float speedMultiplier;
    public float timeValue;
    GameManager gameManager;

    public bool isCustomerActive;

    public GameObject orderDisplay;

    public bool OnItemDrop(PlayerController playerScript)
    {
        if (playerScript.gotChoppedItem)
        {
            playerScript.gotChoppedItem = false;
            Destroy(playerScript.bowl);
            if (currentList.vegetables.Length == playerScript.combinationList.Count)
            {
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

            if (playerScript.correctItem)
            {
                ResetCustomer();
                playerScript.score+=1;


                if (timeValue >= 0.7f)
                {
                    var randomPickup = Random.Range(0, gameManager.pickups.Length);
                    var pickupPos = new Vector3(Random.Range(-5f,5f),0f, Random.Range(-3f, 3f));
                    var pickup = (Pickup)Instantiate(gameManager.pickups[randomPickup]);
                    pickup.transform.position = pickupPos;
                    pickup.playerTag = playerScript.playerContorl.ToString();

                    Debug.Log("playergets pickup: " + pickup.name);
                }
            }
            else
            {
                //player penalty
                Debug.Log("Customer is angry");
            }
        }
        playerScript.combinationList = new List<InventoryEnum>();
        return true;
    }

    public bool OnItemPickup(PlayerController playerScript)
    {
        return false;
    }

    private void Awake()
    {
        gameManager = GameManager.Instance;
        currentOrder = new List<Vegetable>();
    }


    void Update()
    {
        if (isCustomerActive)
        {
            if (currentWaitTime <= 0 )
            {
                ResetCustomer();

            }
            else
            {
                currentWaitTime -= Time.deltaTime* speedMultiplier;
              timeValue = currentWaitTime / customerWaitTime;
                waitTimeIndicator.fillAmount =  currentWaitTime / customerWaitTime ;
            }
        }
    }

    private void OnEnable()
    {
        GetSaladList();
    }

    void GetSaladList()
    {
        orderDisplay.SetActive(true);

        waitTimeIndicator.fillAmount = 1f;
        var value = Random.Range(0, gameManager.saladList.Length);
        currentList = gameManager.saladList[value];

        customerWaitTime = 0f;
        for(int i = 0; i< currentList.vegetables.Length; i++)
        {
           var currentObject= Instantiate( gameManager.vegetablesPrefab[(int)currentList.vegetables[i]],orderIndicator[i]);
            currentObject.transform.localPosition = Vector3.zero;
            currentOrder.Add(currentObject);

            customerWaitTime += currentObject.choppingTime;
        }
        customerWaitTime = (customerWaitTime*2)+ bufferTime;
        currentWaitTime = customerWaitTime;
        isCustomerActive = true;
    }

    void ResetCustomer()
    {

        isCustomerActive = false;
        for (int i = 0; i < currentOrder.Count; i++)
        {

            Destroy(currentOrder[i].gameObject);
        }
        currentOrder = new List<Vegetable>();
        waitTimeIndicator.fillAmount = 0f;
        orderDisplay.SetActive(false);
        StartCoroutine(SpawnNewCustomer());
    }


    IEnumerator SpawnNewCustomer()
    {
        yield return new WaitForSeconds(3f);
        GetSaladList();

    }

}
