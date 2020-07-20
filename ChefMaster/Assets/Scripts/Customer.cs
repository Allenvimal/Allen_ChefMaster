using System.Collections;
using System.Collections.Generic;

using System;
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
    public static event Action<Player> IncreaseScore;

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

            if (playerScript.correctItem)
            {
                ResetCustomer();
                playerScript.score+=5;

                if (IncreaseScore != null)
                    IncreaseScore(playerScript.playerContorl);


                //reward Player
                Debug.Log("Reward Player : " +playerScript.score );

                if (timeValue >= 0.7f)
                {
                    Debug.Log("playergets pickup");
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

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
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

    private void OnDisable()
    {
       
    }
    void GetSaladList()
    {
        waitTimeIndicator.fillAmount = 1f;
        var value = UnityEngine.Random.Range(0, gameManager.saladList.Length);
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
        StartCoroutine(SpawnNewCustomer());
    }


    IEnumerator SpawnNewCustomer()
    {
        yield return new WaitForSeconds(3f);
        GetSaladList();

    }

}
