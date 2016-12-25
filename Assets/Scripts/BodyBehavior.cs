using UnityEngine;

public class BodyBehavior : MonoBehaviour
{
	//public BodyBehavior other;
    
    public float rotationPeriod = 360.0f;
    private float rotationVelocity;
	
	public double mass;
	
	public Vector3D position;
	public Vector3D velocity;
	public Vector3D acceleration;
	
	void Start()
	{
        rotationVelocity = 2 * Mathf.PI / rotationPeriod;
	}
	
	void Update()
	{
        // For now, just rotate on one axis
        transform.Rotate( Vector3.up * rotationVelocity * Time.deltaTime );
	}
}
