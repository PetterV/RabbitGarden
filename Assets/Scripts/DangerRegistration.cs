using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerRegistration : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            GetComponentInParent<CuriousCreature>().playerWithinDanger = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            GetComponentInParent<CuriousCreature>().playerWithinDanger = false;
        }
    }
}
