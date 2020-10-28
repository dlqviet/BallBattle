using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    [Header("Player 1 Energy Bar")]
    public GameObject p1EnergySlots;
    public GameObject p1EnergyContainer;
    public GameObject p1EnergyHighlight;

    [Header("Player 2 Energy Bar")]
    public GameObject p2EnergySlots;
    public GameObject p2EnergyContainer;
    public GameObject p2EnergyHighlight;

    [HideInInspector]
    public float p1Energy;
    [HideInInspector]
    public float p2Energy;
    [HideInInspector]
    public int energyBar;
    [HideInInspector]
    public float highlightSlot = 1;

    private BarSlotObjectPool playerBarSlotOP;

    private float energyRegen;

    private void Start()
    {
        playerBarSlotOP = FindObjectOfType<BarSlotObjectPool>();
        energyBar = FindObjectOfType<GameManager>().energyBar;
        energyRegen = FindObjectOfType<GameManager>().energyRegen;

        p1EnergyContainer.GetComponent<Slider>().maxValue = energyBar;
        p1EnergyHighlight.GetComponent<Slider>().maxValue = energyBar;

        p2EnergyContainer.GetComponent<Slider>().maxValue = energyBar;
        p2EnergyHighlight.GetComponent<Slider>().maxValue = energyBar;

        for (int i = 0; i < energyBar; i++)
        {
            GameObject aNewBar = playerBarSlotOP.GetBarSlot();
            aNewBar.transform.parent = p1EnergySlots.transform;
            GameObject anotherNewBar = playerBarSlotOP.GetBarSlot();
            anotherNewBar.transform.parent = p2EnergySlots.transform;
        }
    }

    private void FixedUpdate()
    {
        if (p1Energy > energyBar)
        {
            p1Energy = energyBar;
        }
        if (p2Energy > energyBar)
        {
            p2Energy = energyBar;
        }

        p1Energy += energyRegen * Time.deltaTime;
        p2Energy += energyRegen * Time.deltaTime;

        p1EnergyContainer.GetComponent<Slider>().value = p1Energy;
        p2EnergyContainer.GetComponent<Slider>().value = p2Energy;

        for (int i = 0; i <= energyBar; i++)
        {
            if (p2Energy >= i)
            {
                p2EnergyHighlight.GetComponent<Slider>().value = i;
            }
            else if (p2Energy >= energyBar)
            {
                p2EnergyHighlight.GetComponent<Slider>().value = energyBar;
            }

            if (p1Energy >= i)
            {
                p1EnergyHighlight.GetComponent<Slider>().value = i;
            }
            else if (p1Energy >= energyBar)
            {
                p1EnergyHighlight.GetComponent<Slider>().value = energyBar;
            }
        }
    }
}
