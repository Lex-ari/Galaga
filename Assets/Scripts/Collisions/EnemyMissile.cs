/***************************************************************
file: EnemyMissile.cs
author: Alex Mariano
class: CS 4700 – Game Development
assignment: program 3
date last modified: 10/18/2024

purpose: This program handles the behavior of an enemy missile
attack pattern. This includes shooting two missiles.

****************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{

    const float TIME_BETWEEN_SHOTS = 0.2f;

    public GameObject missilePrefab;
    public Vector3 missileOffset = new Vector3(0, -1f, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //function: FirePattern
    //purpose: Public visible call to call coroutine to fire twice.
    public void FirePattern()
    {
        StartCoroutine(FireTwice());
    }

    //function: FireTwice
    //purpose: Fires a missile... twice.
    IEnumerator FireTwice()
    {
        FireOnce();
        yield return new WaitForSeconds(TIME_BETWEEN_SHOTS);
        FireOnce();
    }

    //function: FireOnce
    //purpose: Instantiates a new missile
    public void FireOnce()
    {
        Instantiate(missilePrefab, transform.position + missileOffset, Quaternion.Euler(new Vector3(0, 0, 0)));
    }
}
