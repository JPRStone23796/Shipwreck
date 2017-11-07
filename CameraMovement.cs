using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {


    public GameObject PlayerObject;
    Vector3 Offset;

    void Start()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        Offset = transform.position - PlayerObject.transform.position;
    }

    public float speed = 0.3F;
    private Vector3 CameraVelocity = Vector3.zero;

    void UpdateCamera()
    {
        if(PlayerObject==null)
        {
            PlayerObject = GameObject.FindGameObjectWithTag("Player");
            Offset = transform.position - PlayerObject.transform.position;
        }
        transform.position = Vector3.SmoothDamp(transform.position, (PlayerObject.transform.position + Offset), ref CameraVelocity, speed);
        Quaternion rotation = Quaternion.Euler(45.7f, transform.eulerAngles.y, 0);
        transform.rotation = rotation;
    }


    public bool RotateBegin;
    public float Rotationspeed = 15;
   public Vector3 beginningAngle;

    void rotationInput()
    {

        if(Input.GetKeyDown(KeyCode.Space) && RotateBegin==false)
        {
            RotateBegin = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && RotateBegin == true)
        {
            RotateBegin = false;
        }

        if (RotateBegin==true )
        {
            
            if (beginningAngle == Vector3.zero) { beginningAngle = new Vector3(45.7f, transform.eulerAngles.y, 0); }          
            transform.LookAt(PlayerObject.transform);
            Offset = transform.position - PlayerObject.transform.position;
            transform.RotateAround(PlayerObject.transform.position, Vector3.up, Rotationspeed * Time.deltaTime);

            if ((transform.eulerAngles.y - beginningAngle.y) >= 89)
            {
                RotateBegin = false;
                beginningAngle = new Vector3(45.7f, transform.eulerAngles.y, 0);
            }
        }

        if (RotateBegin == false)
        {
            if(beginningAngle.y >355)
            {
              
                transform.LookAt(PlayerObject.transform);
                Offset = transform.position - PlayerObject.transform.position;
                transform.RotateAround(PlayerObject.transform.position, Vector3.up, Rotationspeed * Time.deltaTime);
                beginningAngle = new Vector3(45.7f, transform.eulerAngles.y, 0);
            }
        }

         

    }


   



    void FixedUpdate()
    {
        UpdateCamera();
        rotationInput();

    }
    
}
