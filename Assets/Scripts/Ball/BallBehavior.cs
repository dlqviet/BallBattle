using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public GameObject attackerDetector;

    [HideInInspector]
    public bool passThisBall;

    private float ballSpeed;

    private void Start()
    {
        ballSpeed = FindObjectOfType<GameManager>().ballSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Attacker")
        {
            other.gameObject.tag = "AttackerWithBall";
            passThisBall = false;
            this.gameObject.SetActive(false);
            this.transform.parent = other.transform;
        }
    }

    private void FixedUpdate()
    {
        if (passThisBall)
        {
            Transform followAttacker = GetClosestAttacker(attackerDetector.GetComponent<AttackerDetectorScript>().attackerDetection);
            if (followAttacker)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, followAttacker.position, ballSpeed * Time.deltaTime);
            }
            else
            {
                FindObjectOfType<DefendSoldierBehavior>().defenderScored = true;
                passThisBall = false;
                this.gameObject.SetActive(false);
            }
        }
    }

    //get the closest attacker for passing ball
    Transform GetClosestAttacker(List<Transform> attackers)
    {
        Transform closestAtker = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = this.transform.position;

        foreach (Transform potentialAtker in attackers)
        {
            float dist = Vector3.Distance(potentialAtker.position, currentPosition);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestAtker = potentialAtker;
            }
        }
        return closestAtker;
    }
}
