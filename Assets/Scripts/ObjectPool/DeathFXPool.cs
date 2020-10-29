using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFXPool : MonoBehaviour
{
    [SerializeField]
    private GameObject deathFXPrefab;
    [SerializeField]
    private Queue<GameObject> deathFXPool = new Queue<GameObject>();
    [SerializeField]
    private int poolSize = 0;

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject effect = Instantiate(deathFXPrefab);
            deathFXPool.Enqueue(effect);
            effect.SetActive(false);
        }
    }

    //take object out of pool
    public GameObject GetDeathFX()
    {
        if (deathFXPool.Count > 0)
        {
            GameObject effect = deathFXPool.Dequeue();
            effect.SetActive(true);
            return effect;
        }
        else
        {
            GameObject effect = Instantiate(deathFXPrefab);
            return effect;
        }
    }

    //return object to pool
    public void ReturnDeathFX(GameObject effect)
    {
        deathFXPool.Enqueue(effect);
        effect.SetActive(false);
    }
}
