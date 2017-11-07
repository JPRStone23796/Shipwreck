using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    public Transform FrontOfShip,BackOfShip;
    public GameObject[] AIPoints;
    NavMeshAgent enemyAI;
    float startTime;
    float journeyLength;
    private double speed = 0.5;
    // Use this for initialization
   


    private float destinationGeneratorTimer;
    public float setDestinationGeneratorTimer;
    private bool bPlayerChasing;



    void DestinationGenerator()
    {


        if(FrontOfShip==null)
        {
            FrontOfShip = GameObject.Find("FrontAI").transform;
            BackOfShip = GameObject.Find("BackAI").transform;
        }
        int randomDestintion;
        if (bPlayerChasing == false)
        {
            if (destinationGeneratorTimer > 0)
            {
                destinationGeneratorTimer -= Time.deltaTime;
            }
            else
            {
                randomDestintion = Random.Range(1, 3);
             
                if (randomDestintion == 1)
                {
                    int pointRandom = Random.Range(0, 4);
                    enemyAI.destination = AIPoints[pointRandom].transform.position;
                }
                else if (randomDestintion == 2)
                {
                    int pointRandom = Random.Range(1, 3);

                    if (pointRandom == 1)
                    {
                        enemyAI.destination = FrontOfShip.position;
                        bPlayerChasing = true;
                    }

                    else if (pointRandom == 2)
                    {
                        enemyAI.destination = BackOfShip.position;
                        bPlayerChasing = true;
                    }


                }
                if((enemyAI.destination - transform.position).magnitude >1)
                    destinationGeneratorTimer = setDestinationGeneratorTimer;
            }
        } 
        else
        {
            CheckatDestination();
        } 
    }


    void CheckatDestination()
    {
        
            if((enemyAI.destination - transform.position).magnitude <1)
            {
                bPlayerChasing = false;
                destinationGeneratorTimer = 0;
            }
        
    }



    void Start()
    {
     
        enemyAI = GetComponent<NavMeshAgent>();
        for (int i = 0; i < AIPoints.Length; i++)
        {
            AIPoints[i] = GameObject.Find("Point" + (i + 1).ToString());
        }
        destinationGeneratorTimer = 0;

    }




    // Update is called once per frame
    void Update()
    {

        DestinationGenerator();

    }
}
