using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AttackSoldierBehavior : MonoBehaviour
{
    private float energyCost;
    private float spawnTime;
    private float reactivateTime;
    private float normalSpeed;
    private float carryingSpeed;

    //variable for delaying update function
    private float timer;

    private AttackerPool attackerPool;
    private FieldManager field;

    private GameObject ballToFollow;

    private void Start()
    {
        attackerPool = FindObjectOfType<AttackerPool>();
        field = FindObjectOfType<FieldManager>();

        ballToFollow = GameObject.FindGameObjectWithTag("Ball");

        //get parameters
        energyCost = FindObjectOfType<GameManager>().atk_energyCost;
        spawnTime = FindObjectOfType<GameManager>().atk_spawnTime;
        reactivateTime = FindObjectOfType<GameManager>().atk_reactivateTime;
        normalSpeed = FindObjectOfType<GameManager>().atk_normalSpeed;
        carryingSpeed = FindObjectOfType<GameManager>().atk_carryingSpeed;

        timer = spawnTime;

        StartCoroutine(Spawning());
    }

    IEnumerator Spawning()
    {
        yield return new WaitForSeconds(spawnTime);
        this.TeamColorChange();
    }

    private void FixedUpdate()
    {
        //run straight
        //this.transform.position += transform.forward * normalSpeed * Time.deltaTime;

        //follow ball
        if (!this.transform.GetChild(2).gameObject.activeSelf)
            this.transform.position = Vector3.MoveTowards(this.transform.position, ballToFollow.transform.position, normalSpeed * Time.deltaTime);
        else
            this.transform.position = this.transform.position;
        //run with ball
        //this.transform.position += transform.forward * carryingSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Limit")
        {
            this.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "Ball")
        {
            this.InactiveColorChange();
        }
    }

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
        this.transform.GetChild(2).gameObject.SetActive(false);
        if (field.p1Color == true && field.p2Color == false)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (field.p1Color == false && field.p2Color == true)
        {
            this.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    //change color due to inactive
    private void InactiveColorChange()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(2).gameObject.SetActive(true);
    }
}
