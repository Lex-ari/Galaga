using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour
{

    public float fireDelay = 1f;
    float cooldownTimer = 0.0f;
    float cooldownTimer2 = 0.0f;
    public float timeBetweenShots = 0.2f;

    public GameObject missilePrefab;
    public Vector3 missileOffset = new Vector3(0, 2f, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        cooldownTimer2 -= Time.deltaTime;
        if (Input.GetButton("Fire1")){
            if (cooldownTimer <= 0){
                cooldownTimer = fireDelay;
                cooldownTimer2 = timeBetweenShots;

                Instantiate(missilePrefab, transform.position + missileOffset, transform.rotation);
            } 
            else if (cooldownTimer2 <= 0){
                cooldownTimer2 = fireDelay;
                Instantiate(missilePrefab, transform.position + missileOffset, transform.rotation);
            }
            else {
                // Do nothing, wait for cooldown
            }
        }
    }
}
