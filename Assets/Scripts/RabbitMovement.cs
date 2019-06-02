using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMovement : MonoBehaviour
{
	// Movement Controls
	public bool readyToStart = false;
	public bool moveActive = false;
	public bool moving = false;
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

        if(readyToStart && Input.GetKeyDown("down")){
	    	readyToStart = false;
	    	moveActive = true;
        	ChangeToDown();
        }

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
        	if ( movingDown && movingDownIsValid ) {
        		MoveDown();
        	}
        	if ( movingLeft && movingLeftIsValid ) {
        		MoveLeft();
        	}
        	if ( movingUp && movingUpIsValid ) {
        		MoveUp();
        	}
        	if ( movingRight && movingRightIsValid ) {
        		MoveRight();
        	}
        //} 
    }

    void ChangeToDown(){
		DeactivateAllDirections();
    	movingDown = true;
    	Debug.Log("Moving down");
    }

    void ChangeToLeft(){
    	DeactivateAllDirections();
    	movingLeft = true;
    	Debug.Log("Moving left");
    }

    void ChangeToUp(){
    	DeactivateAllDirections();
    	movingUp = true;
    	Debug.Log("Moving up");
    }

    void ChangeToRight(){
    	DeactivateAllDirections();
    	movingRight = true;
    	Debug.Log("Moving right");
    }

    void MoveDown(){
    	Vector2 targetPosition = new Vector2(0, 0 - moveDistance);
		transform.Translate(targetPosition);
    }

    void MoveLeft(){
    	Vector2 targetPosition = new Vector2(0 - moveDistance, 0);
		transform.Translate(targetPosition);
    }

    void MoveUp(){
    	Vector2 targetPosition = new Vector2(0, 0 + moveDistance);
		transform.Translate(targetPosition);
    }

    void MoveRight(){
    	Vector2 targetPosition = new Vector2(0 + moveDistance, 0);
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
    		Debug.Log("No longer colliding with WestWall");
    		movingLeftIsValid = true;
    	}
    	else if (otherCollider.gameObject.name == "NorthWall"){
    		Debug.Log("No longer colliding with NorthWall");
    		movingUpIsValid = true;
    	}
    	else if (otherCollider.gameObject.name == "EastWall"){
    		Debug.Log("No longer colliding with EastWall");
    		movingRightIsValid = true;
    	}
    	else if (otherCollider.gameObject.name == "SouthWall"){
    		Debug.Log("No longer colliding with SouthWall");
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
}
