using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMovement : MonoBehaviour
{
	// Movement Controls
	public bool readyToStart = false;
	public bool moveActive = false;
	public bool moving = false;
    public bool frozen = false;

	bool movingUp = false;
	bool movingUpIsValid = true;
	bool movingLeft = false;
	bool movingLeftIsValid = true;
	bool movingRight = false;
	bool movingRightIsValid = true;
	bool movingDown = false;
	bool movingDownIsValid = true;

	// Speed Controls
	public float defaultMoveDistance = 0.1f;
	public float moveDistance;
    public float actualMoveDistance;
	
	// Move delay and timer - unclear if they're needed
	//public float moveSpeed = 0.05f;
	//bool moveNow = false;
	//public float moveDelay = 0.1f;
	//public float timer = 0f;


	// Boost
	public bool boostAvailable = true;
	public bool boostActive = false;
	public float boostTime = 0.3f;
	public float boostActiveTimer = 0f;
	public float boostCooldownTime = 1f;
	public float boostCooldown = 0f;
	public float boostSpeed = 0.3f;

    public float obstacleModifier = 1.0f;

    float freezeDuration;
    float freezeTimer = 0.0f;
    float shakeSpeed = 40.0f;
    float shakeAmount = 0.003f;


	// Collider
	Collider2D rabbitCollider;
    

    // Start is called before the first frame update
    void Awake()
    {
    	rabbitCollider = GetComponent<Collider2D>();
    	moveDistance = defaultMoveDistance;
        readyToStart = true;
    }

    // Update is called once per frame
    void Update()
    {
    	/*timer += Time.deltaTime;
    	if (!moveNow && timer > moveDelay){
    		moveNow = true;
    		timer = 0;
    	}*/

    	if (boostActive){
    		boostActiveTimer += Time.deltaTime;
    		if (boostActiveTimer >= boostTime){
    			DeactivateBoost();
    		}
    	}

    	if (!boostAvailable && !boostActive){
    		boostCooldown += Time.deltaTime;
    		if (boostCooldown >= boostCooldownTime){
    			boostAvailable = true;
    		}
    	}

        actualMoveDistance = moveDistance * obstacleModifier;

        // Start setup
        if (readyToStart) {
            if (Input.GetKeyDown("down") || Input.GetKeyDown("left") || Input.GetKeyDown("right") || Input.GetKeyDown("up"))
            {
                if (Input.GetKeyDown("down"))
                {
                    ChangeToDown();
                }
                if (Input.GetKeyDown("up"))
                {
                    ChangeToUp();
                }
                if (Input.GetKeyDown("left"))
                {
                    ChangeToLeft();
                }
                if (Input.GetKeyDown("right"))
                {
                    ChangeToRight();
                }
            }
        }

        // Change direction while moving
        if (moveActive){
	        if (Input.GetKey("down")){
	        	ChangeToDown();
	        }
	        if (Input.GetKey("left")){
	        	ChangeToLeft();
	        }
	        if (Input.GetKey("up")){
	        	ChangeToUp();
	        }
	        if (Input.GetKey("right")){
	        	ChangeToRight();
	        }
	        if (Input.GetKeyDown("space") && boostAvailable){
	        	ActivateBoost();
	        }
        }

        // if ( moveNow ) { - Tied to the timer
        if (!frozen)
        {
            if (movingDown && movingDownIsValid)
            {
                MoveDown();
            }
            if (movingLeft && movingLeftIsValid)
            {
                MoveLeft();
            }
            if (movingUp && movingUpIsValid)
            {
                MoveUp();
            }
            if (movingRight && movingRightIsValid)
            {
                MoveRight();
            }
        }
        else
        {
            //TODO: Add freeze "animation" here
            freezeTimer += Time.deltaTime;
            if (freezeTimer >= freezeDuration)
            {
                Unfreeze();
            }
            else
            {
                float shakePositionX = transform.position.x + Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
                Vector2 newPosition = new Vector2(shakePositionX, transform.position.y);
                transform.position = newPosition;
            }
        }
        //} 
    }

    public void ChangeToDown(){
		DeactivateAllDirections();
    	movingDown = true;
        if (readyToStart)
        {
            readyToStart = false;
            moveActive = true;
            GameObject.Find("GameController").GetComponent<GameController>().runIsActive = true;
        }
        //Debug.Log("Moving down");
    }

    public void ChangeToLeft(){
    	DeactivateAllDirections();
    	movingLeft = true;
        if (readyToStart)
        {
            readyToStart = false;
            moveActive = true;
            GameObject.Find("GameController").GetComponent<GameController>().runIsActive = true;
        }
        //Debug.Log("Moving left");
    }

    public void ChangeToUp(){
    	DeactivateAllDirections();
    	movingUp = true;
        if (readyToStart)
        {
            readyToStart = false;
            moveActive = true;
            GameObject.Find("GameController").GetComponent<GameController>().runIsActive = true;
        }
        //Debug.Log("Moving up");
    }

    public void ChangeToRight(){
    	DeactivateAllDirections();
    	movingRight = true;
        if (readyToStart)
        {
            readyToStart = false;
            moveActive = true;
            GameObject.Find("GameController").GetComponent<GameController>().runIsActive = true;
        }
        //Debug.Log("Moving right");
    }

    void MoveDown(){
    	Vector2 targetPosition = new Vector2(0, 0 - actualMoveDistance);
		transform.Translate(targetPosition);
    }

    void MoveLeft(){
    	Vector2 targetPosition = new Vector2(0 - actualMoveDistance, 0);
		transform.Translate(targetPosition);
    }

    void MoveUp(){
    	Vector2 targetPosition = new Vector2(0, 0 + actualMoveDistance);
		transform.Translate(targetPosition);
    }

    void MoveRight(){
    	Vector2 targetPosition = new Vector2(0 + actualMoveDistance, 0);
		transform.Translate(targetPosition);
    }

    void DeactivateAllDirections(){
    	movingUp = false;
    	movingLeft = false;
    	movingRight = false;
    	movingDown = false;
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
    	if (otherCollider.gameObject.name == "WestWall"){
    		Debug.Log("Collided with WestWall");
            if (movingLeft)
            {
                DeactivateAllDirections();
            }
            movingLeftIsValid = false;
    	}
    	else if (otherCollider.gameObject.name == "NorthWall"){
    		Debug.Log("Collided with NorthWall");
            if (movingUp)
            {
                DeactivateAllDirections();
            }
            movingUpIsValid = false;
    	}
    	else if (otherCollider.gameObject.name == "EastWall"){
    		Debug.Log("Collided with EastWall");
            if (movingRight)
            {
                DeactivateAllDirections();
            }
            movingRightIsValid = false;
    	}
    	else if (otherCollider.gameObject.name == "SouthWall"){
    		Debug.Log("Collided with SouthWall");
            if (movingDown)
            {
                DeactivateAllDirections();
            }
            movingDownIsValid = false;
    	}
    }

    void OnTriggerExit2D(Collider2D otherCollider)
    {
    	if (otherCollider.gameObject.name == "WestWall"){
    		movingLeftIsValid = true;
    	}
    	else if (otherCollider.gameObject.name == "NorthWall"){
    		movingUpIsValid = true;
    	}
    	else if (otherCollider.gameObject.name == "EastWall"){
    		movingRightIsValid = true;
    	}
    	else if (otherCollider.gameObject.name == "SouthWall"){
    		movingDownIsValid = true;
    	}
    }

    void ActivateBoost(){
    	boostActive = true;
    	boostAvailable = false;
    	boostActiveTimer = 0f;
    	moveDistance = boostSpeed;
    }

    void DeactivateBoost(){
    	boostActive = false;
    	boostCooldown = 0f;
    	moveDistance = defaultMoveDistance;
    }

    public void Reset()
    {
        moveDistance = defaultMoveDistance;
        DeactivateAllDirections();
    }

    public void Freeze(float frozenFor)
    {
        frozen = true;
        freezeDuration = frozenFor;
        freezeTimer = 0.0f;
    }

    public void Unfreeze()
    {
        frozen = false;
    }
}
