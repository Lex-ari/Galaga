using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{

    public GameObject alienManifest;
    
    private AlienManifest alienManifestScript;

    // Start is called before the first frame update
    void Start()
    {
        alienManifestScript = alienManifest.GetComponent<AlienManifest>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
