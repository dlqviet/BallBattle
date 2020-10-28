using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarSlotObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject barSlotPrefab;
    [SerializeField]
    private Queue<GameObject> barSlotPool = new Queue<GameObject>();
    private int poolSize = 0;

    private void Start()
    {
        poolSize = FindObjectOfType<GameManager>().energyBar;

        for (int i = 0; i < poolSize * 2; i++)
        {
            GameObject attacker = Instantiate(barSlotPrefab);
            barSlotPool.Enqueue(attacker);
            attacker.SetActive(false);
        }
    }

    //take object out of pool
    public GameObject GetBarSlot()
    {
        if (barSlotPool.Count > 0)
        {
            GameObject barSlot = barSlotPool.Dequeue();
            barSlot.SetActive(true);
            return barSlot;
        }
        else
        {
            GameObject barSlot = Instantiate(barSlotPrefab);
            return barSlot;
        }
    }

    //return object to pool
    public void ReturnATKBar(GameObject thisBarSLot)
    {
        barSlotPool.Enqueue(thisBarSLot);
        thisBarSLot.SetActive(false);
    }
}
