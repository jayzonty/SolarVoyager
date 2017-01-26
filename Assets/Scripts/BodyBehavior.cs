using UnityEngine;

public class BodyBehavior : MonoBehaviour
{
	//public BodyBehavior other;
    
	public bool shouldRotate = true;
    public float rotationPeriod = 360.0f;
	public float rotationTheta = 0.0f;
	
    private float rotationVelocity;
	
	public double mass;
	
	void Start()
	{
        
	}
	
	void Update()
	{
		rotationVelocity = 360.0f / rotationPeriod;
		
		rotationTheta += rotationVelocity * Time.deltaTime;
		
        // For now, just rotate on one axis
        transform.localRotation = Quaternion.Euler( 0.0f, rotationTheta, 0.0f );
	}
}
