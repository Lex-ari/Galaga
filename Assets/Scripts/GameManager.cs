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

    IEnumerator AttackEnemies()
    {
        yield return new WaitForSeconds(2.0f);
        attackManagerScript.DoAnAttack();
        yield return new WaitForSeconds(5.0f);
        workingCoroutine = null;
    }

    public void AddPoints(int points)
    {
        this.points += points;
    }

    private void UpdatePointCount()
    {
        score.text = points.ToString();
    }

    public void SetRespawning()
    {
        currentGameState = gameState.respawn;
        StartCoroutine(RespawnHandler());
    }
    
    IEnumerator RespawnHandler()
    {
        yield return new WaitForSeconds(5.0f);
        if (lives > 0)
        {
            Debug.Log("Respawn Handler Spawning Player");
            GameObject player = spawnPlayer();
            StartCoroutine(StartStageProcess(player));
            lives -= 1;
        }
        else 
        {
            // gameover
        }
    }

    GameObject spawnPlayer()
    {
        GameObject player = Instantiate(playerShip, playerStartPosition, Quaternion.Euler(new Vector3(0, 0, 0)));
        return player;
    }

    IEnumerator StartStageProcess(GameObject player)
    {
        Debug.Log("Doing Stage Process");
        centerStage.enabled = true;
        centerStage.text = ("READY");
        yield return new WaitForSeconds(2.0f);
        centerStage.enabled = false;
        PlayerMovement playerMovementScript = player.GetComponent<PlayerMovement>();
        playerMovementScript.SetMovementProperty(true);
        currentGameState = holdDeathGameState;
    }
}
