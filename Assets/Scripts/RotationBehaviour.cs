using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBehaviour : MonoBehaviour
{
	public float period = 360.0f;
	public float theta = 0.0f;
	
	void Start()
	{
	}
	
	void Update()
	{
		float rotationVelocity = 360.0f / period;
		
		theta += rotationVelocity * Time.deltaTime;
		
		// Wrap around when theta exceeds 360 degrees.
		if( theta > 360.0f )
		{
			theta -= 360.0f;
		}
		
		// For now, just rotate on one axis
		transform.localRotation = Quaternion.Euler( 0.0f, theta, 0.0f );
	}
}
