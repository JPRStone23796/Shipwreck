using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class ImprovedPlayerMovement : MonoBehaviour {

    private NavMeshAgent playerObject;
    // Use this for initialization
    void Start()
    {
        playerObject = GetComponent<NavMeshAgent>();
        PlayerDestination = new NavMeshPath();
        i = 1;

    }


    public float buttonHeldTimer;
    public NavMeshPath PlayerDestination ;
    void BeginMovement()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {

            if ((Input.GetButtonDown("Fire1")))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.tag == "Sea")
                    {
                       playerObject.CalculatePath(hit.point,PlayerDestination);
                     
                        i = 1;
                    }

                }

            }
        }
    }


    public float Speed,RotationSpeed,currentSpeed;
    float previousPoint;
    int i;
    void TravelToDestination()
    {
        currentSpeed = 0;
        if (PlayerDestination.corners.Length >0)
        {
        if (i<PlayerDestination.corners.Length)
        {
            Vector3 currentCorner = PlayerDestination.corners[i];
            if((currentCorner - transform.position).magnitude >1)
            {
                 
            Vector3 relativePos = currentCorner - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
                   
                    if (playerObject.isOnNavMesh)
                    {
                        if (Quaternion.Angle(transform.rotation, rotation) < 15)
                        {
                            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * RotationSpeed);
                            currentSpeed = Speed;
                            transform.position += transform.forward * Time.deltaTime * currentSpeed;
                        }
                        if (Quaternion.Angle(transform.rotation, rotation) >=15 && Quaternion.Angle(transform.rotation, rotation)<50)
                        {
                            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * RotationSpeed);
                            currentSpeed = Speed/2f;
                            transform.position += transform.forward * Time.deltaTime * currentSpeed;
                        }

                        if (Quaternion.Angle(transform.rotation, rotation) > 50)
                        {
                            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * (RotationSpeed));
                            currentSpeed = 0f;
                        }


                    }

                    previousPoint = playerObject.remainingDistance;

                }
                if ((currentCorner - transform.position).magnitude < 1)
                {
                    i++;
                }
               
            }
         

        }
     
       

    }


    void Update()
    {
        BeginMovement();
        TravelToDestination();

    }
}
