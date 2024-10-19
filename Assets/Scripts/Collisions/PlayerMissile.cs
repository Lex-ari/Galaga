/***************************************************************
file: PlayerMissile.cs
author: Alex Mariano
class: CS 4700 – Game Development
assignment: program 3
date last modified: 10/18/2024

purpose: This program manages the behavior of the player's
attack pattern.

****************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMissile : MonoBehaviour
{
    const float TIME_BETWEEN_SHOTS = 0.2f;
    const float FIRE_DELAY = 1f;
    float cooldownTimer = 0.0f;
    float cooldownTimer2 = 0.0f;

    public GameObject missilePrefab;
    public Vector3 missileOffset = new Vector3(0, 1f, 0);

    bool missileEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        cooldownTimer2 -= Time.deltaTime;
        if (Input.GetButton("Fire1") && missileEnabled){
            if (cooldownTimer <= 0){
                cooldownTimer = FIRE_DELAY;
                cooldownTimer2 = TIME_BETWEEN_SHOTS;

                Instantiate(missilePrefab, transform.position + missileOffset, transform.rotation);
            } 
            else if (cooldownTimer2 <= 0){
                cooldownTimer2 = FIRE_DELAY;
                Instantiate(missilePrefab, transform.position + missileOffset, transform.rotation);
            }
            else {
                // Do nothing, wait for cooldown
            }
        }
    }

    public void SetArmLockProperty(bool value)
    {
        missileEnabled = value;
    }
}
