/***************************************************************
file: DamangeHandler.cs
author: Alex Mariano
class: CS 4700 – Game Development
assignment: program 1
date last modified: 10/18/2024

purpose: This program keeps track of the health of a single
GameObject, such as a player or enemy. This also adds points
to the game total and destroyes GameObjects when they die.

****************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{

    private int health = 1;
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

    //function: Die
    //purpose: Function called to execute what happens when a GameObject dies.
    //If Enemy, removes self from manifest and adds points
    //If player, asks Game Manager to respawn.
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

    //function: AddManifestReference
    //purpose: Gives the DamageHandler a refernece to the Alien Manifest
    public void AddManifestReference(GameObject manifest)
    {
        this.enemyManifest = manifest;
    }
}
