using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{

    public int health = 1;
    private GameObject enemyManifest;
    public GameObject gameManager;
    private GameManager gameManagerScript;
    public int pointValue = 100;

    public Color secondaryColor;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) {
            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Hit");
        if (other.tag != "Player")
        {
            health--;
            if (secondaryColor != null)
            {
                GetComponent<SpriteRenderer>().color = secondaryColor;
            }
        }
    }

    void Die(){
        if (gameObject.tag == "Enemy")
        {
            AlienManifest manifest = enemyManifest.GetComponent<AlienManifest>();
            manifest.RemoveAlienFromManifest(gameObject);
            gameManagerScript.AddPoints(pointValue);
        }
        if (gameObject.tag == "Player")
        {
            Debug.Log("Player to Game Manager Set Respawning");
            gameManagerScript.SetRespawning();
        }
        Destroy(gameObject);
    }

    public void AddManifestReference(GameObject manifest)
    {
        this.enemyManifest = manifest;
    }
}
