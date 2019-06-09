using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRegistration : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Player within enter");
        if (collider.gameObject.tag == "Player")
        {
            GetComponentInParent<CuriousCreature>().playerWithinFollow = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            GetComponentInParent<CuriousCreature>().playerWithinFollow = false;
        }
    }
}
