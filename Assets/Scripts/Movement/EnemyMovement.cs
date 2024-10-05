using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{//https://www.youtube.com/watch?v=am3IitICcyA

    //Waypoint Handling
    public GameObject entryPattern; //This should exist in the world
    public GameObject attackPatternPrefab; //This is a prefab dependant on original position
    public Transform formationPosition;
    private int currentWaypointIndex = 0;

    private List<Transform> entryPatternWaypoints = new List<Transform>();
    private List<Transform> attackPatternWaypoints = new List<Transform>();
    private List<Transform> waypoints;

    
    public float moveSpeed;
    
    public float turnSpeed = 1000.0f;

    public enum state {
        Entering,
        Formation,
        Attacking,
        Init,
    }

    public state currentState = state.Init;

    
    public void setEntryPattern(GameObject entryPattern)
    {
        this.entryPattern = entryPattern;
        foreach (Transform child in entryPattern.transform)
        {
            entryPatternWaypoints.Add(child);
        }
        transform.position = entryPatternWaypoints[0].transform.position;
    }

    public void setAttackPattern(GameObject attackPattern)
    {
        this.attackPatternPrefab = attackPattern;
        foreach (Transform child in attackPatternPrefab.transform)
        {
            attackPatternWaypoints.Add(child);
        }
    }

    public void setFormationPosition(GameObject formationPosition)
    {
        this.formationPosition = formationPosition.transform;
    }


    // Start is called before the first frame update
    void Start()
    {
        if (entryPattern != null){
            setEntryPattern(entryPattern);
            transform.position = entryPatternWaypoints[currentWaypointIndex].transform.position;
            entryPatternWaypoints.Add(formationPosition);        
            waypoints = entryPatternWaypoints;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == state.Entering){
            EntryMovement();
        }
        if (currentState == state.Formation){
            FollowFormation();
        }
        if (currentState == state.Attacking){
            Attack();
        }
    }

    void EntryMovement()
    {
        FollowPath();
        // If the enemy has reached a close enough position to the formation position
        if (currentWaypointIndex == waypoints.Count - 1){
            if (Vector3.Distance(transform.position, formationPosition.transform.position) < 0.1f)
            {
                currentState = state.Formation;
                currentWaypointIndex = 0;
            }
            else {
                MoveToFormation();
            }
        }
    }

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

                if (transform.position == waypoints[currentWaypointIndex].transform.position)
                {
                    currentWaypointIndex += 1;
                }
            }            
        }
    }

    void MoveToFormation()
    {
        Vector3 pointTarget = formationPosition.transform.position - transform.position;
        float angle = Mathf.Atan2(pointTarget.y, pointTarget.x) * Mathf.Rad2Deg - 90f;

        // transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle)), 500f * Time.deltaTime);

        transform.position = Vector2.MoveTowards(transform.position, formationPosition.transform.position, moveSpeed * Time.deltaTime);
    }

    void FollowFormation()
    {
        transform.position = formationPosition.transform.position;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, 0)), 100f * Time.deltaTime);
    }

    void Attack()
    {
        waypoints = attackPatternWaypoints;
        FollowPath();
        
    }

    
}
