using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private AttractedObject3 attractor;
	public float height;
	public float speed;
	public float sprintFactor;
	public float boost;
	public float turnSensivity;

	public PlayerInput input;
	public GameObject leftArm;
	public GameObject rightArm;

	private Transform moveDirectionReference;

	private Rigidbody rb;

	private float jumpFactor = 0.5f;
	private bool mustJump = false;
	private bool isChargingJump = false;

	private bool boosting = false;
	private bool pressingBoost = false;
	private Vector3 boostingVector;
	private float boostingDelta = 0.5f;
	public float boostingSmoothness;

	private bool sprinting = false;

	private Vector3 displacement = Vector3.zero;
	private Vector2 movement = Vector2.zero;
	private float turning = 0f;

	private PlayerGravity pg;

    void Start(){
		pg = GetComponent<PlayerGravity>();
		rb = GetComponent<Rigidbody>();
		attractor = GetComponent<PlayerGravity>().mainAttractor;

		float earthRadius = attractor.transform.lossyScale.z;
		transform.position = new Vector3(0, earthRadius, 0) + attractor.transform.position;

		moveDirectionReference = leftArm.transform;

		Collider ownCollider = GetComponentInChildren<Collider>();
		GameObject[] arms = GameObject.FindGameObjectsWithTag("Arm");
		for(int i = 0; i < arms.Length; ++i)
        {
			Physics.IgnoreCollision(ownCollider, arms[i].GetComponent<Collider>(), true);
        }

		//leftArm.GetComponent<TestHand>().playerCollider = ownCollider;
		//rightArm.GetComponent<TestHand>().playerCollider = ownCollider;
	}

	public void onMove(InputAction.CallbackContext ctx)
    {
		movement = ctx.ReadValue<Vector2>();
    }

	public void onTurn(InputAction.CallbackContext ctx)
    {
		turning = ctx.ReadValue<Vector2>().x;
    }

	public void onJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
			isChargingJump = true;
        }
        if (ctx.canceled)
        {
			isChargingJump = false;
			mustJump = true;
        }
    }

	public void onBoost(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
			pressingBoost = true;
			boosting = true;
		}

		if (ctx.canceled)
			pressingBoost = false;
    }

	public void onSprint(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
			sprinting = true;
        }
		if (ctx.canceled)
		{
			sprinting = false;
		}
	}

    private void Update()
    {
		//allows the player to turn on his horizontal plane
		transform.rotation *=
			Quaternion.Euler(new Vector3(0, turning * turnSensivity * Time.deltaTime, 0));
	}

    void FixedUpdate()
    {
		attractor = pg.mainAttractor;

		Vector3 mainVector = transform.position - attractor.transform.position;
		mainVector.Normalize();

		Vector3 jumpForce = Vector3.zero;
		if (isChargingJump)
		{
			jumpFactor += Time.fixedDeltaTime;
		}
		if (mustJump)
		{
			jumpForce = mainVector * jumpFactor * height;
			mustJump = false;
			jumpFactor = 0.5f;
		}

		rb.AddForce(jumpForce, ForceMode.VelocityChange);

		//------------------------------------------------------------------------------------------

		//makes the player always be standing in relation to the planet
		transform.rotation = Quaternion.FromToRotation(transform.up, mainVector) * transform.rotation;

		//moving
		Vector3 normal = transform.up;
		Vector3 right = Vector3.ProjectOnPlane(moveDirectionReference.right, normal);
		Vector3 forward = Vector3.ProjectOnPlane(moveDirectionReference.forward, normal);
		displacement = (movement.x * right + movement.y * forward);

		if (sprinting)
		{
			displacement *= sprintFactor;
		}
		displacement *= Time.fixedDeltaTime * speed;
		float heightCorrection = attractor.radius -
			Mathf.Sqrt(displacement.sqrMagnitude + Mathf.Pow(attractor.radius, 2));

		//rb.MovePosition(rb.position + displacement + heightCorrection * mainVector);
		transform.position += displacement + heightCorrection * mainVector;

		if (pressingBoost)
		{
			boostingVector = rightArm.transform.forward * boost;
			Rigidbody arb = attractor.GetComponent<Rigidbody>();
			//if (arb != null) boostingVector += arb.velocity;
		}


		if (boosting){
			Vector3 correctionVector = boostingSmoothness * (boostingVector - rb.velocity);
			if(correctionVector.magnitude < boostingDelta && !pressingBoost)
            {
				boosting = false;
            } else
            {
				rb.velocity += correctionVector;
            }				
		}
	}

    private void OnCollisionEnter(Collision collision)
    {
		if(!pressingBoost)
			boosting = false;
    }

}
