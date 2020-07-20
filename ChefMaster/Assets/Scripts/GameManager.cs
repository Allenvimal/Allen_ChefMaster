using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// Controls the current Scene flow
/// 
/// </summary>

[System.Serializable]
public class SaladClass
{
    public InventoryEnum[] vegetables;
}
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    public SaladClass[] saladList;

    public Sprite[] vegetables;
    public Vegetable[] vegetablesPrefab;

    public PlayerController player_P1;
    public PlayerController player_P2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
