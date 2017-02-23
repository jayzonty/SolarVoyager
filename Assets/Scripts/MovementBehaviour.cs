using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
	public float acceleration = 10.0f;
	public float deccelerationFactor = 0.25f;
	
	public float zeroSpeedThreshold = 0.1f;
	
	public float maxSpeed = 50.0f;
	
	private float speed = 1.0f;
	private Vector3 moveDirection;
	
	// Direction is expected to be expressed in world coordinates.
	public void Move( Vector3 direction )
	{
		moveDirection = direction.normalized;
		
		speed = Mathf.Clamp( speed + acceleration * Time.deltaTime, 0.0f, maxSpeed );
	}
	
	void Start()
	{
		moveDirection = Vector3.zero;
	}
	
	void Update()
	{
		transform.Translate( moveDirection * speed, Space.World );
		if( speed > 0.0f )
		{
			speed = speed * ( 1.0f - deccelerationFactor );
			if( speed <= zeroSpeedThreshold )
			{
				speed = 0.0f;
			}
		}
	}
}
