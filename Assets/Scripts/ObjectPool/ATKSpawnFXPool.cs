using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATKSpawnFXPool : MonoBehaviour
{
    [SerializeField]
    private GameObject atkSpawnPrefab;
    [SerializeField]
    private Queue<GameObject> atkSpawnFXPool = new Queue<GameObject>();
    [SerializeField]
    private int poolSize = 0;

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject effect = Instantiate(atkSpawnPrefab);
            atkSpawnFXPool.Enqueue(effect);
            effect.SetActive(false);
        }
    }

    //take object out of pool
    public GameObject GetFX()
    {
        if (atkSpawnFXPool.Count > 0)
        {
            GameObject effect = atkSpawnFXPool.Dequeue();
            effect.SetActive(true);
            return effect;
        }
        else
        {
            GameObject effect = Instantiate(atkSpawnPrefab);
            return effect;
        }
    }

    //return object to pool
    public void ReturnFX(GameObject effect)
    {
        atkSpawnFXPool.Enqueue(effect);
        effect.SetActive(false);
    }
}
