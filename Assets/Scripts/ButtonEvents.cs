using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class ButtonEvents : MonoBehaviour
{
    public TMP_Text scoreText;
    private void Awake()
    {
        scoreText.text = "+" + PlayerPrefs.GetInt(StaticDefinitions.SCORE).ToString() + " POINTS";
    }
    public void restartButton()
    {
        
        SceneManager.LoadScene(PlayerPrefs.GetString(StaticDefinitions.LEVEL_NAME_TO_RETURN));
    }
}
