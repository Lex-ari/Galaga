/***************************************************************
file: HUDManager.cs
author: Alex Mariano
class: CS 4700 – Game Development
assignment: program 1
date last modified: 10/18/2024

purpose: HUD Manager for Title Screen

****************************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{

    public TMP_Text hiscoreText;
    public TMP_Text pushSpaceKeyText;

    private float timeElapsedFromStart = 0;

    private ScoreManager scoreManagerScript;

    void Awake()
    {
        scoreManagerScript = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateHighScore();
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsedFromStart += Time.deltaTime;
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            Blink();         
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
            }
        }
    }

    //function: Blink
    //purpose: Blinks the PUSH SPACE KEY text in the hud
    void Blink()
    {
        if ((int) timeElapsedFromStart % 2 == 0)
        {
            pushSpaceKeyText.enabled = true;
        }
        else 
        {
            pushSpaceKeyText.enabled = false;
        }
    }

    //function: UpdateHighScore
    //purpose: Updates the High Score if the received score is higher.
    void UpdateHighScore()
    {
        hiscoreText.text = scoreManagerScript.GetHighScore().ToString();
    }

    //function: UpdateLastScore
    //purpose: Updates the Last Score Text
    void UpdateLastScore()
    {
    }
}
