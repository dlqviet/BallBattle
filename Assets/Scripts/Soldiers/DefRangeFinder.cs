using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefRangeFinder : MonoBehaviour
{
    [HideInInspector]
    public float detectionRange;
    [HideInInspector]
    public GameObject target;
    [HideInInspector]
    public bool startFollow;

    private void Start()
    {
        detectionRange = FindObjectOfType<GameManager>().def_detectionRange;
        this.gameObject.GetComponent<SphereCollider>().radius = detectionRange / 30;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "AttackerWithBall") 
        {
            startFollow = true;
            target = other.gameObject;
        }
    }
}
