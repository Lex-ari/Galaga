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
    }

    public int points = 0;
    

    public gameState currentGameState = gameState.idle;

    private Coroutine workingCoroutine = null;

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
        UpdatePointCount();
    }

    IEnumerator SpawnEnemies(){
        while (currentGameState == gameState.spawningEnemies)
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
            }
        }
    }

    IEnumerator AttackEnemies()
    {
        yield return new WaitForSeconds(2.0f);
        while (currentGameState == gameState.enemiesAttacking)
        {
            attackManagerScript.DoAnAttack();
            yield return new WaitForSeconds(5.0f);
        }
    }

    public void AddPoints(int points)
    {
        this.points += points;
    }

    private void UpdatePointCount()
    {
        score.text = points.ToString();
    }
}
