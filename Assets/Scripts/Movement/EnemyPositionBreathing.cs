using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPositionBreathing : MonoBehaviour
{

    GameObject center;

    List<Transform> childrenPositions = new List<Transform>();
    List<Vector3> childrenInitPositions = new List<Vector3>();
    
    float breathingPosition = 0.0f;
    bool rising = true;
    private float ratio = 0.3f;
    private float seconds = 2.0f;
    Vector3 initialPositionVector;

    public enum formationState {
        Breathing,
        Sliding,
        Idle,

    }

    public formationState currentState = formationState.Idle;


    // Start is called before the first frame update
    void Start()
    {
        // center = transform.parent.gameObject;
        foreach (Transform child in transform)
        {
            childrenPositions.Add(child);
            childrenInitPositions.Add(child.transform.localPosition);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == formationState.Idle){

        }
        if (currentState == formationState.Breathing)
        {
            FormationBreathe();
        }
        if (currentState == formationState.Sliding)
        {

        }
    }

    void FormationBreathe()
    {
        float ratioedBreathingPosition = breathingPosition * ratio;

        for (int i = 0; i < childrenPositions.Count; i++)
        {
            childrenPositions[i].transform.localPosition = Vector3.Scale(
                childrenInitPositions[i], 
                new Vector3(1 + ratioedBreathingPosition, 1 + ratioedBreathingPosition, 0));
        }

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
    }
}
