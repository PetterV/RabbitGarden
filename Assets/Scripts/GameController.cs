using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Tooltip("The LevelID; must be the same as in the level select menu")]
    public int levelID;
    [Tooltip("Max number of seconds required for earning 2 stars")]
    public float requirement2;
    [Tooltip("Max number of seconds required for earning 3 stars")]
    public float requirement3;
    [Tooltip("How much time does each missing carrot give")]
    public float penaltyTime;

	public bool isPaused = true;
    [Tooltip("The Rabbit Game Object")]
    public GameObject player;
    [Tooltip("The Hole Game Object")]
    public HomeDetection homeDetection;
    RabbitInventory rabbitInventory;
    RabbitMovement rabbitMovement;

    [Tooltip("The in-level 'main menu' panel")]
    public GameObject mainMenu;
    [Tooltip("The in-level Completion Panel")]
    public GameObject completionPanel;

    TextMeshProUGUI carrotCounter;
    TextMeshProUGUI totalCarrotCounter;
    int totalCarrots;

    [Tooltip("Has the player picked up enough carrots to finish")]
    public bool carrotRatioFinishable = false;
    public Color insufficientCarrots;
    public Color sufficientCarrots;

    public bool runIsActive = false;
    float timer;
    float finalTime;
    TextMeshProUGUI timerText;

	GameObject pauseBar;
    GameObject touchRegistration;
    // Start is called before the first frame update
    void Start()
    {
        rabbitMovement = player.GetComponent<RabbitMovement>();
        rabbitInventory = player.GetComponent<RabbitInventory>();
        carrotCounter = GameObject.Find("CurrentCarrots").GetComponent<TextMeshProUGUI>();
        totalCarrotCounter = GameObject.Find("TotalCarrots").GetComponent<TextMeshProUGUI>();
        timerText = GameObject.Find("TimerTextHUD").GetComponent<TextMeshProUGUI>();
        CountCarrots();
        UpdateCarrotCount();
        UpdateTimer();
        pauseBar = GameObject.Find("PauseBar");
        pauseBar.SetActive(false);
        touchRegistration = GameObject.Find("TouchRegistration");
        touchRegistration.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown("p")){
			if (isPaused){
				UnPause();
			}
			else {
				Pause();
			}
		}

        if (runIsActive)
        {
            timer += Time.deltaTime;
            UpdateTimer();
        }
    }

    void Pause(){
    	isPaused = true;
    	pauseBar.SetActive(true);
    }

    void UnPause(){
    	isPaused = false;
    	pauseBar.SetActive(false);
    }

    public void RunComplete()
    {
        UpdateTimer();
        runIsActive = false;

        int missingCarrots = CalculateMissingCarrots();
        float missingCarrotsTime = CalculateMissingCarrotsTime(missingCarrots);

        finalTime = timer + missingCarrotsTime;

        int rating = 0;
        if (finalTime <= requirement3)
        {
            rating = 3;
        }
        else if (finalTime <= requirement2)
        {
            rating = 2;
        }
        else
        {
            rating = 1;
        }


        completionPanel.SetActive(true);
        completionPanel.GetComponent<CompletionPanel>().SetPanelInfo(rabbitInventory.food, totalCarrots, timer, finalTime, missingCarrots, missingCarrotsTime, rating, requirement3);

        LevelController levelController = GameObject.Find("LevelController").GetComponent<LevelController>();

        if (levelController.GetLevelRating(levelID) == 0)
        {
            levelController.StoreLevelTime(levelID, finalTime, rating);
        }
        else if (finalTime < levelController.GetLevelTime(levelID))
        {
            levelController.StoreLevelTime(levelID, finalTime, rating);
        }

        GameObject.Find("TimerPanel").SetActive(false);
    }

    public void ResetAll()
    {
        GameObject.Find("LevelController").GetComponent<LevelController>().LoadLevel(levelID);
    }

    public void Play()
    {
        player.SetActive(true);
        mainMenu.SetActive(false);
        completionPanel.SetActive(false);
        homeDetection.readyToPlay = true;
        touchRegistration.SetActive(true);
    }

    public void Quit()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

    void CountCarrots()
    {
        totalCarrots = GameObject.Find("AllCarrots").transform.childCount;
    }

    public void UpdateCarrotCount()
    {
        if (!carrotRatioFinishable)
        {
            Image carrotBackground = GameObject.Find("CarrotCounts").GetComponent<Image>();
            double carrots = System.Convert.ToDouble(rabbitInventory.food);
            double allCarrots = System.Convert.ToDouble(totalCarrots);
            double carrotRatio = carrots / allCarrots;
            if (carrotRatio >= 0.5)
            {
                carrotRatioFinishable = true;
                carrotBackground.color = sufficientCarrots;
            }
            else
            {
                carrotBackground.color = insufficientCarrots;
            }
        }
        carrotCounter.text = rabbitInventory.food.ToString();
        totalCarrotCounter.text = totalCarrots.ToString();
    }

    public void ReturnToMainMeny()
    {
        LevelController levelController = GameObject.Find("LevelController").GetComponent<LevelController>();
        levelController.ReturnToLevelSelect();
    }

    public void UpdateTimer()
    {
        int timerInt = Mathf.FloorToInt(timer);
        float ddFloat = (timer - timerInt) * 100;
        int ddInt = System.Convert.ToInt32(ddFloat);
        string dd = ddInt.ToString("00");
        string ss = System.Convert.ToInt32(timer % 60).ToString("00");
        string mm = (Mathf.Floor(timer / 60) % 60).ToString("00");
        timerText.text = mm + ":" + ss + ":" + dd;
    }

    int CalculateMissingCarrots()
    {
        int missingCarrotsTemp = totalCarrots - rabbitInventory.food;
        return missingCarrotsTemp;
    }

    float CalculateMissingCarrotsTime(int missingCarrots)
    {
        float missingCarrotTime = missingCarrots * penaltyTime;
        return missingCarrotTime;
    }
}
