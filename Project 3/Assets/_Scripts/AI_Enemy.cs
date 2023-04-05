using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.IO;
using UnityEngine.AI;

public class AI_Enemy : MonoBehaviour
{
    // group of behaviors for AI to run 
    private enum Behaviors { Guard, Combat, Seek };
    //this create a attribute of behaviors and assigns it the initial value of Guard
    Behaviors aiBehaviors = Behaviors.Guard;

    //this bool is flipped based on whether or not the player is within range of the AI Enemy
    bool isINRANGE = false;

    //this int gives the integer representation of the array value value of the current waypoint.
    int currentW = 0;

    // this is the nest waypoint for the AI to go to while patroling
    Vector3 Destination;

    //Reverse path boolean
    bool rPath = false;


    //this takes in the player as transform to then gain knowledge of the position of the
    // player comared to the enemy;
    public Transform player;

    //general distance for traversing waypoints 
    float followdistance;

    //this is the distance to determine whether or not the enemy will engage with the player
    float fightDistance;

    //this array houses all the waypoint an AI can go to. Im setting it as public because its easy and I'm 
    //referencing the example from class and it has its arraypublic
    public Transform[] waypoints;

    NavMeshAgent nav;

    /// <summary>
    /// this function will be responsible for one of the AI walking in between several waypoints. 
    /// </summary>
    void patrol()
    {
        followdistance = Vector3.Distance(gameObject.transform.position, waypoints[currentW].position);

        if (isINRANGE == false)
        {

            if (followdistance > 10f)
            {
                Destination = waypoints[currentW].position;
                nav.SetDestination(Destination);
            }
            else
            {
                if (rPath)
                {
                    if (currentW <= 0)
                    {
                        rPath = false;
                    }
                    else
                    {
                        currentW--;
                        Destination = waypoints[currentW].position;
                    }
                }
                else
                {
                    if (currentW >= waypoints.Length - 1)
                    {
                        rPath = true;
                    }
                    else
                    {
                        currentW++;
                        Destination = waypoints[currentW].position;
                    }
                }
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
    void fightplayer()
    {
        if (isINRANGE)
        {
            melee();
        }
        else
        {
            aiBehaviors = Behaviors.Guard;
        }


    }

    /// <summary>
    /// this function is responsible for looking at the player and shootinng at them
    /// </summary>
    void melee()
    {

        transform.LookAt(player.position);
        Destination = player.position;
        nav.SetDestination(Destination);

    }

    /// <summary>
    /// this function is responsible for making the enemy follow the player
    /// </summary>
    void followPlayer()
    {
        fightDistance = Vector3.Distance(gameObject.transform.position, player.position);
        if (fightDistance <= 2f)
        {
            isINRANGE = true;
           // aiBehaviors = Behaviors.Combat;
        }
        else
        {
            isINRANGE = false;
          //  aiBehaviors = Behaviors.Guard;
        }
    }

    void runBehaviors()
    {
        switch (aiBehaviors)
        {
            case Behaviors.Guard:
                patrol();
                break;
            case Behaviors.Combat:
                fightplayer();
                break;
            //case Behaviors.Seek:
            //    followPlayer();
            //    break;

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
        followPlayer();
    }
}
