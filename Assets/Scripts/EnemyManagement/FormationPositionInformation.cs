/***************************************************************
file: FormationPositionInformation.cs
author: Alex Mariano
class: CS 4700 – Game Development
assignment: program 1
date last modified: 10/18/2024

purpose: This program serves as general values for an alien
position.

****************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum alienColor {
    green,
    yellow,
    red,    
    none,
}    
public enum side {
    left,
    right,
}

public class FormationPositionInformation : MonoBehaviour
{
    public int popUpGroup;
    

    public side sidePosition;

 

    public alienColor colorPosition;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetPopUpGroup(){
        return popUpGroup;
    }

    public side GetSidePosition(){
        return sidePosition;
    }

    public alienColor GetColor(){
        return colorPosition;
    }
}
