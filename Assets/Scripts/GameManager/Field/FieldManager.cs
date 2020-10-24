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

    private AttackerPool attackerPool;
    private DeffenderPool deffenderPool;

    private Ray ray;
    private RaycastHit rayHit;

    private void Start()
    {
        attackerPool = FindObjectOfType<AttackerPool>();
        deffenderPool = FindObjectOfType<DeffenderPool>();

        matchCounter++;
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

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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

    private void SpawningBall()
    {
        Vector3 ballPosition = new Vector3(Random.Range(-3f, 3f), 0.25f, Random.Range(0f, -5f));
        ball.transform.position = ballPosition;
        ball.SetActive(true);
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
