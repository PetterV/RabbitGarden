using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitInventory : MonoBehaviour
{
	public int food = 0;
    GameController gameController;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void OnTriggerEnter2D(Collider2D col){
    	if (col.gameObject.tag == "Food"){
    		food += 1;
    		Destroy(col.gameObject);
            gameController.UpdateCarrotCount();
    	}
    }
}
