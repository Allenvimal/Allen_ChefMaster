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
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<UIManager>();
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

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        score_P1.text = gameManager.player_P1.score.ToString();
        score_P2.text = gameManager.player_P2.score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        time_P1.text = gameManager.player_P1.playerTime.ToString();
       
    }
    private void OnEnable()
    {
        Customer.IncreaseScore += IncreaseScore;
    }

    private void OnDisable()
    {
        Customer.IncreaseScore -= IncreaseScore;
    }

    void IncreaseScore(Player player)
    {
        if (player == Player.P1)
        {
            score_P1.text = gameManager.player_P1.score.ToString();
        }
        else if (player == Player.P2)
        {
            score_P2.text = gameManager.player_P2.score.ToString();
        }
    }


}
