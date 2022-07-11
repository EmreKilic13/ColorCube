using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerForNextLevelScene : MonoBehaviour
{
    public Image time;
    public Text timeText;
    private float currentTime = 3;
    private float duration = 3;
    void Start()
    {
        currentTime = duration;
        timeText.text = currentTime.ToString();
        StartCoroutine(countdownTime());
        
    }

    private IEnumerator countdownTime()
    {
        while (currentTime >= 0)
        {
            time.fillAmount = Mathf.InverseLerp(0, duration, currentTime);
            timeText.text = currentTime.ToString();
            yield return new WaitForSeconds(1f);
            currentTime--;
        }
        SceneManager.LoadScene(PlayerPrefs.GetString(StaticDefinitions.NEXT_LEVEL_NAME));
        yield return null;
    }
}
