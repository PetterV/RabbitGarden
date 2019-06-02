using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeDetection : MonoBehaviour
{
    public bool onRun = false;
    public bool finishRun = false;
    public bool readyToPlay = false;
    public GameObject player;
    GameController gameController;

    float timeAfterFinish = 0.02f;
    public float finishTimer = 0f;


    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (finishRun) //Once a run is complete, there's a brief window before the game concludes
        {
            finishTimer += Time.deltaTime;
            if (finishTimer >= timeAfterFinish)
            {
                finishRun = false;
                finishTimer = 0f;
                FinishAtHome();
            }
        }
    }

    //Method to complete a run
    public void FinishAtHome()
    {
        player.SetActive(false);
        readyToPlay = false;
        Debug.Log("Finished!");
        gameController.RunComplete();
    }

    //Register the start of a run
    void OnTriggerExit2D(Collider2D col)
    {
        if (readyToPlay && col.gameObject == player)
        {
            onRun = true;
        }
    }

    //Register the end of a run
    void OnTriggerEnter2D(Collider2D col)
    {
        if (onRun && col.gameObject == player)
        {
            onRun = false;
            finishRun = true;
        }
    }
}
