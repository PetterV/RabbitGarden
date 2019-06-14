using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetButtonInfo : MonoBehaviour
{
    public int levelID;
    public int levelRating;
    float levelTime;

    LevelController levelController;

    TextMeshProUGUI timerText;
    TextMeshProUGUI starText;
    // Start is called before the first frame update
    void Start()
    {
        timerText = transform.Find("TimerText").GetComponent<TextMeshProUGUI>();
        starText = transform.Find("StarText").GetComponent<TextMeshProUGUI>();
        UpdateTimerAndStars();
    }

    public void UpdateTimerAndStars()
    {
        levelController = GameObject.Find("LevelController").GetComponent<LevelController>();
        levelRating = levelController.GetLevelRating(levelID);
        levelTime = levelController.GetLevelTime(levelID);

        UpdateTimer();
        SetStars();
    }

    void UpdateTimer()
    {
        int timerInt = Mathf.FloorToInt(levelTime);
        float ddFloat = (levelTime - timerInt) * 100;
        int ddInt = System.Convert.ToInt32(ddFloat);
        string dd = ddInt.ToString("00");
        string ss = System.Convert.ToInt32(levelTime % 60).ToString("00");
        string mm = (Mathf.Floor(levelTime / 60) % 60).ToString("00");
        timerText.text = mm + ":" + ss + ":" + dd;
    }

    void SetStars()
    {
        if (levelRating < 1)
        {
            starText.text = " ";
        }
        else if (levelRating == 1)
        {
            starText.text = "*";
        }
        if (levelRating == 2)
        {
            starText.text = "* *";
        }
        if (levelRating > 2)
        {
            starText.text = "* * *";
        }

        transform.parent.GetComponent<LevelsPanel>().CalculateTotalStars();
    }

    public void LoadLevel()
    {
        GameObject.Find("LevelController").GetComponent<LevelController>().LoadLevel(levelID);
    }
}
