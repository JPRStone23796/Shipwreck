using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyStats : MonoBehaviour {


    public float PlayerHealth = 100;
    private Image PlayerHealthUI;
 

    void Awake()
    {
        PlayerHealthUI = GetComponentInChildren<Image>();
    }



    void HealthCheck()
    {
        if(PlayerHealth<=0)
        {
            Destroy(this.gameObject);
        }
    }


   


    private void FixedUpdate()
    {
        HealthCheck();
       
    }



    void OnCollisionEnter(Collision other)
    {
   
        if (other.gameObject.tag == "CannonBall")
        {
            Destroy(other.gameObject);
            PlayerHealth -= 50;
            float fill = PlayerHealth / 100;
            PlayerHealthUI.fillAmount = fill;
        }
    }
}
