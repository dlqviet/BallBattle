using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public GameObject p1Field;
    public GameObject p2Field;
    public GameObject ball;
    [HideInInspector]
    public bool p1Color;
    [HideInInspector]
    public bool p2Color;

    private int matchCounter = 0;
    private float atkEnergyCost;
    private float defEnergyCost;

    private AttackerPool attackerPool;
    private DefenderPool deffenderPool;
    private EnergyManager playerEnergy;

    private Ray ray;
    private RaycastHit rayHit;

    private void Start()
    {
        atkEnergyCost = FindObjectOfType<GameManager>().atk_energyCost;
        defEnergyCost = FindObjectOfType<GameManager>().def_energyCost;

        attackerPool = FindObjectOfType<AttackerPool>();
        deffenderPool = FindObjectOfType<DefenderPool>();
        playerEnergy = FindObjectOfType<EnergyManager>();

        //random atk/def on the first match
        int randomNUmber = Random.Range(0, 2);
        if (randomNUmber == 0)
        {
            p1Field.GetComponent<PlayerFieldManager>().atk = true;
            p2Field.GetComponent<PlayerFieldManager>().atk = false;
        }
        else
        {
            p1Field.GetComponent<PlayerFieldManager>().atk = false;
            p2Field.GetComponent<PlayerFieldManager>().atk = true;
        }
        SpawningBall();
    }

    //control with touch
    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out rayHit))
            {
                //click on player1 field
                if (rayHit.collider.CompareTag("P1Field"))
                {
                    p1Color = true;
                    p2Color = false;
                    //distinguish atk and def for player 1
                    if (p1Field.GetComponent<PlayerFieldManager>().atk == true && 
                        playerEnergy.p1Energy >= atkEnergyCost)
                    {
                        playerEnergy.p1Energy -= atkEnergyCost;

                        //spawn player 1 attacker
                        GameObject newAttacker = attackerPool.GetAttacker();
                        newAttacker.transform.position = rayHit.point;
                        newAttacker.transform.Rotate(0, 180, 0);
                    }
                    else if (p1Field.GetComponent<PlayerFieldManager>().atk == false && 
                        playerEnergy.p1Energy >= defEnergyCost)
                    {
                        playerEnergy.p1Energy -= defEnergyCost;

                        //spawn player 1 defender
                        GameObject newDefender = deffenderPool.GetDefender();
                        newDefender.transform.position = rayHit.point;
                        newDefender.transform.Rotate(0, 180, 0);
                    }
                }

                //click on player2 field
                if (rayHit.collider.CompareTag("P2Field"))
                {
                    p2Color = true;
                    p1Color = false;
                    //distinguish atk and def for player 2
                    if (p2Field.GetComponent<PlayerFieldManager>().atk == true &&
                        playerEnergy.p2Energy >= atkEnergyCost)
                    {
                        playerEnergy.p2Energy -= atkEnergyCost;

                        //spawn player 2 attacker
                        GameObject newAttacker = attackerPool.GetAttacker();
                        newAttacker.transform.position = rayHit.point;
                    }
                    else if (p2Field.GetComponent<PlayerFieldManager>().atk == false &&
                        playerEnergy.p2Energy >= defEnergyCost)
                    {
                        playerEnergy.p2Energy -= defEnergyCost;

                        //spawn player 2 defender
                        GameObject newDefender = deffenderPool.GetDefender();
                        newDefender.transform.position = rayHit.point;
                    }
                }
            }
        }

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out rayHit, 1000))
                {
                    //click on player1 field
                    if (rayHit.collider.CompareTag("P1Field"))
                    {
                        p1Color = true;
                        p2Color = false;
                        if (p1Field.GetComponent<PlayerFieldManager>().atk == true)
                        {
                            GameObject newAttacker = attackerPool.GetAttacker();
                            newAttacker.transform.position = rayHit.point;
                            newAttacker.transform.Rotate(0, 180, 0);
                        }
                        else
                        {
                            GameObject newDefender = deffenderPool.GetDefender();
                            newDefender.transform.position = rayHit.point;
                            newDefender.transform.Rotate(0, 180, 0);
                        }
                    }

                    //click on player2 field
                    if (rayHit.collider.CompareTag("P2Field"))
                    {
                        p2Color = true;
                        p1Color = false;
                        if (p2Field.GetComponent<PlayerFieldManager>().atk == true)
                        {
                            GameObject newAttacker = attackerPool.GetAttacker();
                            newAttacker.transform.position = rayHit.point;
                        }
                        else
                        {
                            GameObject newDefender = deffenderPool.GetDefender();
                            newDefender.transform.position = rayHit.point;
                        }
                    }
                }

            }
        }
    }

    private void SpawningBall()
    {
        if (p1Field.GetComponent<PlayerFieldManager>().atk == true)
        {
            Vector3 ballPosition = new Vector3(Random.Range(-3f, 3f), 0.25f, Random.Range(1f, 5.5f));
            ball.transform.position = ballPosition;
            ball.SetActive(true);
        }
        else
        {
            Vector3 ballPosition = new Vector3(Random.Range(-3f, 3f), 0.25f, Random.Range(-1f, -5f));
            ball.transform.position = ballPosition;
            ball.SetActive(true);
        }
    }

    public void UpdateAfterMatchEnd()
    {
        matchCounter++;
        //swap atk/def between 2 teams after each match
        if (p1Field.GetComponent<PlayerFieldManager>().atk == true)
        {
            p1Field.GetComponent<PlayerFieldManager>().atk = false;
            p2Field.GetComponent<PlayerFieldManager>().atk = true;
        }
        else
        {
            p1Field.GetComponent<PlayerFieldManager>().atk = true;
            p2Field.GetComponent<PlayerFieldManager>().atk = false;
        }
    }

}
