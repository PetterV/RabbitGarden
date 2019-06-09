using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuriousCreature : MonoBehaviour
{
    SpriteRenderer sprite;
    CircleCollider2D dangerArea;
    CircleCollider2D followArea;
    GameObject player;

    public bool playerWithinDanger = false;
    public bool playerWithinFollow = false;

    float moveSpeed = 0.03f;

    // Replace the Colliders with Scripts on the collider objects, setting bools in this one.

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player"); //Is it dangerous to have this here?

        sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        dangerArea = transform.Find("DangerArea").GetComponent<CircleCollider2D>();
        followArea = transform.Find("FollowArea").GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Follow the player if they are within range
        if (playerWithinFollow)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed);
        }
    }
}
