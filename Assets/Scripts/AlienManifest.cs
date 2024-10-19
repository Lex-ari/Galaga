/***************************************************************
file: AlienManifest.cs
author: Alex Mariano
class: CS 4700 – Game Development
assignment: program 1
date last modified: 10/18/2024

purpose: This program keeps track of all aliens that are present
in the scene. Allows other functions to retrieve an alien
object available.

****************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienManifest : MonoBehaviour
{

    private List<GameObject> alienManifest = new List<GameObject>();
    private Queue<GameObject> recentAliens = new Queue<GameObject>();

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //function: AddAlienToManifest
    //purpose: Adds a GameObject to the full alienManifest list
    public void AddAlienToManifest(GameObject alien)
    {
        alienManifest.Add(alien);
    }

    //function: RemoveAlienFromManifest
    //purpose: Removes an alien GameObject from the alienManifest list
    public void RemoveAlienFromManifest(GameObject alien)
    {
        bool success = alienManifest.Remove(alien);
        // Debug.Log(success);
    }

    //function: GetAlienFittingParamters
    //purpose: Returns an Alien from the alienManifest list that has the requested
    //attributes such as color, side, and group id.
    public GameObject GetAlienFittingParamters(alienColor color, side sidePosition, int groupId)
    {
        for (int i = alienManifest.Count - 1; i >= 0; i--)
        {
            if (!IsRecentOrNotMinimum(alienManifest[i]))
            {
                EnemyMovement enemyMovement = alienManifest[i].GetComponent<EnemyMovement>();
                FormationPositionInformation posInfo = enemyMovement.GetFormationPosition().GetComponent<FormationPositionInformation>();
                if (color == posInfo.GetColor() && sidePosition == posInfo.GetSidePosition())
                {
                    if (enemyMovement.GetState() != EnemyMovement.state.attacking)
                    {
                        AddToRecentsQueue(alienManifest[i]);
                        return alienManifest[i];
                    }

                }
            }

        }
        return null;
    }

    //function: GetAliensFittingGroupId
    //purpose: Gets aliens that are part of a group attack.
    public List<GameObject> GetAliensFittingGroupId(int groupId)
    {
        List<GameObject> groupAttackers = new List<GameObject>();
        for (int i = alienManifest.Count - 1; i >= 0; i--)
        {
            EnemyMovement enemyMovement = alienManifest[i].GetComponent<EnemyMovement>();
            FormationPositionInformation posInfo = enemyMovement.GetFormationPosition().GetComponent<FormationPositionInformation>();
            if (posInfo.GetGroupAttackId() == groupId)
            {
                if (enemyMovement.GetState() != EnemyMovement.state.attacking)
                {
                    groupAttackers.Add(alienManifest[i]);
                }

            }           
        }
        return groupAttackers;
    }

    //function: IsRecentOrNotMinimum
    //purpose: A check to see if an alien object has been recently used.
    //This is to avoid the same alien attacking and allow new ones to perform.
    //If there are fewer alive aliens in the scene, then first alien called
    //gets priority.
    private bool IsRecentOrNotMinimum(GameObject alien)
    {
        if (alienManifest.Count <= recentAliens.Count)
        {
            return false;
        }
        if (recentAliens.Contains(alien))
        {
            Debug.Log("returned did Contain");
            return true;
        }
        return false;
    }

    //function: AddToRecentsQueue
    //purpose: Manages the Recent Queue.
    //Adds an Alien to the Recent Queue and dequeues when it is "full", to allow
    //an alien that "rested" for a while to attack again.
    private void AddToRecentsQueue(GameObject alien)
    {
        Debug.Log("add to recentAliens");
        recentAliens.Enqueue(alien);
        if (recentAliens.Count >= 10)
        {
            Debug.Log("removed from recentAliens");
            recentAliens.Dequeue();
        }
    }
    
    public int GetNumberOfAliveAliens()
    {
        return alienManifest.Count;
    }
    
}
