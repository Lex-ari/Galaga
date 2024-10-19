/***************************************************************
file: EnemyMovement.cs
author: Alex Mariano
class: CS 4700 – Game Development
assignment: program 3
date last modified: 10/18/2024

purpose: This program defines the behavior for any given
enemy, which includes multiple states mainly entering,
formation, and attacking.

****************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    //Waypoint Handling
    private GameObject entryPattern; //This should exist in the world
    private GameObject attackPatternPrefab; //This is a prefab dependant on original position
    private GameObject attackPatternInstance;
    private GameObject formationPosition;
    private int currentWaypointIndex = 0;

    private List<Transform> entryPatternWaypoints = new List<Transform>();
    private List<Transform> attackPatternWaypoints = new List<Transform>();
    private List<Transform> waypoints;

    
    public float moveSpeed;
    
    public float turnSpeed = 1000.0f;

    public enum state {
        entering,
        formation,
        attacking,
        init,
    }

    public state currentState = state.init;

    //function:  SetEntryPattern
    //purpose: Sets the Alien's entry pattern from offscreen to play area.
    public void SetEntryPattern(GameObject entryPattern)
    {
        this.entryPattern = entryPattern;
        foreach (Transform child in entryPattern.transform)
        {
            entryPatternWaypoints.Add(child);
        }
        // transform.position = entryPatternWaypoints[0].transform.position;
    }

    //function: SetAttackPattern
    //purpose: Sets Alien's Attack Pattern
    public void SetAttackPattern(GameObject attackPattern)
    {
        this.attackPatternPrefab = attackPattern;

    }

    //function: SetFormationPosition
    //purpose: Set Alien's Formation Position
    public void SetFormationPosition(GameObject formationPosition)
    {
        this.formationPosition = formationPosition;
    }

    //function: GetFormationPosition
    //purpose: Gets Alien's Formation Position
    public GameObject GetFormationPosition()
    {
        return this.formationPosition;
    }

    //fucntion: SetState
    //purpose: Parses a String and sets the current state of the alien to one
    //of the matching string states.
    public void SetState(string newState)
    {
        if (newState == "entering")
        {
            currentState = state.entering;
        }
        if (newState == "formation")
        {
            currentState = state.formation;
        }
        if (newState == "ttacking")
        {
            currentState = state.attacking;
        }
    }

    public state GetState()
    {
        return currentState;
    }


    // Start is called before the first frame update
    void Start()
    {
        // if (entryPattern != null){
        //     setEntryPattern(entryPattern);
        //     transform.position = entryPatternWaypoints[currentWaypointIndex].transform.position;    
        //     waypoints = entryPatternWaypoints;
        // }
        // if (attackPatternPrefab != null){
        //     setAttackPattern(attackPatternPrefab);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == state.entering){
            EntryMovement();
        }
        if (currentState == state.formation){
            FollowFormation();
        }
        if (currentState == state.attacking){
            AttackMovement();
        }
    }

    //function: Enter
    //purpose: Public Visible function to set entering state
    public void Enter(){
        currentState = state.entering;
    }

    //function: EntryMovement
    //purpose: Loads waypoints and commands to follow path and afterwards
    //follow formation. 
    // Wrapper for FollowPath to do state.formation afterwards.
    void EntryMovement()
    {
        waypoints = entryPatternWaypoints;
        FollowPath();
        //If reached endpoint
        if (currentWaypointIndex == waypoints.Count){
            //done following
            currentState = state.formation;
        }
    }

    //function: FollowPath
    //purpose: Moves the GameObject to each waypoint in waypoints.
    // This handles translation and rotation 
    void FollowPath()
    {
        if (waypoints != null){
            if (currentWaypointIndex <= waypoints.Count - 1)
            {
                Vector3 pointTarget = waypoints[currentWaypointIndex].transform.position - transform.position;
                float angle = Mathf.Atan2(pointTarget.y, pointTarget.x) * Mathf.Rad2Deg - 90f;

                // transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle)), 500f * Time.deltaTime);

                transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) <= 0.1f)
                {
                    currentWaypointIndex += 1;
                }
            }            
        }
    }

    //function: MoveToFormation
    //purpose: Speedy slew to formation, meant to be after completing path
    void MoveToFormation()
    {
        Vector3 pointTarget = formationPosition.transform.position - transform.position;
        float angle = Mathf.Atan2(pointTarget.y, pointTarget.x) * Mathf.Rad2Deg - 90f;

        // transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle)), 500f * Time.deltaTime);

        transform.position = Vector2.MoveTowards(transform.position, formationPosition.transform.position, moveSpeed * Time.deltaTime);
    }

    //function: FollowFormation
    //purpose: Slow slew to formation, handles when current gameobject
    //transform and formation transform distance is negligble but not 0.
    void FollowFormation()
    {       
        currentWaypointIndex = 0;     
        if (Vector3.Distance(transform.position, formationPosition.transform.position) > 0.1f)
        {
            MoveToFormation();
        }
        else 
        {
            transform.position = formationPosition.transform.position;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, 0)), 100f * Time.deltaTime);
        }

    }

    //function: Attack
    //purpose: Sets up Attack Pattern and follows pattern, and attack by
    //missiles.
    public void Attack()
    {
        attackPatternInstance = Instantiate(attackPatternPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        attackPatternWaypoints = new List<Transform>();
        foreach (Transform child in attackPatternInstance.transform)
        {
            attackPatternWaypoints.Add(child);
        }
        currentState = state.attacking;
    }

    //function: AttackMovement
    //purpose: Wrapper for FollowPath to handle Alien off-screen movement
    void AttackMovement()
    {
        waypoints = attackPatternWaypoints;
        FollowPath();
        if (currentWaypointIndex == waypoints.Count)
        {
            //If path ends outside of map, jump back up
            if (transform.position.y <= -5.0f )
            {
                transform.position = formationPosition.transform.position + new Vector3(0, 3, 0);
            }
            
            currentState = state.formation;
            Destroy(attackPatternInstance);
            attackPatternInstance = null;
        }
    }

    
}
