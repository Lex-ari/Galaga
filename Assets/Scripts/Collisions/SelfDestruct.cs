/***************************************************************
file: SelfDestruct.cs
author: Alex Mariano
class: CS 4700 – Game Development
assignment: program 3
date last modified: 10/18/2024

purpose: This program Deinstnatiates a Game Object, primarily
missiles, once they have persisted for 5 seconds. These
GameObjects should already be out of the visible frame.

****************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float aliveTime = 5.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        aliveTime -= Time.deltaTime;
        if (aliveTime <= 0) {
            Destroy(gameObject);
        }
    }
}
