using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TestManager : MonoBehaviour
{

    public GameObject alien;

    public enum state {
        Entering,
        Formation,
        Attacking,
        Init,
    }

    public state currentState = state.Init;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == state.Entering){
            // alien.currentState = state.Entering;
        }
        if (currentState == state.Formation){
            // alien.currentState = state.Formation;
        }
        if (currentState == state.Attacking){
            EnemyMovement alienScript = alien.GetComponent<EnemyMovement>();
            alienScript.Attack();
            currentState = state.Init;
        }
    }
}
