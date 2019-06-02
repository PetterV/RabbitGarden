using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public bool isPaused = true;
    public GameObject player;
    public HomeDetection homeDetection;
    RabbitInventory rabbitInventory;
    RabbitMovement rabbitMovement;

    public GameObject mainMenu;
    public GameObject completionPanel;

	GameObject pauseBar;
    // Start is called before the first frame update
    void Start()
    {
        rabbitMovement = player.GetComponent<RabbitMovement>();
        rabbitInventory = player.GetComponent<RabbitInventory>();
        pauseBar = GameObject.Find("PauseBar");
        pauseBar.SetActive(false);
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
        completionPanel.SetActive(true);
        completionPanel.GetComponent<CompletionPanel>().SetPanelInfo(rabbitInventory.food);
    }

    public void ResetAll()
    {
        homeDetection.readyToPlay = false;
        homeDetection.onRun = false;
        homeDetection.finishTimer = 0f;
        player.transform.position = GameObject.Find("PlayerStartPosition").transform.position;
        rabbitMovement.Reset();
        rabbitInventory.food = 0;
        player.SetActive(false);
    }

    public void Play()
    {
        player.SetActive(true);
        mainMenu.SetActive(false);
        completionPanel.SetActive(false);
        homeDetection.readyToPlay = true;
    }
}
