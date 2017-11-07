using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlayerInfo
{
    int PlayerHealthMax, PlayerHealth, NumOfDeaths;


    public PlayerInfo()
    {
        PlayerHealthMax = 0;
        PlayerHealth = 0;
        NumOfDeaths = 0;
    }


    public void SetInfo(int MaxHealth)
    {

        PlayerHealthMax = MaxHealth;
        PlayerHealth = PlayerHealthMax;


    }



    public int GetPlayerHealth()
    {
        return PlayerHealth;
    }

    public void DamageTaken(int Damage)
    {
        PlayerHealth -= Damage;
    }

    public void ResetHealth()
    {
        PlayerHealth = PlayerHealthMax;
    }

    public void NewDeath()
    {
        NumOfDeaths++;
    }
}



public class PlayerManager : MonoBehaviour
{


    PlayerInfo PlayerStats = new PlayerInfo();

    void Start()
    {
        PlayerStats.SetInfo(100);
    }




    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "EnemysCannonBall")
        {
            PlayerStats.DamageTaken(50);
            Destroy(other.gameObject);
        }
    }
    

    
        
      

}
