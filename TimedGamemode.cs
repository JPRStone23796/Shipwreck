using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedGamemode : MonoBehaviour {



    public GameObject PlayerObject, EnemyObject, FortObject;
    private GameObject FortSpawn1, FortSpawn2, PlayerSpawn;
    private GameObject[] EnemySpawn;

    void Awake()
    {
        EnemySpawn = new GameObject[4];
        EnemySpawn[0] = GameObject.Find("Point1");
        EnemySpawn[1] = GameObject.Find("Point2");
        EnemySpawn[2] = GameObject.Find("Point3");
        EnemySpawn[3] = GameObject.Find("Point4");
        FortSpawn1 = GameObject.Find("TowerPoint1");
        FortSpawn2 = GameObject.Find("TowerPoint2");
        PlayerSpawn = GameObject.Find("PlayerPoint");
    }




    void GameBeginning()
    {
        Instantiate(PlayerObject, PlayerSpawn.transform.position, Quaternion.identity);
        Instantiate(FortObject, FortSpawn1.transform.position, Quaternion.identity);
        Instantiate(FortObject, FortSpawn2.transform.position, Quaternion.identity);
    }

    public GameObject[] CurrentEnemies;
    private bool GameBegan;
    void EnemyCurrent()
    {
        if(GameBegan && GameCountdown>0)
        {
            CurrentEnemies = GameObject.FindGameObjectsWithTag("EnemyShip");

            if (CurrentEnemies.Length <2)
            {
                int randomSpawn = Random.Range(0, 3);
                GameObject currentSpawn = EnemySpawn[randomSpawn];
                Instantiate(EnemyObject, currentSpawn.transform.position, Quaternion.identity);

            }
            GameCountdown -= Time.deltaTime;
        }
       


    }

    public float BeginningCountdown,GameCountdown;
    
    void Countdown()
    {
        if (BeginningCountdown >= 0)
        {
            BeginningCountdown -= Time.deltaTime;
        }

        if (BeginningCountdown < 0 && GameBegan == false) { GameBegan = true; }


    }

    // Use this for initialization
    void Start () {
        GameBeginning();

    }
	
	// Update is called once per frame
	void Update () {
        Countdown();
        EnemyCurrent();

    }
}
