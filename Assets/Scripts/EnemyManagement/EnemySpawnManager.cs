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

    public GameObject entryMiddleR;
    public GameObject entryMiddleL;
    public GameObject entryLowerR;
    public GameObject entryLowerL;
    public GameObject alienManifest;
    
    private AlienManifest alienManifestScript;
    public int stage = 0;

    private List<GameObject> spawnedEnemies = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        alienManifestScript = alienManifest.GetComponent<AlienManifest>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stage != 0)
        {
            spawnPopUpGroup();
            stage = 0;
        }
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
    void spawnPopUpGroup(){
        List<Transform> stagePositions = getFormationTransformsFromStage(stage);
        if (stage == 1)
        {
            List<alienColor> stage1RedGroup = new List<alienColor>{
                alienColor.red,
                alienColor.red,
                alienColor.red,
                alienColor.red,
            };
            List<alienColor> stage1YellowGroup = new List<alienColor>{
                alienColor.yellow,
                alienColor.yellow,
                alienColor.yellow,
                alienColor.yellow,
            };
            List<Transform> stage1Positions = splitByColor(stagePositions, alienColor.red);
            List<Transform> stage1RedPositions = stage1Positions.GetRange(0, 4);
            List<Transform> stage1YellowPositions = stage1Positions.GetRange(4, 4);
            StartCoroutine(enemyLocalGroupSpawner(stage1RedGroup, stage1RedPositions, entryMiddleL, spawnPointHigh));
            StartCoroutine(enemyLocalGroupSpawner(stage1YellowGroup, stage1YellowPositions, entryMiddleR, spawnPointHigh));
        }
        else if (stage == 2)
        {
            List<alienColor> stage2OrderGroup = new List<alienColor>{
                alienColor.green,
                alienColor.red,
                alienColor.green,
                alienColor.red,
                alienColor.green,
                alienColor.red,
                alienColor.green,
                alienColor.red,
            };
            List<Transform> stage2Positions = alternateByColor(stagePositions, alienColor.green);
            StartCoroutine(enemyLocalGroupSpawner(stage2OrderGroup, stage2Positions, entryLowerL, spawnPointLeft));
        }
        else if (stage == 3)
        {
            List<alienColor> stage3OrderGroup = new List<alienColor>{
                alienColor.red,
                alienColor.red,
                alienColor.red,
                alienColor.red,
                alienColor.red,
                alienColor.red,
                alienColor.red,
                alienColor.red,
            };
            StartCoroutine(enemyLocalGroupSpawner(stage3OrderGroup, stagePositions, entryLowerR, spawnPointRight));
        }
        else if (stage == 4)
        {
            List<alienColor> stage4OrderGroup = new List<alienColor>{
                alienColor.yellow,
                alienColor.yellow,
                alienColor.yellow,
                alienColor.yellow,
                alienColor.yellow,
                alienColor.yellow,
                alienColor.yellow,
                alienColor.yellow,
            };
            StartCoroutine(enemyLocalGroupSpawner(stage4OrderGroup, stagePositions, entryMiddleR, spawnPointHigh));
        }
        else if (stage == 5)
        {
            List<alienColor> stage4OrderGroup = new List<alienColor>{
                alienColor.yellow,
                alienColor.yellow,
                alienColor.yellow,
                alienColor.yellow,
                alienColor.yellow,
                alienColor.yellow,
                alienColor.yellow,
                alienColor.yellow,
            };
            StartCoroutine(enemyLocalGroupSpawner(stage4OrderGroup, stagePositions, entryMiddleL, spawnPointHigh));
        }
        stage++;
    }

    IEnumerator enemyLocalGroupSpawner(List<alienColor> enemyColorOrder, List<Transform> enemyFormationPositions, GameObject entryPoint, GameObject spawnPoint)
    {
        List<Transform> stagingPositions = new List<Transform>(enemyFormationPositions);
        
        if (enemyColorOrder.Count == enemyFormationPositions.Count)
        {
            for (int i = 0; i < enemyColorOrder.Count; i++)
            {
                GameObject newAlien = instantiateEnemy(enemyColorOrder[i], entryPoint, enemyFormationPositions[i].gameObject, spawnPoint);
                EnemyMovement enemyScript = newAlien.GetComponent<EnemyMovement>();
                enemyScript.setState("Entering");
                alienManifestScript.addAlienToManifest(newAlien);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    List<Transform> getFormationTransformsFromStage(int stage)
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

    GameObject instantiateEnemy(alienColor color, GameObject entryPattern, GameObject formationPosition, GameObject spawnPoint)
    {
        GameObject newAlien;
        if (color == alienColor.yellow)
        {
            newAlien = Instantiate(yelAlienPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);

        }         
        else if (color == alienColor.red)
        {
            newAlien = Instantiate(redAlienPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }   
        else if (color == alienColor.green)
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

    List<Transform> splitByColor(List<Transform> positions, alienColor splitColor)
    {
        List<Transform> colorOne = new List<Transform>();
        List<Transform> colorTwo = new List<Transform>();
        foreach (Transform transform in positions)
        {
            FormationPositionInformation posInfo = transform.GetComponent<FormationPositionInformation>();
            if (splitColor == posInfo.getColor())
            {
                colorOne.Add(transform);
            }
            else
            {
                colorTwo.Add(transform);
            }
        }
        foreach (Transform transform in colorTwo)
        {
            colorOne.Add(transform);
        }
        return colorOne;
    }

    //Assumes that there is equal number of two colors in positions
    List<Transform> alternateByColor(List<Transform> positions, alienColor splitColor)
    {
        List<Transform> colorOne = new List<Transform>();
        List<Transform> colorTwo = new List<Transform>();
        foreach (Transform transform in positions)
        {
            FormationPositionInformation posInfo = transform.GetComponent<FormationPositionInformation>();
            if (splitColor == posInfo.getColor())
            {
                colorOne.Add(transform);
            }
            else
            {
                colorTwo.Add(transform);
            }
        }
        List<Transform> alternateColor = new List<Transform>();
        for (int i = 0; i < colorOne.Count; i++)
        {
            alternateColor.Add(colorOne[i]);
            alternateColor.Add(colorTwo[i]);
        }
        return alternateColor;
    }
}
