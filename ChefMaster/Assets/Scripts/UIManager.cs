using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 
/// UIManager holds the OnScreen UI functions and datas 
/// 
/// </summary>

public class UIManager : MonoBehaviour
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


    [Header("Player_P1")]
    public TextMeshProUGUI time_P1;
    public TextMeshProUGUI score_P1;

    [Header("Player_P2")]
    public TextMeshProUGUI time_P2;
    public TextMeshProUGUI score_P2;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
