/***************************************************************
file: PlayerMovement.cs
author: Alex Mariano
class: CS 4700 – Game Development
assignment: program 1
date last modified: 10/18/2024

purpose: This program defines the behavior for the movement of
the player. Allows the player to move left and right.

****************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    float maxSpeed = 3.5f;
    float boundry = 3.5f;

    bool movementEnabled = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movementEnabled)
        {
            Vector3 pos = transform.position;
            pos.x += Input.GetAxis("Horizontal") * maxSpeed * Time.deltaTime;

            
            if (pos.x > boundry) {
                pos.x = boundry;
            }
            if (pos.x < -boundry) {
                pos.x = -boundry;
            }
            transform.position = pos;
        }

    }

    //function: SetMovementProperty
    //purpose: Enables / Disables the player movement
    public void SetMovementProperty(bool value)
    {
        movementEnabled = value;
    }

}
