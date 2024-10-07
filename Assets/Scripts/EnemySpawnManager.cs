using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{

    public GameObject redAlienPrefab;
    public GameObject yelAlienPrefab;
    public GameObject greAlienPrefab;

    public GameObject spawnPointHigh;
    public GameObject spawnPointLeft;
    public GameObject spawnPointRight;

    public GameObject formation;

    public GameObject EntryMiddleR;
    public GameObject EntryMiddleL;
    public GameObject EntryLowerR;
    public GameObject EntryLowerL;




    public int stage = 1;



    // Start is called before the first frame update
    void Start()
    {
        SpawnPopUpGroup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    // State 1:
        // 4 Yellow from Upper Right
        // 4 Red from Upper Left
    // Stage 2
        // Green + Red Combo, 8 total, from Lower Left Side
    // Stage 3:
        // 8 Red, Lower Right Side
    // Stage 4:
        // 8 Yellow from Upper Rgiht
    // Stage 5:
        // 8 Yellow from Upper Left
    void SpawnPopUpGroup(){
        List<Transform> currentStagePositions = GetFormationTransformsFromStage(stage);
        if (stage == 1)
        {
            List<GameObject> stage1YellowGroup = new List<GameObject>();
            List<GameObject> stage1RedGroup = new List<GameObject>();
            foreach (Transform position in currentStagePositions)
            {
                FormationPositionInformation posInfo = position.GetComponent<FormationPositionInformation>();
                GameObject newAlien;
                if (posInfo.getColor() == FormationPositionInformation.color.yellow)
                {
                    newAlien = InstantiateEnemy("Yellow", EntryMiddleL, position.gameObject, spawnPointHigh);
                    stage1YellowGroup.Add(newAlien);
                }
                else 
                {
                    newAlien = InstantiateEnemy("Red", EntryMiddleR, position.gameObject, spawnPointHigh);
                    stage1RedGroup.Add(newAlien);
                }
            }
            StartCoroutine(EnemyLocalGroupSpawner(stage1RedGroup));
            StartCoroutine(EnemyLocalGroupSpawner(stage1YellowGroup));
        }
        else if (stage == 2)
        {
            List<GameObject> stage2OrderGroup = new List<GameObject>();
            List<Transform> stagePositionsCopy = new List<Transform>(currentStagePositions);

        }
        else if (stage == 3)
        {

        }
        else if (stage == 4)
        {

        }
        else if (stage == 5)
        {

        }

        stage++;
    }

    IEnumerator EnemyLocalGroupSpawner(List<GameObject> enemyPopUpGroups)
    {
        foreach (GameObject enemy in enemyPopUpGroups)
        {
            EnemyMovement enemyScript = enemy.GetComponent<EnemyMovement>();
            enemyScript.setState("Entering");
            yield return new WaitForSeconds(0.2f);
        }
        
    }

    List<Transform> GetFormationTransformsFromStage(int stage)
    {
        List<Transform> currentStagePositions = new List<Transform>();
        foreach(Transform child in formation.transform)
        {
            FormationPositionInformation posInfo = child.GetComponent<FormationPositionInformation>();
            if (posInfo.getPopUpGroup() == stage)
            {
                currentStagePositions.Add(child);
            }
        }

        return currentStagePositions;
    }

    GameObject InstantiateEnemy(string color, GameObject entryPattern, GameObject formationPosition, GameObject spawnPoint)
    {
        GameObject newAlien;
        if (color == "Yellow")
        {
            newAlien = Instantiate(yelAlienPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);

        }         
        else if (color == "Red")
        {
            newAlien = Instantiate(redAlienPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }   
        else if (color == "Green")
        {
            newAlien = Instantiate(greAlienPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        } 
        else 
        {
            return null;
        }
        EnemyMovement newAlienMovement = newAlien.GetComponent<EnemyMovement>();
        newAlienMovement.setEntryPattern(entryPattern);
        newAlienMovement.setFormationPosition(formationPosition);
        return newAlien;
    }
}
