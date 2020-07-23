using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

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
        var time = Mathf.RoundToInt(gameManager.player_P1.playerTime);
        time_P1.text = time.ToString();
        time = Mathf.RoundToInt(gameManager.player_P2.playerTime);
        time_P2.text = time.ToString();

    }
    private void OnEnable()
    {
        PlayerController.IncreaseScore += IncreaseScore;
    }

    private void OnDisable()
    {
        PlayerController.IncreaseScore -= IncreaseScore;
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
