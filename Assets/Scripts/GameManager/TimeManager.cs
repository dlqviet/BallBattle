using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    //public float countdownTime = 3;
    public Text matchTimer;
    //public Text countdownText;

    [HideInInspector]
    public float timePerMatch;

    private void Start()
    {
        timePerMatch = FindObjectOfType<GameManager>().timePerMatch;
    }

    private void FixedUpdate()
    {
        if (timePerMatch < 10)
        {
            matchTimer.color = Color.yellow;
            if (timePerMatch < 0)
            {
                FindObjectOfType<GameManager>().nextMatch = true;
                timePerMatch = FindObjectOfType<GameManager>().timePerMatch;
            }
        }

        timePerMatch -= 1 * Time.deltaTime;
        matchTimer.text = timePerMatch.ToString("0");
    }
}
