using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderPool : MonoBehaviour
{
    [SerializeField]
    private GameObject defenderPrefab;
    [SerializeField]
    private Queue<GameObject> defenderPool = new Queue<GameObject>();
    [SerializeField]
    private int poolSize = 0;

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject defender = Instantiate(defenderPrefab);
            defenderPool.Enqueue(defender);
            defender.SetActive(false);
        }
    }

    public GameObject GetDefender()
    {
        if (defenderPool.Count > 0)
        {
            GameObject defender = defenderPool.Dequeue();
            defender.SetActive(true);
            return defender;
        }
        else
        {
            GameObject defender = Instantiate(defenderPrefab);
            return defender;
        }
    }

    public void ReturnDefender(GameObject defender)
    {
        defenderPool.Enqueue(defender);
        defender.SetActive(false);
    }
}
