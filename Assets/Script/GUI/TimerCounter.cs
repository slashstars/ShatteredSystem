﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerCounter : MonoBehaviour
{
    public int levelTimer = 60;

    private Text counterText;

    // Use this for initialization
    void Start()
    {
        counterText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        counterText.text = "TimeLeft:" + Mathf.Round(LevelTimer.levelTimer) + "s";
    }
}