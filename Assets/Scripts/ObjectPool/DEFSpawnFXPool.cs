using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEFSpawnFXPool : MonoBehaviour
{
    [SerializeField]
    private GameObject defSpawnFXPrefab;
    [SerializeField]
    private Queue<GameObject> defSpawnFXPool = new Queue<GameObject>();
    [SerializeField]
    private int poolSize = 0;

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject effect = Instantiate(defSpawnFXPrefab);
            defSpawnFXPool.Enqueue(effect);
            effect.SetActive(false);
        }
    }

    //take object out of pool
    public GameObject GetFX()
    {
        if (defSpawnFXPool.Count > 0)
        {
            GameObject effect = defSpawnFXPool.Dequeue();
            effect.SetActive(true);
            return effect;
        }
        else
        {
            GameObject effect = Instantiate(defSpawnFXPrefab);
            return effect;
        }
    }

    //return object to pool
    public void ReturnFX(GameObject effect)
    {
        defSpawnFXPool.Enqueue(effect);
        effect.SetActive(false);
    }
}
