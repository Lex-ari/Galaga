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

    public int getPopUpGroup(){
        return popUpGroup;
    }

    public side getSidePosition(){
        return sidePosition;
    }

    public alienColor getColor(){
        return colorPosition;
    }
}
