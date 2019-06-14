using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CompletionPanel : MonoBehaviour
{
    TextMeshProUGUI foodCount;
    TextMeshProUGUI totalCarrotCount;

    TextMeshProUGUI timerDisplay;
    TextMeshProUGUI finalTimeDisplay;
    TextMeshProUGUI starDisplay;

    TextMeshProUGUI missingCarrotsDisplay;
    TextMeshProUGUI penaltyTimeDisplay;

    TextMeshProUGUI targetTimeText;

    void Start()
    {
        foodCount = GameObject.Find("FoodCount").GetComponent<TextMeshProUGUI>();
        totalCarrotCount = GameObject.Find("TotalFoodCount").GetComponent<TextMeshProUGUI>();

        timerDisplay = GameObject.Find("TimerDisplay").GetComponent<TextMeshProUGUI>();
        finalTimeDisplay = GameObject.Find("FinalTimeDisplay").GetComponent<TextMeshProUGUI>();
        starDisplay = GameObject.Find("StarDisplay").GetComponent<TextMeshProUGUI>();

        missingCarrotsDisplay = GameObject.Find("MissingCarrotsDisplay").GetComponent<TextMeshProUGUI>();
        penaltyTimeDisplay = GameObject.Find("PenaltyTimeDisplay").GetComponent<TextMeshProUGUI>();

        targetTimeText = GameObject.Find("TargetTimeDisplay").GetComponent<TextMeshProUGUI>();

        gameObject.SetActive(false);
    }

    public void SetPanelInfo(int food, int totalFood, float timer, float finalTime, int missingCarrots, float penaltyTime, int rating, float requirement3)
    {
        foodCount.text = food.ToString();
        totalCarrotCount.text = totalFood.ToString();

        timerDisplay.text = TimerToString(timer);
        finalTimeDisplay.text = TimerToString(finalTime);
        starDisplay.text = GetCorrectRating(rating);

        missingCarrotsDisplay.text = missingCarrots.ToString();
        penaltyTimeDisplay.text = TimerToString(penaltyTime);

        if (rating < 3)
        {
            targetTimeText.text = "Get under " + TimerToString(requirement3) + " to earn * * *";
        }
        else
        {
            targetTimeText.gameObject.SetActive(false);
        }
    }

    string TimerToString(float timer)
    {
        int timerInt = Mathf.FloorToInt(timer);
        float ddFloat = (timer - timerInt) * 100;
        int ddInt = System.Convert.ToInt32(ddFloat);
        string dd = ddInt.ToString("00");
        string ss = System.Convert.ToInt32(timer % 60).ToString("00");
        string mm = (Mathf.Floor(timer / 60) % 60).ToString("00");
        string timerText = mm + ":" + ss + ":" + dd;
        return timerText;
    }

    string GetCorrectRating(int rating)
    {
        string ratingString = "*";
        if (rating == 3)
        {
            ratingString = "* * *";
        }
        else if (rating == 2)
        {
            ratingString = "* *";
        }
        return ratingString;
    }
}
