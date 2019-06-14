using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuriousCreature : MonoBehaviour
{
    SpriteRenderer sprite;
    CircleCollider2D dangerArea;
    CircleCollider2D followArea;
    GameObject player;

    public bool interested = true;
    bool hasMovedAway = false;
    public bool playerWithinDanger = false;
    public bool playerWithinFollow = false;

    float moveSpeed = 0.03f;

    float disinterestTimer;
    float disinterestDuration = 7.0f;

    float freezeDuration = 2.0f;

    bool coloredDisinterest = false;
    public Color interestedColor;
    public Color disinterestedColor;

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
        if (playerWithinDanger && interested)
        {
            CaughtPlayer();
        }

        // Follow the player if they are within range
        if (playerWithinFollow && interested)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed);
        }

        if (!interested)
        {
            if (disinterestTimer >= disinterestDuration)
            {
                RestoreInterest();
            }
            else
            {
                disinterestTimer += Time.deltaTime;
                if (!playerWithinFollow && !hasMovedAway)
                {
                    hasMovedAway = true;
                }
            }

            // Move away from player
            if (disinterestTimer >= (freezeDuration / 2) && playerWithinFollow && !hasMovedAway)
            {
                Vector2 newDirection = new Vector2(player.transform.position.x * -1, player.transform.position.y * -1);
                transform.position = Vector2.MoveTowards(transform.position, newDirection, (moveSpeed / 2));
                if (!coloredDisinterest)
                {
                    DisinterestColouring();
                }
            }
        }
    }

    public void BecomeDisinterested()
    {
        interested = false;
        disinterestTimer = 0.0f;
    }

    void DisinterestColouring()
    {
        sprite.color = disinterestedColor;
        coloredDisinterest = true;
    }

    void RegularColoring()
    {
        sprite.color = interestedColor;
        coloredDisinterest = false;
    }

    public void RestoreInterest()
    {
        interested = true;
        hasMovedAway = false;
        RegularColoring();
    }

    public void CaughtPlayer()
    {
        BecomeDisinterested();
        player.GetComponent<RabbitMovement>().Freeze(freezeDuration);
    }
}
