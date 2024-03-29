﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public RabbitMovement rabbitMovement;
    private Vector3 offset;

    // Use this for initialization
    public void Start()
    {
        Vector3 startPosition = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        transform.position = startPosition;
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
        rabbitMovement = player.GetComponent<RabbitMovement>();
        player.SetActive(false);
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        if (!rabbitMovement.frozen)
        {
            transform.position = player.transform.position + offset;
        }
    }
}
