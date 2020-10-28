using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditorInternal;
using UnityEngine;

public class AttackSoldierBehavior : MonoBehaviour
{
    private float energyCost;
    private float spawnTime;
    private float reactivateTime;
    private float normalSpeed;
    private float carryingSpeed;
    private bool p1Field;
    private bool p2Field;

    private AttackerPool attackerPool;

    private GameObject ballToFollow;
    //private bool thereIsBall;

    private void Start()
    {
        //get scripts
        attackerPool = FindObjectOfType<AttackerPool>();

        //get parameters
        energyCost = FindObjectOfType<GameManager>().atk_energyCost;
        spawnTime = FindObjectOfType<GameManager>().atk_spawnTime;
        reactivateTime = FindObjectOfType<GameManager>().atk_reactivateTime;
        normalSpeed = FindObjectOfType<GameManager>().atk_normalSpeed;
        carryingSpeed = FindObjectOfType<GameManager>().atk_carryingSpeed;
        p1Field = FindObjectOfType<FieldManager>().p1Color;
        p2Field = FindObjectOfType<FieldManager>().p2Color;

        StartCoroutine(Spawning());
    }

    private void FixedUpdate()
    {
        ballToFollow = GameObject.FindGameObjectWithTag("Ball");
        //thereIsBall = GameObject.FindGameObjectWithTag("Ball");
        if (!this.transform.GetChild(2).gameObject.activeSelf)
        {
            if (ballToFollow)
            {
                //follow ball
                this.transform.position = Vector3.MoveTowards(this.transform.position, ballToFollow.transform.position, normalSpeed * Time.deltaTime);
            }
            else if (!ballToFollow && this.transform.GetChild(3).gameObject.activeSelf)
            {
                //run with ball
                this.transform.position += transform.forward * carryingSpeed * Time.deltaTime;
            }
            else
            {
                //run straight
                this.transform.position += transform.forward * normalSpeed * Time.deltaTime;
            }
        }
        else
            this.transform.position = this.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Goal":
                if (this.gameObject.tag == "AttackerWithBall")
                {

                }
                break;
            case "Limit":
                if (this.gameObject.tag == "AttackerWithBall")
                {
                    this.transform.GetChild(5).gameObject.SetActive(true);
                    this.transform.GetChild(5).transform.parent = null;
                    FindObjectOfType<BallBehavior>().passThisBall = true;
                }
                this.gameObject.SetActive(false);
                break;
            case "Ball":
                //turn on fake ball and highlight
                this.transform.GetChild(3).gameObject.SetActive(true);
                this.transform.GetChild(4).gameObject.SetActive(true);
                break;
            case "Defender":
                if (this.transform.childCount > 5)
                {
                    InactiveColorChange();
                    this.transform.GetChild(5).gameObject.SetActive(true);
                    this.transform.GetChild(5).transform.parent = null;
                    FindObjectOfType<BallBehavior>().passThisBall = true;
                }
                break;
        }
    }

    //put object into object pool
    private void OnDisable()
    {
        if(attackerPool != null)
        {
            attackerPool.ReturnAttacker(this.gameObject);
        }
    }

    //change color according to team
    private void TeamColorChange()
    {
        this.gameObject.tag = "Attacker";

        this.GetComponent<Collider>().enabled = true;

        //set team color
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

        //turn off color, fake ball and highlight, turn on inactive state
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(2).gameObject.SetActive(true);
        this.transform.GetChild(3).gameObject.SetActive(false);
        this.transform.GetChild(4).gameObject.SetActive(false);

        //this.gameObject.tag = "Attacker";

        //Active after a set of time
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
