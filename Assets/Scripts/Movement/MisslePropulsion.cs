/***************************************************************
file: MisslePropulsion.cs
author: Alex Mariano
class: CS 4700 – Game Development
assignment: program 1
date last modified: 10/18/2024

purpose: This program defines the movement behavior for a 
missile.

****************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisslePropulsion : MonoBehaviour
{

    public float maxSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 velocity = new Vector3(0, maxSpeed * Time.deltaTime, 0);
        pos += velocity;
        transform.position = pos;
    }
}
