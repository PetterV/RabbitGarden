using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class LevelController : MonoBehaviour
{
    public int highestLevelCountPlusOne = 0;

    public float[] levelTimes;
    public int[] levelRatings;

    void Awake()
    {
        GameObject[] levelControllers = GameObject.FindGameObjectsWithTag("LevelController");

        if (levelControllers.Length > 1)
        {
            Destroy(this.gameObject);
        }

        // Otherwise, save this
        DontDestroyOnLoad(this.gameObject);

        levelTimes = new float[highestLevelCountPlusOne];
        levelRatings = new int[highestLevelCountPlusOne];

        LoadGame();

        if (levelTimes.Length < highestLevelCountPlusOne || levelRatings.Length < highestLevelCountPlusOne)
        {
            Debug.LogError("Save file was shorter than needed. Creating new files.");
            levelTimes = new float[highestLevelCountPlusOne];
            levelRatings = new int[highestLevelCountPlusOne];
            SaveGame();
        }
    }
    
    public void LoadLevel(int levelNumber)
    {
        SaveGame();
        string levelName = "Level" + levelNumber.ToString();
        SceneManager.LoadScene(levelName);
    }

    public void ReturnToLevelSelect()
    {
        SaveGame();
        SceneManager.LoadScene("MainMeny");
    }

    public void StoreLevelTime(int levelID, float levelTime, int rating)
    {
        levelRatings[levelID] = rating;
        levelTimes[levelID] = levelTime;
    }

    public float GetLevelTime(int levelID)
    {
        float levelTime = levelTimes[levelID];
        return levelTime;
    }
    public int GetLevelRating(int levelID)
    {
        int levelRating = levelRatings[levelID];
        return levelRating;
    }

    public void FinalQuit()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }

    void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file1 = File.Create(Application.persistentDataPath + "/levelTimes.gd");
        bf.Serialize(file1, levelTimes);
        file1.Close();
        FileStream file2 = File.Create(Application.persistentDataPath + "/levelRatings.gd");
        bf.Serialize(file2, levelRatings);
        file2.Close();
    }
    void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/levelTimes.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file1 = File.Open(Application.persistentDataPath + "/levelTimes.gd", FileMode.Open);
            levelTimes = (float[])bf.Deserialize(file1);
            file1.Close();
        }

        if (File.Exists(Application.persistentDataPath + "/levelRatings.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file2 = File.Open(Application.persistentDataPath + "/levelRatings.gd", FileMode.Open);
            levelRatings = (int[])bf.Deserialize(file2);
            file2.Close();
        }
    }
    public void DeleteRecords()
    {
        levelTimes = new float[highestLevelCountPlusOne];
        levelRatings = new int[highestLevelCountPlusOne];
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file1 = File.Create(Application.persistentDataPath + "/levelTimes.gd");
        bf.Serialize(file1, levelTimes);
        file1.Close();
        FileStream file2 = File.Create(Application.persistentDataPath + "/levelRatings.gd");
        bf.Serialize(file2, levelRatings);
        file2.Close();
        
        GameObject.Find("LevelsPanel").GetComponent<LevelsPanel>().UpdateAllLevelInfo();
    }
}
