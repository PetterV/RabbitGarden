using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CompletionPanel : MonoBehaviour
{
    public TextMeshProUGUI foodCount;

    public void SetPanelInfo(int food)
    {
        foodCount.text = food.ToString();
    }
}
