using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DefendSoldierBehavior : MonoBehaviour
{
    [HideInInspector]
    public bool defenderScored;

    private float spawnTime;
    private float reactivateTime;
    private float normalSpeed;
    private float returnSpeed;
    private bool p1Field;
    private bool p2Field;

    private bool returnNow;

    private DefenderPool defenderPool;
    private DefRangeFinder rangeFinder;

    private void Start()
    {
        //get scripts
        defenderPool = FindObjectOfType<DefenderPool>();
        rangeFinder = FindObjectOfType<DefRangeFinder>();

        //get parameters
        spawnTime = FindObjectOfType<GameManager>().def_spawnTime;
        reactivateTime = FindObjectOfType<GameManager>().def_reactivateTime;
        normalSpeed = FindObjectOfType<GameManager>().def_normalSpeed;
        returnSpeed = FindObjectOfType<GameManager>().def_returnSpeed;
        p1Field = FindObjectOfType<FieldManager>().p1Color;
        p2Field = FindObjectOfType<FieldManager>().p2Color;

        StartCoroutine(Spawning());
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Limit":
                returnNow = true;
                break;
            case "AttackerWithBall":
                InactiveColorChange();
                break;          
        }
    }

    private void FixedUpdate()
    {
        //check for scoring
        if (defenderScored == true)
        {
            if (p1Field && !p2Field)
            {
                FindObjectOfType<ScoreManager>().player1Scored = true;
                defenderScored = false;
            }
            if (!p1Field && p2Field)
            {
                FindObjectOfType<ScoreManager>().player2Scored = true;
                defenderScored = false;
            }
        }

        //return to 1st position if inactive or hit limit
        if (this.transform.GetChild(2).gameObject.activeSelf || returnNow)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, 
                this.rangeFinder.transform.position, 
                returnSpeed * Time.deltaTime);
        }
        //start chasing down attacker with ball
        else if (rangeFinder.startFollow)
        {
            Transform targetPosition = rangeFinder.target.transform;
            this.transform.position = Vector3.MoveTowards(this.transform.position, 
                targetPosition.position, 
                normalSpeed * Time.deltaTime);
        }
    }

    //put object into object pool
    private void OnDisable()
    {
        if (defenderPool != null)
        {
            defenderPool.ReturnDefender(this.gameObject);
        }
    }

    //change color according to team
    private void TeamColorChange()
    {
        this.GetComponent<Collider>().enabled = true;

        this.transform.GetChild(2).gameObject.SetActive(false);
        if (p1Field && !p2Field)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (!p1Field && p2Field)
        {
            this.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    //change color due to inactive
    private void InactiveColorChange()
    {
        this.GetComponent<Collider>().enabled = false;

        rangeFinder.target = null;

        rangeFinder.startFollow = false;

        //turn off color and turn on inactive state
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(2).gameObject.SetActive(true);

        StartCoroutine(Reactive());
    }

    IEnumerator Spawning()
    {
        yield return new WaitForSeconds(spawnTime);
        this.TeamColorChange();
    }

    IEnumerator Reactive()
    {
        yield return new WaitForSeconds(reactivateTime);
        TeamColorChange();
    }
}
