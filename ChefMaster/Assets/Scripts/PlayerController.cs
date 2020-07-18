using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    GameObject player;

    public float moveSpeed;

    public string horizontalAxis = "Horizontal_P1";
    public string verticalAxis = "Vertical_P1";

    public string pickupItem = "Pickup_P1";
    public string dropItem = "Drop_P1";

    public List<GameObject> vegetables;
    GameManager gameManager;


    public Transform[] placeHolders;


    public bool playerControl;

    public bool removeflag;

    public bool doAction;
    public Collider collidedObject;

    public List<InventoryEnum> combinationList;
    public SaladClass saladClass;

    public bool correctItem;
    public IEnumerable<InventoryEnum> eaqualItems;
    public List<InventoryEnum> dummyList;
    public int pointer;


    public Transform chopPos;

    public int score;

    public bool gotChoppedItem;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        playerControl = true;
        pointer = 0;
        player = this.gameObject;
        vegetables = new List<GameObject>();
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        //Player movement Control

        if (playerControl)
        {
            var horizontal = Input.GetAxis(horizontalAxis);
            var vertical = Input.GetAxis(verticalAxis);

            if (Input.GetButton(horizontalAxis) || Input.GetButton(verticalAxis))
            {
                float myAngle = Mathf.Atan2(Input.GetAxis(horizontalAxis), Input.GetAxis(verticalAxis)) * Mathf.Rad2Deg;

                player.transform.rotation = Quaternion.Euler(0f, myAngle, 0f);
                player.transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
            }
        }

        //Player interactions 

        if (playerControl && doAction)
        {
            if (Input.GetButtonDown(pickupItem))
            {
                if (collidedObject.tag == "Vegetable")
                {
                    Debug.Log("item Picked");

                    if (pointer < 2)
                    {
                        var currentVegetabe = collidedObject.GetComponent<VegetableBasket>().vegetable;
                        var itemPicked = Instantiate(gameManager.vegetables[(int)currentVegetabe], placeHolders[pointer]);

                        vegetables.Add(itemPicked);
                        RearrangeOrder();
                        pointer++;
                    }
                }
                else if (collidedObject.tag == "ChoppingBoard")
                {
                    gotChoppedItem = true;
                    Debug.Log("item Picked from Chopping Board");
                }
            }
            if (Input.GetButtonDown(dropItem))
            {
                if (collidedObject.tag == "ChoppingBoard")
                {
                    Debug.Log("item droped");

                    var waittime = vegetables[0].GetComponent<Vegetable>().choppingTime;

                    playerControl = false;
                    StartCoroutine(StartChopping(waittime));
                    
                    pointer--;

                }
                if (collidedObject.tag == "Customer" && gotChoppedItem)
                {
                    gotChoppedItem = false;
                    saladClass = collidedObject.GetComponent<Customer>().currentList;
                    if (saladClass.vegetables.Length == combinationList.Count)
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



                        dummyList = new List<InventoryEnum>(combinationList);

                        for (int i = 0; i < saladClass.vegetables.Length; i++)

                        {
                            for (int j = 0; j < dummyList.Count; j++)
                            {
                                if (saladClass.vegetables[i] == dummyList[j])
                                {
                                    correctItem = true;
                                    dummyList.Remove(dummyList[j]);
                                    Debug.Log("Correct loop" + "saladClass " + i + " : combination " + j);
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        correctItem = false;
                        Debug.Log("Incorrect item list count");

                    }

                    if (correctItem && dummyList.Count == 0)
                    {
                       
                        score++;
                        Debug.Log("Current Score: " + score);
                    }
                    combinationList = new List<InventoryEnum>();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        collidedObject = other;
        doAction = true;
     
    }

    private void OnTriggerExit(Collider other)
    {
        collidedObject = null;
        doAction = false;
    }

    IEnumerator StartChopping(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        var item = vegetables[0];
        item.transform.parent = chopPos;
        item.transform.localPosition = Vector3.zero;
        combinationList.Add(item.GetComponent<Vegetable>().vegetableType);
        item.GetComponent<Vegetable>().model.SetActive(false);
        item.GetComponent<Vegetable>().ChoppedModel.SetActive(true);
        vegetables.Remove(item);

        playerControl = true;
    }

    void RearrangeOrder()
    {
        if (pointer > 0)
        {
            for (int i = 0; i < vegetables.Count; i++)
            {
                var nextPos = vegetables[i];
                nextPos.transform.parent = placeHolders[i+1];
                nextPos.transform.localPosition = Vector3.zero;
            }
        }
    }




}
