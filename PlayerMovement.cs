using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{

    private NavMeshAgent playerObject;
    // Use this for initialization
    void Start()
    {
        playerObject = GetComponent<NavMeshAgent>();
       
      
    }


    public float buttonHeldTimer;
    void Movement()
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
                        playerObject.destination = hit.point;
                    }

                }
              
            }
        }
    }


    void Update()
    {
        Movement();
       
    }
}
