using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FortManager : MonoBehaviour {

    public bool PlayerEnter;
    public GameObject Cannon,FortParent;
    private GameObject Player;
    

    public float PlayerHealth = 100;
    public Image FortHealth;




    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        FortParent = transform.parent.gameObject;
    }




    public float velocity;
    float gravity = 9.81f;
    float radianAngle;


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
        Vector3 futurePosition = ((Player.transform.forward *  PlayerMovement.currentSpeed) / TimeOfFlight * Random.Range(0.97f,1.0f)) + Player.transform.position;

        RBVelocity(futurePosition);

        return futurePosition;
    }

    private float reloadTimer;
    public float setReloadTimer;

    void CannonTimer()
    {

        if(Player==null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        if(PlayerEnter)
        {
            if (reloadTimer > 0) { reloadTimer -= Time.deltaTime; }
            if (reloadTimer<=0)
            {
                FireCannon();
                reloadTimer = setReloadTimer;
            }
        }

    }


    Vector3 Pdistance;
    void PlayerDistance()
    {
        Pdistance = Player.transform.position - transform.position;
        if(Pdistance.magnitude <15)
        {
            PlayerEnter = true;
        }

        if (Pdistance.magnitude >= 15)
        {
            PlayerEnter = false;
        }

    }





    void HealthCheck()
    {
        if (PlayerHealth <= 0)
        {
            Destroy(FortParent.gameObject);
        }
    }


    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "CannonBall")
        {
            Destroy(other.gameObject);
            PlayerHealth -= 50;
            float fill = PlayerHealth / 100;
            FortHealth.fillAmount = fill;
        }
    }



    void FixedUpdate()
    {
        CannonTimer();
        PlayerDistance();
        HealthCheck();
    }
}
