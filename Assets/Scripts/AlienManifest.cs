using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienManifest : MonoBehaviour
{

    private List<GameObject> alienManifest = new List<GameObject>();

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addAlienToManifest(GameObject alien)
    {
        alienManifest.Add(alien);
    }

    public void removeAlienFromManifest(GameObject alien)
    {
        alienManifest.Remove(alien);
    }



    public GameObject getAlienFittingParamters(alienColor color, side sidePosition, int groupId)
    {
        for (int i = alienManifest.Count - 1; i >= 0; i--)
        {
            FormationPositionInformation posInfo = alienManifest[i].GetComponent<EnemyMovement>().getFormationPosition().GetComponent<FormationPositionInformation>();
            if (color == posInfo.getColor() && sidePosition == posInfo.getSidePosition())
            {
                return alienManifest[i];
            }
        }
        return null;
    }
    
}
