using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text player1WinCount;
    public Text player2WinCount;

    [HideInInspector]
    public bool player1Scored;
    [HideInInspector]
    public bool player2Scored;

    [HideInInspector]
    public int p1Score = 0;
    [HideInInspector]
    public int p2Score = 0;

    private void Update()
    {
        if (player1Scored)
        {
            p1Score++;
            player1WinCount.text = p1Score.ToString();
            player1Scored = false;
            FindObjectOfType<GameManager>().nextMatch = true;
        }
        if (player2Scored)
        {
            p2Score++;
            player2WinCount.text = p2Score.ToString();
            player2Scored = false;
            FindObjectOfType<GameManager>().nextMatch = true;
        }
    }
}
