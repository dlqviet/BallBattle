using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Attacker":
                this.gameObject.SetActive(false);
                break;
            case "Defender":
                Debug.Log("oh no... anyway");
                break;
        }
    }
}
