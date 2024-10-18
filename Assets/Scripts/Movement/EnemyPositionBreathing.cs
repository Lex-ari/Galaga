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
        initialPositionVector = transform.position;
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
            ToInitPosition();
            breathingPosition = 0;
            rising = true;
        }
        if (currentState == formationState.Breathing)
        {
            FormationBreathe();
            UpdateBreathing();
        }
        if (currentState == formationState.Sliding)
        {
            FormationSlide();
            UpdateBreathing();
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
    }

    void FormationSlide()
    {
        float plusMinusRatio = breathingPosition * 2f - 1f;
        Vector3 newPosition = initialPositionVector + new Vector3(plusMinusRatio, 0, 0);
        transform.position = newPosition;
    }

    void ToInitPosition()
    {
        transform.position = Vector2.MoveTowards(transform.position, initialPositionVector, 1f * Time.deltaTime);
        if (Vector3.Distance(transform.position, initialPositionVector) <= 0.1f)
        {
            currentState = formationState.Breathing;
        }
    }

    void UpdateBreathing()
    {
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

    public void setFormationIdle()
    {
        currentState = formationState.Idle;
    }
}
