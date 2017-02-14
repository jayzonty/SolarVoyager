using UnityEngine;

public class BodyBehavior : MonoBehaviour
{
	//public BodyBehavior other;
	public string planetName;
	
	// TODO: Make this more elegant lol
	public float followOffset = 30.0f;
	
    private float rotationVelocity;
	
	public double mass;
	
	void Start()
	{
        
	}
	
	void Update()
	{
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
