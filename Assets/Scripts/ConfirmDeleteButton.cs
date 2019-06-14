using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmDeleteButton : MonoBehaviour
{
    public void FindLevelControllerAndDeleteRecords()
    {
        GameObject.Find("LevelController").GetComponent<LevelController>().DeleteRecords();
    }
}
