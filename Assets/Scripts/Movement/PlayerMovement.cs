using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    float maxSpeed = 3.5f;
    float boundry = 3.5f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += Input.GetAxis("Horizontal") * maxSpeed * Time.deltaTime;

        
        if (pos.x > boundry) {
            pos.x = boundry;
        }
        if (pos.x < -boundry) {
            pos.x = -boundry;
        }
        transform.position = pos;
    }

}
