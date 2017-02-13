using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
	public float acceleration = 10.0f;
	public float decceleration = 10.0f;
	
	public float maxSpeed = 50.0f;
	
	private Vector3 moveDirection;
	private Vector3 velocity;
	
	// Direction is expected to be expressed in world coordinates.
	public void Move( Vector3 direction )
	{
		moveDirection = direction.normalized;
	}
	
	void Start()
	{
		moveDirection = velocity = Vector3.zero;
	}
	
	void Update()
	{
		Vector3 accelerationVector = moveDirection * acceleration * Time.deltaTime;
		velocity = Vector3.ClampMagnitude( velocity + accelerationVector, maxSpeed );
		
		transform.Translate( velocity, Space.World );
		
		if( velocity.sqrMagnitude > 0.0f )
		{
			float deltaSpeed = Mathf.Min( velocity.magnitude, decceleration * Time.deltaTime );
			
			Vector3 deccelerationVector = -velocity.normalized * deltaSpeed;
			velocity += deccelerationVector;
		}
	}
}
