using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// 
/// Controls the Player
/// - Player movement controls can the chosed from [playerControl] (i.e., Player_1 or Player_2)
/// - Player_1 controls
///     - w => Up
///     - s => Down
///     - a => Right
///     - d => Left
///     - h => Pickup item
///     - j => Drop item
/// - Player_2 controls
///     - up_arrow => Up
///     - down_arrow => Down
///     - right_arrow => Right
///     - left_arrow => Left
///     - Num_4 => Pickup item
///     - Num_5 => Drop item
///     
/// </summary>
public class PlayerController : MonoBehaviour
{

    public static event Action<Player> IncreaseScore;

    GameObject player;

    public float moveSpeed;

    public Player playerContorl;

    public string horizontalAxis = "Horizontal_P1";
    public string verticalAxis = "Vertical_P1";

    public string pickupItem = "Pickup_P1";
    public string dropItem = "Drop_P1";

    public List<GameObject> vegetables;
    GameManager gameManager;


    public Transform[] placeHolders;


    public bool isPlayerInteractable;

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
    public int lastScore;

    public bool gotChoppedItem;

    public GameObject bowl;

    public float playerTime;
    public float playerStartTime;
    public bool isPlayerDead;
    public float extraSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        playerTime = playerStartTime;
        isPlayerDead = false;

        Initialise();
        score = 0;
        lastScore = 0;
        isPlayerInteractable = true;
        pointer = 0;
       
        vegetables = new List<GameObject>();
        gameManager = GameManager.Instance;
    }

    void Initialise()   //Innitialise current player control keys
    {
        horizontalAxis = "Horizontal_" + playerContorl;
        verticalAxis = "Vertical_" + playerContorl;

        pickupItem = "Pickup_" + playerContorl;
        dropItem = "Drop_" + playerContorl;

        player.layer = LayerMask.NameToLayer(playerContorl.ToString());

    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInteractable)
        {
            //Player movement Control
            #region Player movement Control


            var horizontal = Input.GetAxis(horizontalAxis);
            var vertical = Input.GetAxis(verticalAxis);

            if (Input.GetButton(horizontalAxis) || Input.GetButton(verticalAxis))
            {
                float myAngle = Mathf.Atan2(Input.GetAxis(horizontalAxis), Input.GetAxis(verticalAxis)) * Mathf.Rad2Deg;

                player.transform.rotation = Quaternion.Euler(0f, myAngle, 0f);
                player.transform.Translate(Vector3.forward * Time.deltaTime * (moveSpeed+extraSpeed));
            }
            #endregion

            //Player interactions 
            #region Player interactions  


            if (collidedObject != null && collidedObject.tag == "Interactable")
            {
                if (Input.GetButtonDown(pickupItem))
                {
                    var interfaceFunction = collidedObject.GetComponent<InterfaceFunctions>();
                    if (interfaceFunction != null)
                    {
                        if (!collidedObject.GetComponent<InterfaceFunctions>().OnItemPickup(this))
                            Debug.Log("Method not Implemented");
                    }
                }
                if (Input.GetButtonDown(dropItem))
                {
                    var interfaceFunction = collidedObject.GetComponent<InterfaceFunctions>();
                    if (interfaceFunction != null)
                    {
                        if (!collidedObject.GetComponent<InterfaceFunctions>().OnItemDrop(this))
                            Debug.Log("Method Not Implemented");
                    }
                }
            }
            #endregion

            PlayerTimer();
        }

        if(score != lastScore && IncreaseScore != null)
        {
            lastScore = score;
            IncreaseScore(playerContorl);
        }
    }

void PlayerTimer()
    {
        if(playerTime>=0)
        {
            playerTime -= Time.deltaTime;

        }
        else
        {
            isPlayerInteractable = false;
            isPlayerDead = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable")
            collidedObject = other;
        else if (other.tag == "Pickup")
        {
            //do action
        }

    }

    private void OnTriggerExit(Collider other)
    {
        collidedObject = null;
    }

    public void RearrangeOrder()
    {
        if (pointer > 0)
        {
            for (int i = 0; i < vegetables.Count; i++)
            {
                var nextPos = vegetables[i];
                nextPos.transform.parent = placeHolders[i + 1];
                nextPos.transform.localPosition = Vector3.zero;
            }
        }
    }






}
