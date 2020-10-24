using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerPool : MonoBehaviour
{
    [SerializeField]
    private GameObject attackerPrefab;
    [SerializeField]
    private Queue<GameObject> attackerPool = new Queue<GameObject>();
    [SerializeField]
    private int poolSize = 0;

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject attacker = Instantiate(attackerPrefab);
            attackerPool.Enqueue(attacker);
            attacker.SetActive(false);
        }
    }

    //take object out of pool
    public GameObject GetAttacker()
    {
        if (attackerPool.Count > 0)
        {
            GameObject attacker = attackerPool.Dequeue();
            attacker.SetActive(true);
            return attacker;
        }
        else
        {
            GameObject attacker = Instantiate(attackerPrefab);
            return attacker;
        }
    }

    //return object to pool
    public void ReturnAttacker(GameObject attacker)
    {
        attackerPool.Enqueue(attacker);
        attacker.SetActive(false);
    }
}
