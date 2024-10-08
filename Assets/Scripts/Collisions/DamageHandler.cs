using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{

    public int health = 1;
    private GameObject enemyManifest;

    // Start is called before the first frame update
    void Start()
    {
        
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
        }
    }

    void Die(){
        if (gameObject.tag == "Enemy")
        {
            AlienManifest manifest = enemyManifest.GetComponent<AlienManifest>();
            manifest.RemoveAlienFromManifest(gameObject);
        }
        Destroy(gameObject);
    }

    public void AddManifestReference(GameObject manifest)
    {
        this.enemyManifest = manifest;
    }
}
