using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerDetectorScript : MonoBehaviour
{
    //[HideInInspector]
    public List<Transform> attackerDetection = new List<Transform>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Attacker")
        {
            attackerDetection.Add(other.transform);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "AttackerWithBall")
        {
            attackerDetection.Remove(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Attacker")
        {
            attackerDetection.Remove(other.transform);
        }
    }
}
