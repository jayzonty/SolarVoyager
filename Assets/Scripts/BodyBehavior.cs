using UnityEngine;

public class BodyBehavior : MonoBehaviour
{
	//public BodyBehavior other;
	public string planetName;
    
	public bool shouldRotate = true;
    public float rotationPeriod = 360.0f;
	public float rotationTheta = 0.0f;
	
	// TODO: Make this more elegant lol
	public float followOffset = 30.0f;
	
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
	
	// TODO: Make this more elegant lol
	// This is to compute for the vector for following purposes
	public Vector3 GetOffsetVector()
	{
		// Assume that the sun is at 0, 0, 0 for now.
		Vector3 displacement = -transform.position;
		Vector3 perp = Vector3.Cross( displacement.normalized, Vector3.up ) + Vector3.up;
		
		Vector3 offset = perp.normalized * followOffset;
		return offset;
	}
}
