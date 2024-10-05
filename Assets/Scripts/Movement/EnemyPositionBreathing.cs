using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPositionBreathing : MonoBehaviour
{

    GameObject center;

    float breathingPosition = 0.0f;
    bool rising = true;
    private float ratio = 0.3f;
    private float seconds = 2.0f;
    Vector3 initialPositionVector;

    // Start is called before the first frame update
    void Start()
    {
        // center = transform.parent.gameObject;
        initialPositionVector = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float ratioedBreathingPosition = breathingPosition * ratio;

        Vector3 newPosition = Vector3.Scale(initialPositionVector, new Vector3(1 + ratioedBreathingPosition, 1 + ratioedBreathingPosition, 1 + ratioedBreathingPosition));

        if (rising) {
            breathingPosition += Time.deltaTime / seconds;
        } else {
            breathingPosition -= Time.deltaTime / seconds;
        }

        if (breathingPosition >= 1.0f)
        {        
            rising = false;
        } 
        if (breathingPosition <= 0.0f){
            rising = true;
        }

        transform.localPosition = newPosition;
    }
}
