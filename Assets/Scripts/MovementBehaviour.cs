using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
	public float acceleration = 10.0f;
	public float decceleration = 10.0f;
	
	public float maxSpeed = 50.0f;
	
	private Vector3 direction;
	private float speed = 0.0f;
	
	// The direction is relative to the Transform component of
	// whatever game object this script is attached to.
	public void Move( Vector3 direction )
	{
		speed = Mathf.Clamp( speed + acceleration * Time.deltaTime, 0.0f, maxSpeed );
		this.direction = direction.normalized;
	}
	
	void Start()
	{
		speed = 0.0f;
	}
	
	void Update()
	{
		speed = Mathf.Clamp( speed - decceleration * Time.deltaTime, 0.0f, maxSpeed );
		if( speed > 0.0f )
		{
			transform.Translate( direction * speed );
		}
	}
}
