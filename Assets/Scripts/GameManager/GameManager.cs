using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //game settings:
    [Header("Game Settings")]
    public float matchPerGame = 5;
    public float timePerMatch = 140;
    public float energyBar = 6;
    public float energyRegen = 0.5f;

    //attacker:
    [Header("Attacking Soldiers:")]
    public float atk_energyCost = 2;
    public float atk_spawnTime = 0.5f;
    public float atk_reactivateTime = 2.5f;
    public float atk_normalSpeed = 1.5f;
    public float atk_carryingSpeed = 0.75f;

    //defender:
    [Header("Defending Soldiers:")]
    public float def_energyCost = 3;
    public float def_spawnTime = 0.5f;
    public float def_reactivateTime = 4;
    public float def_normalSpeed = 1;
    public float def_returnSpeed = 2;
    public float def_detectionRange = 35;

    //ball:
    [Header("Ball:")]
    public float ballSpeed = 1.5f;

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }
}
