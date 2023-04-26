using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player_Ally_Ai : MonoBehaviour
{
    public Transform[] enemies;

    Transform activeE; 
    // group of behaviors for AI to run 
    private enum Behaviors { Guard, Combat };

    //this create a attribute of behaviors and assigns it the initial value of Guard
    Behaviors aiBehaviors = Behaviors.Guard;

    //these two are for enemy combat
    public Transform shotspawn;
    public GameObject spell;

    //this bool is flipped based on whether or not the player is within range of the AI Enemy
    bool isINRANGE = false;

    //this int gives the integer representation of the array value value of the current waypoint.
    //int currentW = 0;

    // this is the nest waypoint for the AI to go to while patroling
    Vector3 Destination;

    //Reverse path boolean
    //bool rPath = false;


    //this takes in the player as transform to then gain knowledge of the position of the
    // player comared to the enemy;
    public Transform player;

    //general distance for traversing waypoints 
    float distance;

    //this is the distance to determine whether or not the enemy will engage with the player
    float fightDistance;

    //this array houses all the waypoint an AI can go to. Im setting it as public because its easy and I'm 
    //referencing the example from class and it has its arraypublic
    public Transform homeStation;

    NavMeshAgent nav;


    float second = 0;

    /// <summary>
    /// this function will be responsible for one of the AI walking in between several waypoints. 
    /// </summary>
    void patrol()
    {
        distance = Vector3.Distance(gameObject.transform.position, homeStation.position);
        Vector3 offset = new Vector3(2.5f, 0f, 2.5f);

        if (isINRANGE == false)
        {

            if (distance > 2.0f)
            {
                Destination = homeStation.position - offset;
                nav.SetDestination(Destination);
            }

        }
        else
        {
            aiBehaviors = Behaviors.Combat;
        }

    }

    /// <summary>
    /// Called when the AI enemy is going to engage in combat with the player
    /// </summary>
    //void fightplayer()
    //{
    //    if (isINRANGE)
    //    {

    //        shoot();


    //    }
    //    else
    //    {
    //        aiBehaviors = Behaviors.Guard;
    //    }


    //}


    /// <summary>
    /// this function is responsible for looking at the player and shootinng at them
    /// </summary>
    void shoot()
    {
        
        transform.LookAt(activeE.position);
        Instantiate(spell, shotspawn.position, shotspawn.rotation);
        Debug.Log("Ally Shooting");
        //if (second % 2 == 0)
        //{
           
        //    //second = 0;
        //}
    }

    /// <summary>
    /// this function is responsible for making the enemy follow the player
    /// </summary>
    void followEnemy()
    {
        fightDistance = Vector3.Distance(player.position, activeE.position);
        if (fightDistance <= 5f)
        {
            isINRANGE = true;
            if (isINRANGE == true)
            {
                second += Time.deltaTime;
                if (second >= 2)
                {
                    shoot();
                    second = 0;
                }
            }
            
        }
        else
        {
            isINRANGE = false;
            aiBehaviors = Behaviors.Guard;
        }
    }

    void GetActiveE()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (Vector3.Distance(enemies[i].position, player.position) <= 10)
            {
                activeE = enemies[i];
                
            }
        }
        followEnemy();
    }

    void runBehaviors()
    {
        switch (aiBehaviors)
        {
            case Behaviors.Guard:
                patrol();
                break;
            case Behaviors.Combat:
                followEnemy();
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        runBehaviors();
        GetActiveE();
        //fightplayer();
        second += Time.deltaTime;
    }
}
