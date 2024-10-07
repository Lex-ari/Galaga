using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{

    public float timeBetweenShots = 0.2f;

    public GameObject missilePrefab;
    public Vector3 missileOffset = new Vector3(0, -2f, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FirePattern()
    {
        StartCoroutine(FireTwice());
    }

    IEnumerator FireTwice()
    {
        FireOnce();
        yield return new WaitForSeconds(timeBetweenShots);
        FireOnce();
    }

    public void FireOnce()
    {
        Instantiate(missilePrefab, transform.position + missileOffset, Quaternion.Euler(new Vector3(0, 0, 0)));
    }
}
