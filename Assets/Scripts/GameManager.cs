using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject attackManager;
    public GameObject enemySpawnManager;
    public GameObject enemyManifest;
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

    public gameState currentGameState = gameState.idle;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
