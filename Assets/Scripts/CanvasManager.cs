using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class CanvasManager : MonoBehaviour
{
    public TMP_Text levelNameText;
    public TMP_Text scoreText;
    void Awake()
    {
        
        setLevelName(PlayerPrefs.GetInt(StaticDefinitions.NUMBER_OF_CURRENT_LEVEL));
        setScore(PlayerPrefs.GetInt(StaticDefinitions.SCORE));
    }

    public void setLevelName(int numberOfLevel)
    {
        levelNameText.text = "Level "+ numberOfLevel.ToString();
    }

    public void setScore(int score)
    {
        scoreText.text = "Score +" + score.ToString();
    }

    
}
