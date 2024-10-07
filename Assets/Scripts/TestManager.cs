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

    EnemyMovement alienScript;


    // Start is called before the first frame update
    void Start()
    {
        alienScript = alien.GetComponent<EnemyMovement>();
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
            
            alienScript.Attack();
            StartCoroutine(AlienFire());
            currentState = state.Init;
        }
    }

    IEnumerator AlienFire()
    {
        yield return new WaitForSeconds(0.5f);
        EnemyMissile alienMissileScript = alien.GetComponent<EnemyMissile>();
        alienMissileScript.FireOnce();
    }
}
