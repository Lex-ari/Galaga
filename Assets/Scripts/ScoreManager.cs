/***************************************************************
file: ScoreManager.cs
author: Alex Mariano
class: CS 4700 – Game Development
assignment: program 1
date last modified: 10/18/2024

purpose: Handles Title Screen Behavior. Waits for user to start
game and sends it to the next scene.

****************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{

    private HUDManager hudManager;
    private int highScore;
    private int lastScore;

    public static ScoreManager instance = null;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        hudManager = GameObject.Find("HUDManager").GetComponent<HUDManager>();
    
        highScore = PlayerPrefs.GetInt("highscore", highScore);
        Debug.Log("got Hiscore: " + highScore);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    //function: UpdateLastScore
    //purpose: Updates the Last Score Text
    public void UpdateLastScore(int lastScore)
    {
        this.lastScore = lastScore;
        if (lastScore > highScore)
        {
            highScore = lastScore;
            PlayerPrefs.SetInt("highscore", highScore);
        }
    }

    public int GetLastScore()
    {
       return lastScore; 
    }

    public int GetHighScore()
    {
        return highScore;
    }
}
