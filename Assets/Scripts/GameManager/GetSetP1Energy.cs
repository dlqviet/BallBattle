using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetSetP1Energy : MonoBehaviour
{
    private float p1Energy;
    private int maxEnergy;
    private int barNumber;
    private int energySubtractor;

    private void Start()
    {
        maxEnergy = FindObjectOfType<GameManager>().energyBar;
        barNumber = maxEnergy - 1;
        energySubtractor = 0;
    }

    private void FixedUpdate()
    {
        
    }
}
