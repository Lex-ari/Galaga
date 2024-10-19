/***************************************************************
file: EnemySpawnManager.cs
author: Alex Mariano
class: CS 4700 – Game Development
assignment: program 3
date last modified: 10/18/2024

purpose: This program manages the spawning and entry of
different waves of enemies.

****************************************************************/

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
    public int stage = 1;

    private List<GameObject> spawnedEnemies = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        alienManifestScript = alienManifest.GetComponent<AlienManifest>();
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

    //function: SpawnPopUpGroup
    //purpose: Lists out different configuration of enemies for each wave and
    //cycles through them when called.
    public bool SpawnPopUpGroup(){
        List<Transform> stagePositions = GetFormationTransformsFromStage(stage);
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
            List<Transform> stage1Positions = SplitByColor(stagePositions, alienColor.red);
            List<Transform> stage1RedPositions = stage1Positions.GetRange(0, 4);
            List<Transform> stage1YellowPositions = stage1Positions.GetRange(4, 4);
            StartCoroutine(EnemyLocalGroupSpawner(stage1RedGroup, stage1RedPositions, entryMiddleL, spawnPointHigh));
            StartCoroutine(EnemyLocalGroupSpawner(stage1YellowGroup, stage1YellowPositions, entryMiddleR, spawnPointHigh));
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
            List<Transform> stage2Positions = AlternateByColor(stagePositions, alienColor.green);
            StartCoroutine(EnemyLocalGroupSpawner(stage2OrderGroup, stage2Positions, entryLowerL, spawnPointLeft));
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
            StartCoroutine(EnemyLocalGroupSpawner(stage3OrderGroup, stagePositions, entryLowerR, spawnPointRight));
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
            StartCoroutine(EnemyLocalGroupSpawner(stage4OrderGroup, stagePositions, entryMiddleR, spawnPointHigh));
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
            StartCoroutine(EnemyLocalGroupSpawner(stage4OrderGroup, stagePositions, entryMiddleL, spawnPointHigh));
        }
        else
        {
            return false;
        }
        stage++;
        return true;
    }

    //function: EnemyLocalGroupSpawner
    //purpose: This retrieves the order of enemies to instantiate and will
    //set them up for their requested entry pattern.
    IEnumerator EnemyLocalGroupSpawner(List<alienColor> enemyColorOrder, List<Transform> enemyFormationPositions, GameObject entryPoint, GameObject spawnPoint)
    {
        List<Transform> stagingPositions = new List<Transform>(enemyFormationPositions);
        
        if (enemyColorOrder.Count == enemyFormationPositions.Count)
        {
            for (int i = 0; i < enemyColorOrder.Count; i++)
            {
                GameObject newAlien = InstantiateEnemy(enemyColorOrder[i], entryPoint, enemyFormationPositions[i].gameObject, spawnPoint);
                EnemyMovement enemyScript = newAlien.GetComponent<EnemyMovement>();
                enemyScript.SetState("entering");
                alienManifestScript.AddAlienToManifest(newAlien);
                DamageHandler alienDamageHandlerScript = newAlien.GetComponent<DamageHandler>();
                alienDamageHandlerScript.AddManifestReference(alienManifest);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    //function: GetFormationTransformsFromStage
    //purpose: This appends all alien formations in the current scene into
    //a list. This will return the proper positions for a given stage.
    List<Transform> GetFormationTransformsFromStage(int stage)
    {
        List<Transform> currentStagePositions = new List<Transform>();
        foreach(Transform child in formation.transform)
        {
            FormationPositionInformation posInfo = child.GetComponent<FormationPositionInformation>();
            if (posInfo.GetPopUpGroup() == stage)
            {
                currentStagePositions.Add(child);
            }
        }

        return currentStagePositions;
    }

    //function: InstantiateEnemy
    //purpose: Instantiates an enemy based on color, pattern, position, and spawn.
    GameObject InstantiateEnemy(alienColor color, GameObject entryPattern, GameObject formationPosition, GameObject spawnPoint)
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
        newAlienMovement.SetEntryPattern(entryPattern);
        newAlienMovement.SetFormationPosition(formationPosition);
        return newAlien;
    }

    //function: SplitByColor
    //purpose: Takes in a List<> of positions and returns a sorted list
    //where the first half is the split color.
    List<Transform> SplitByColor(List<Transform> positions, alienColor splitColor)
    {
        List<Transform> colorOne = new List<Transform>();
        List<Transform> colorTwo = new List<Transform>();
        foreach (Transform transform in positions)
        {
            FormationPositionInformation posInfo = transform.GetComponent<FormationPositionInformation>();
            if (splitColor == posInfo.GetColor())
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
    //function: AlternateByColor
    //purpose: Takes in a list<> of positions and returns a sorted list
    //where every other index of positions is the requested color.
    List<Transform> AlternateByColor(List<Transform> positions, alienColor splitColor)
    {
        List<Transform> colorOne = new List<Transform>();
        List<Transform> colorTwo = new List<Transform>();
        foreach (Transform transform in positions)
        {
            FormationPositionInformation posInfo = transform.GetComponent<FormationPositionInformation>();
            if (splitColor == posInfo.GetColor())
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
