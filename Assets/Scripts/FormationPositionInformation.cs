using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationPositionInformation : MonoBehaviour
{
    public int popUpGroup;
    
    public enum side {
        left,
        right,
    }
    public side sidePosition;

    public enum color {
        green,
        yellow,
        red,    
    }

    public color colorPosition;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getPopUpGroup(){
        return popUpGroup;
    }

    public side getSidePosition(){
        return sidePosition;
    }

    public color getColor(){
        return colorPosition;
    }
}
