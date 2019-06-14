using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaObstacle : MonoBehaviour
{
    public float speedModifier = 1.0f;
    float defaultModifier = 1.0f;
    
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<RabbitMovement>().obstacleModifier = speedModifier;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<RabbitMovement>().obstacleModifier = defaultModifier;
        }
    }
}
