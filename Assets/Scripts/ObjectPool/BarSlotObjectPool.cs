using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarSlotObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject barSlotPrefab;
    [SerializeField]
    private Queue<GameObject> barSlotPool = new Queue<GameObject>();
    private int poolSize;

    private void Start()
    {
        poolSize = FindObjectOfType<GameManager>().energyBar ;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject barSlot = Instantiate(barSlotPrefab);
            barSlotPool.Enqueue(barSlot);
            barSlot.SetActive(false);
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
    public void ReturnBarSlot(GameObject thisBarSLot)
    {
        barSlotPool.Enqueue(thisBarSLot);
        thisBarSLot.SetActive(false);
    }
}
