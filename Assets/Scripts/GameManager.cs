/***************************************************************
file: GameManager.cs
author: Alex Mariano
class: CS 4700 – Game Development
assignment: program 1
date last modified: 10/18/2024

purpose: Serves as the Game Manager for the Game Scene.
This chooses what actions / states the Aliens work in.
Will call the Aliens to "enter" the scene, do their formation
and do attack patterns.
Manages Player Behavior and death conditions such as points.

****************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{

    public GameObject attackManager;
    public GameObject enemySpawnManager;
    public GameObject enemyManifest;
    public GameObject formationPositions;
    public TMP_Text score;
    public TMP_Text centerStage;
    public GameObject playerShip;

    public Vector3 playerStartPosition;


    private EnemyPositionBreathing formationPositionsScript;
    private AttackManager attackManagerScript;
    private EnemySpawnManager enemySpawnManagerScript;
    private AlienManifest alienManifestScript;

    public static GameManager instance = null;
    public enum gameState {
        menu,
        spawningEnemies,
        enemiesAttacking,
        respawn,
        idle,
        gameOver,
    }

    public int points = 0;
    

    public gameState currentGameState = gameState.idle;

    private gameState holdDeathGameState;

    private Coroutine workingCoroutine = null;

    public int lives = 3;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        enemySpawnManagerScript = enemySpawnManager.GetComponent<EnemySpawnManager>();
        attackManagerScript = attackManager.GetComponent<AttackManager>();
        alienManifestScript = enemyManifest.GetComponent<AlienManifest>();
        formationPositionsScript = formationPositions.GetComponent<EnemyPositionBreathing>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentGameState == gameState.spawningEnemies && workingCoroutine == null)
        {
            workingCoroutine = StartCoroutine(SpawnEnemies());
        }
        if (currentGameState == gameState.enemiesAttacking && workingCoroutine == null)
        {
            workingCoroutine = StartCoroutine(AttackEnemies());
        }
        if (currentGameState != gameState.respawn)
        {
            holdDeathGameState = currentGameState;
        }
        UpdatePointCount();
    }

    //function: SpawnEnemies
    //purpose: Calls upon the EnemySpawnManager to send a single popup group
    //until all popup groups are in formation.
    //In the event that the player is killed before the whole group is spawned,
    //This will wait until the game is in the "play" state and will continue
    //its recent spawn popup pattern.
    IEnumerator SpawnEnemies(){
        bool complete = false;
        while (!complete)
        {
            if (currentGameState == gameState.spawningEnemies)
            {
                bool spawnSuccess = enemySpawnManagerScript.SpawnPopUpGroup();
                // Debug.Log("spawnEnemies");
                if (spawnSuccess)
                {
                    yield return new WaitForSeconds(4.5f);
                }
                else
                {
                    currentGameState = gameState.enemiesAttacking;
                    formationPositionsScript.setFormationIdle();
                    workingCoroutine = null;
                    break;
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    //function: AttackEnemies
    //purpose: This pushes the AttackManager to do an Attack.
    IEnumerator AttackEnemies()
    {
        yield return new WaitForSeconds(2.0f);
        attackManagerScript.DoAnAttack();
        yield return new WaitForSeconds(5.0f);
        workingCoroutine = null;
    }

    //function: AddPoints
    //purpose: Adds to the current player point count
    public void AddPoints(int points)
    {
        this.points += points;
    }

    //function: UpdatePointCount
    //purpose: Updates the text on the HUD to the player point count
    private void UpdatePointCount()
    {
        score.text = points.ToString();
    }

    //function: SetRespawning
    //purpose: Public visible function to allow the Player gameobject to
    //request a Respawn. Sets the gameState to respawn.
    public void SetRespawning()
    {
        currentGameState = gameState.respawn;
        StartCoroutine(RespawnHandler());
    }

    //function: RespawnHandler
    //purpose: This waits for the scene to "clean up" and do a respawn. 
    IEnumerator RespawnHandler()
    {
        yield return new WaitForSeconds(5.0f);
        if (lives > 0)
        {
            // Debug.Log("Respawn Handler Spawning Player");
            GameObject player = SpawnPlayer();
            StartCoroutine(StartStageProcess(player));
            lives -= 1;
        }
        else 
        {
            // gameover
        }
    }

    //fucntion: SpawnPlayer
    //purpose: Spawns the player into the scene.
    GameObject SpawnPlayer()
    {
        GameObject player = Instantiate(playerShip, playerStartPosition, Quaternion.Euler(new Vector3(0, 0, 0)));
        return player;
    }

    //function: StartStageProcess
    //purpose: Staging process for when a new game / respawn happens.
    IEnumerator StartStageProcess(GameObject player)
    {
        // Debug.Log("Doing Stage Process");
        centerStage.enabled = true;
        centerStage.text = ("READY");
        yield return new WaitForSeconds(2.0f);
        centerStage.enabled = false;
        PlayerMovement playerMovementScript = player.GetComponent<PlayerMovement>();
        playerMovementScript.SetMovementProperty(true);
        currentGameState = holdDeathGameState;
    }
}
