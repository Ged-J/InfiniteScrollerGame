using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] float runSpeed = 5f;
	[SerializeField] float jumpSpeed = 5f;
	[SerializeField] float climbSpeed = 5f;

	Rigidbody2D myRigidBody;
	Animator myAnimator;
	Collider2D myCollider2D;
	float gravityScaleAtStart;

	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();
		myCollider2D = GetComponent<Collider2D> ();
		gravityScaleAtStart = myRigidBody.gravityScale;
	}
	
	// Update is called once per frame
	void Update () {

		Run ();
		FlipSprite ();
		Jump ();
		ClimbLadder ();
	}

	private void Run() {
	
		float movement = Input.GetAxis ("Horizontal"); //value is between -1 to +1
		Vector2 playerVelocity = new Vector2(movement * runSpeed, myRigidBody.velocity.y);
		myRigidBody.velocity = playerVelocity;
		// print (playerVelocity);

		bool playerHasHorizontalSpeed = Mathf.Abs (myRigidBody.velocity.x) > 0;
		myAnimator.SetBool ("Running", playerHasHorizontalSpeed);

	}

	private void ClimbLadder() {

		if (!myCollider2D.IsTouchingLayers (LayerMask.GetMask ("Climbing"))) {

			myAnimator.SetBool ("Climbing", false);
			myRigidBody.gravityScale = gravityScaleAtStart;
			return;
		}

		float movement = Input.GetAxis ("Vertical"); //value is between -1 to +1
		Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, movement * climbSpeed);
		myRigidBody.velocity = climbVelocity;
		myRigidBody.gravityScale = 0f;

		bool playerHasVerticalSpeed = Mathf.Abs (myRigidBody.velocity.y) > 0;
		myAnimator.SetBool ("Climbing", playerHasVerticalSpeed);
	
	}

	private void Jump() {

		if (!myCollider2D.IsTouchingLayers (LayerMask.GetMask ("Ground"))) {
		
			return;
		}

		if (Input.GetButtonDown("Jump")) {
		
			Vector2 jumpVelocityToAdd = new Vector2 (0f, jumpSpeed);
			myRigidBody.velocity += jumpVelocityToAdd;

		}
	
	}

	private void FlipSprite() {
	
		bool playerHasHorizontalSpeed = Mathf.Abs (myRigidBody.velocity.x) > 0;  //true if player has velocity

		if (playerHasHorizontalSpeed) {  // if the player is moving
		
			transform.localScale = new Vector2 (Mathf.Sign (myRigidBody.velocity.x), 1f);  // get sign of velocity, apply it to x scale value
		}
	}
}
