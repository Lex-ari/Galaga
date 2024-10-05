using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{

    public int health = 1;

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

    void OnTriggerEnter2D() {
        Debug.Log("Hit");
        health--;
    }

    void Die(){
        Destroy(gameObject);
    }
}
