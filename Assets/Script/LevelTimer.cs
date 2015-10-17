﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelTimer : MonoBehaviour
{
    public float time = 60;
    public static float levelTimer;
    private int currentLevel;

    // Use this for initialization
    void Start()
    {
        levelTimer = time;
        currentLevel = Application.loadedLevel;
    }


    // Update is called once per frame
    void Update()
    {
        levelTimer -= Time.deltaTime;

        if (levelTimer <= 0)
            LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        var nextLevel = currentLevel + 1;
        if (nextLevel < Application.levelCount)
        {
            levelTimer = time;
            Application.LoadLevel(nextLevel);
        }
        else
        {
            Application.LoadLevel(0);
        }
    }
}
