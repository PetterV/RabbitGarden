using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelsPanel : MonoBehaviour
{
    int currentStars;
    int totalStars;

    TextMeshProUGUI currentStarsText;

    public void UpdateAllLevelInfo()
    {
        GetButtonInfo[] buttons = GetComponentsInChildren<GetButtonInfo>();
        foreach(GetButtonInfo b in buttons)
        {
            b.UpdateTimerAndStars();
        }
        CalculateTotalStars();
    }

    public void CalculateTotalStars()
    {
        currentStars = 0;
        totalStars = 0;

        GetButtonInfo[] buttons = GetComponentsInChildren<GetButtonInfo>();
        foreach (GetButtonInfo b in buttons)
        {
            currentStars += b.levelRating;
            totalStars += 3;
        }

        currentStarsText = GameObject.Find("CurrentStarsText").GetComponent<TextMeshProUGUI>();

        currentStarsText.text = currentStars.ToString() + " out of " + totalStars.ToString() + " Stars";
    }
}
