using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannon : MonoBehaviour {


    public float velocity;
    float gravity = 9.81f;
    float radianAngle;
    public GameObject Cannon;
    private GameObject Player;


   
    void FireCannon()
    {
        RBVelocity(Player.transform.position);
        Vector3 FuturePlayerPos = ProjectileTime();
        GameObject cannonProjectile = Instantiate(Cannon, transform.position, transform.rotation) as GameObject;
        Rigidbody cannonRB = cannonProjectile.GetComponent<Rigidbody>();
        transform.LookAt(FuturePlayerPos);
        Vector3 shotVelocity = (transform.forward + Vector3.up) * velocity * 0.805f;
        cannonRB.velocity = shotVelocity;
    }


    void RBVelocity(Vector3 pos)
    {

        radianAngle = Mathf.Deg2Rad * 45;
        float maxDistance = ((pos - transform.position).magnitude);
        velocity = (maxDistance * gravity) / Mathf.Sin(2 * radianAngle);
        velocity = Mathf.Sqrt(velocity);
    }

    Vector3 ProjectileTime()
    {
        float TimeOfFlight = (Mathf.Sqrt(2 * velocity)) / gravity;
        ImprovedPlayerMovement PlayerMovement = Player.GetComponent<ImprovedPlayerMovement>();
        Vector3 futurePosition = ((Player.transform.forward * PlayerMovement.currentSpeed) / TimeOfFlight * Random.Range(0.97f, 1.0f)) + Player.transform.position;

        RBVelocity(futurePosition);

        return futurePosition;
    }




    private float CannonTimer;
    public float setCannonTimer;

    void CannonCheck()
    {


        if(Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        if (((Player.transform.position - transform.position).magnitude < 10) && CannonTimer <= 0)
        {
            FireCannon();
            CannonTimer = setCannonTimer;
        }

        if (CannonTimer > 0)
        {
            CannonTimer -= Time.deltaTime;
        }
    }


    // Update is called once per frame
    void Update () {

        CannonCheck();

    }
}
