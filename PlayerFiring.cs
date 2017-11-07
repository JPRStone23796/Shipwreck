using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFiring : MonoBehaviour {

   
    public GameObject CannonBall;
    private bool cannonFireable;
    private float cannonCountdown;
    public float CannonTimer;
    private GameObject Player,Cannon;
    private ProjecileMeshArc arc;

    // Use this for initialization
    void Start () {
        Player = GameObject.Find("Player");
        Cannon = GameObject.Find("Cannon");
        arc = GetComponent<ProjecileMeshArc>();
        cannonCountdown = 0;
	}





    private float buttonHeldTimer;
    private Vector3 ballDrection;

    public void CannonAim()
    {
        if (cannonFireable)
        { 
        if (Input.GetButton("Fire1") && buttonHeldTimer == 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Player")
                {

                    buttonHeldTimer += Time.deltaTime;
                }
            }
        }


        if (Input.GetButton("Fire1") && buttonHeldTimer > 0)
        {

            buttonHeldTimer += Time.deltaTime;
                //DrawArrow();
                DrawMesh();
        }

        if ((!Input.GetButton("Fire1")) && buttonHeldTimer < 0.2)
        {
            buttonHeldTimer = 0;
        }

        if ((!Input.GetButton("Fire1")) && buttonHeldTimer > 0.2)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {

                ballDrection = hit.point - transform.position;
                buttonHeldTimer = 0;
                FireCannonBall();

            }

        }
    }
    }

    void FireCannonBall()
    {
        if (cannonFireable)
        {
            GameObject cannonProjectile = Instantiate(CannonBall, transform.position, transform.rotation) as GameObject;
            Rigidbody cannonRB = cannonProjectile.GetComponent<Rigidbody>();
            //cannonRB.AddForce(Vector3.up * 8000 * 2);

            //if(ballDrection.magnitude<15)
            //{
            //    cannonRB.velocity = (ballDrection * 1.8f);
            //}

            //if (ballDrection.magnitude > 15)
            //{
            //    cannonRB.velocity = (ballDrection * 1.4f);
            //}

            //if (ballDrection.magnitude > 30)
            //{
            //    cannonRB.velocity = (ballDrection * 1.1f);
            //}


            Vector3 shotVelocity = (transform.forward + Vector3.up) * arc.velocity * 0.805f;
            cannonRB.velocity = shotVelocity;

            cannonFireable = false;
            cannonCountdown = CannonTimer;        
            ballDrection = Vector3.zero;
            arc.ProjectileLand = Vector3.zero;

         




        }
       
    }


    public Material Normal;
    private Color CountdownColour;

    void CannonBallCountDown()
    {
        GameObject boat = GameObject.Find("Boat");
        if(cannonCountdown>0)
        {
            if (cannonFireable == false)
            {
                cannonCountdown -= Time.deltaTime;
                CountdownColour = Color.Lerp(Normal.color,Color.white, Mathf.PingPong(Time.time, 1));
                boat.GetComponent<Renderer>().material.color = CountdownColour;  
            }
        }
        else if (cannonCountdown<=0)
        {
            boat.GetComponent<Renderer>().material = Normal;
            cannonFireable = true;
   
        }
        
    }

    Vector3 FirePos;
    void DrawMesh()
    {

        FirePos = MousePosition;
        arc.ProjectileLand = FirePos;
        Cannon.transform.LookAt(FirePos);
    }





    public GameObject ArrowPrefab,ArrowHeadPrefab;
    private Vector3 MousePosition;
    GameObject arrow,arrowhead;
    //void DrawArrow()
    //{
    //    Vector3 arrowPos = (transform.position + MousePosition) / 2;
    //    if (arrow == null)
    //    {
    //        arrow = Instantiate(ArrowPrefab, arrowPos, Quaternion.identity) as GameObject;
    //        arrowhead = Instantiate(ArrowHeadPrefab, MousePosition, Quaternion.identity) as GameObject;

    //    }
    //    if (arrow != null) { arrow.transform.position = arrowPos; }
    //    if (arrowhead != null) { arrowhead.transform.position = MousePosition; }
    //    arrow.transform.LookAt(MousePosition);
    //    arrowhead.transform.LookAt(MousePosition);
    //    Cannon.transform.LookAt(MousePosition);
       
    //    float dist = (MousePosition - transform.position).magnitude;
    //    arrow.transform.localScale = new Vector3(0.05f, 0.05f, dist);
    //}







    void FixedUpdate()
    {

        if ((Input.GetButton("Fire1")) && buttonHeldTimer > 0.2)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Sea"|| hit.transform.gameObject.tag == "Environment" || hit.transform.gameObject.tag == "Enemy")
                {

                    MousePosition = hit.point;
                }
            }
        }

     
    }















    // Update is called once per frame
    void Update () {
        CannonAim();
        CannonBallCountDown();

    }
}
