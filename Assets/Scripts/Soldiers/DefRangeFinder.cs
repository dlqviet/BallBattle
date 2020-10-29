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
        this.gameObject.transform.localScale = new Vector3(detectionRange / 10, detectionRange / 10, detectionRange/10);
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
