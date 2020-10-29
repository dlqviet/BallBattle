using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //game UI:
    [Header("UI:")]
    public GameObject uiBeforeEachMatch;
    public GameObject uiWhenRedLead;
    public GameObject uiWhenBlueLead;
    public GameObject uiWhenDraw;
    public GameObject endGameRed;
    public GameObject endGameBlue;
    public GameObject endGameNone;

    //game settings:
    [Header("Game Settings:")]
    public float matchPerGame = 5;
    public float timePerMatch = 140;
    public int energyBar = 6;
    public float energyRegen = 0.5f;

    //attacker:
    [Header("Attacking Soldiers:")]
    public float atk_energyCost = 2;
    public float atk_spawnTime = 0.5f;
    public float atk_reactivateTime = 2.5f;
    public float atk_normalSpeed = 1.5f;
    public float atk_carryingSpeed = 0.75f;

    //defender:
    [Header("Defending Soldiers:")]
    public float def_energyCost = 3;
    public float def_spawnTime = 0.5f;
    public float def_reactivateTime = 4;
    public float def_normalSpeed = 1;
    public float def_returnSpeed = 2;
    public float def_detectionRange = 35;

    //ball:
    [Header("Ball:")]
    public float ballSpeed = 1.5f;

    //[HideInInspector]
    public bool nextMatch;
    [HideInInspector]
    public bool restartGame;
    //[HideInInspector]
    public int currentMatch = 1;

    private void Start()
    {
        //turn on before match UI 
        this.uiBeforeEachMatch.SetActive(true);

        StartCoroutine(PauseBeforeAMatch(2));
    }

    private void FixedUpdate()
    {
        if (currentMatch > matchPerGame)
        {
            if (FindObjectOfType<ScoreManager>().p1Score > FindObjectOfType<ScoreManager>().p2Score)
            {
                this.endGameRed.SetActive(true);
            }
            else if (FindObjectOfType<ScoreManager>().p1Score < FindObjectOfType<ScoreManager>().p2Score)
            {
                this.endGameBlue.SetActive(true);
            }
            else if (FindObjectOfType<ScoreManager>().p1Score == FindObjectOfType<ScoreManager>().p2Score)
            {
                this.endGameNone.SetActive(true);
            }

            StartCoroutine(PauseForEndGame(3));
        }

        if (nextMatch)
        {
            currentMatch++;

            //deactive all attackers and defenders on field
            while (FindObjectOfType<AttackSoldierBehavior>())
            {
                if (FindObjectOfType<AttackSoldierBehavior>().transform.childCount > 5)
                {
                    FindObjectOfType<AttackSoldierBehavior>().transform.GetChild(5).transform.parent = null;
                }
                FindObjectOfType<AttackSoldierBehavior>().gameObject.SetActive(false);
            }

            while (GameObject.FindGameObjectWithTag("DefenderContainer"))
            {
                GameObject.FindGameObjectWithTag("DefenderContainer").SetActive(false);
            }

            //swap field
            FindObjectOfType<FieldManager>().ball.SetActive(false);
            FindObjectOfType<FieldManager>().UpdateAfterMatchEnd();

            //reset energy
            FindObjectOfType<EnergyManager>().p1Energy = 0;
            FindObjectOfType<EnergyManager>().p2Energy = 0;

            //reset time
            FindObjectOfType<TimeManager>().timePerMatch = this.timePerMatch;

            nextMatch = false;

            //turn on some UIs
            if (currentMatch < matchPerGame + 1)
            {
                if (FindObjectOfType<ScoreManager>().p1Score > FindObjectOfType<ScoreManager>().p2Score)
                {
                    this.uiWhenRedLead.SetActive(true);
                }
                else if (FindObjectOfType<ScoreManager>().p1Score < FindObjectOfType<ScoreManager>().p2Score)
                {
                    this.uiWhenBlueLead.SetActive(true);
                }
                else if (FindObjectOfType<ScoreManager>().p1Score == FindObjectOfType<ScoreManager>().p2Score)
                {
                    this.uiWhenDraw.SetActive(true);
                }
                StartCoroutine(PauseForAnnouncer(2));
            }
        }
    }

    IEnumerator PauseBeforeAMatch(float second)
    {
        Time.timeScale = 0;
        float freezeTime = Time.realtimeSinceStartup + second;
        while (Time.realtimeSinceStartup < freezeTime)
        {
            yield return 0;
        }
        this.uiBeforeEachMatch.SetActive(false);
        Time.timeScale = 1;
    }

    IEnumerator PauseForAnnouncer(float second)
    {
        Time.timeScale = 0;
        float freezeTime = Time.realtimeSinceStartup + second;
        while (Time.realtimeSinceStartup < freezeTime)
        {
            yield return 0;
        }

        this.uiWhenRedLead.SetActive(false);
        this.uiWhenBlueLead.SetActive(false);
        this.uiWhenDraw.SetActive(false);

        this.uiBeforeEachMatch.SetActive(true);

        StartCoroutine(PauseBeforeAMatch(2));
    }

    IEnumerator PauseForEndGame(float second)
    {
        Time.timeScale = 0;
        float freezeTime = Time.realtimeSinceStartup + second;
        while (Time.realtimeSinceStartup < freezeTime)
        {
            yield return 0;
        }

        this.endGameRed.SetActive(false);
        this.endGameBlue.SetActive(false);
        this.endGameNone.SetActive(false);

        SceneManager.LoadScene("NormalScene");
    }
}
