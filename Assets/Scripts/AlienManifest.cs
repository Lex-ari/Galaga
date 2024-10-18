using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienManifest : MonoBehaviour
{

    private List<GameObject> alienManifest = new List<GameObject>();
    private Queue<GameObject> recentAliens = new Queue<GameObject>();

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddAlienToManifest(GameObject alien)
    {
        alienManifest.Add(alien);
    }

    public void RemoveAlienFromManifest(GameObject alien)
    {
        bool success = alienManifest.Remove(alien);
        // Debug.Log(success);
    }



    public GameObject GetAlienFittingParamters(alienColor color, side sidePosition, int groupId)
    {
        for (int i = alienManifest.Count - 1; i >= 0; i--)
        {
            if (!IsRecentOrNotMinimum(alienManifest[i]))
            {
                EnemyMovement enemyMovement = alienManifest[i].GetComponent<EnemyMovement>();
                FormationPositionInformation posInfo = enemyMovement.GetFormationPosition().GetComponent<FormationPositionInformation>();
                if (color == posInfo.GetColor() && sidePosition == posInfo.GetSidePosition())
                {
                    if (enemyMovement.GetState() != EnemyMovement.state.attacking)
                    {
                        AddToRecentsQueue(alienManifest[i]);
                        return alienManifest[i];
                    }

                }
            }

        }
        return null;
    }

    private bool IsRecentOrNotMinimum(GameObject alien)
    {
        if (alienManifest.Count <= recentAliens.Count)
        {
            return false;
        }
        if (recentAliens.Contains(alien))
        {
            Debug.Log("returned did Contain");
            return true;
        }
        return false;
    }

    private void AddToRecentsQueue(GameObject alien)
    {
        Debug.Log("add to recentAliens");
        recentAliens.Enqueue(alien);
        if (recentAliens.Count >= 10)
        {
            Debug.Log("removed from recentAliens");
            recentAliens.Dequeue();
        }
    }
    
}
