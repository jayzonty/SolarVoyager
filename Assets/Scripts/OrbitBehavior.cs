using UnityEngine;

public class OrbitBehavior : MonoBehaviour
{
    public static readonly int NUM_POINTS = 180;
    public float inclination;
    public float period; // in seconds

    public bool circularOrbit = true;

    public float initialTheta = 0.0f;
    public bool randomizeInitialTheta = true;

    public float aphelion;
    public float perihelion;

    private float theta;

    private BodyBehavior body;

    void Start()
    {
        if( randomizeInitialTheta )
        {
            theta = Random.Range( 0.0f, 360.0f );
        }
        else
        {
            theta = initialTheta;
        }

        Vector3 inclinationAngle = new Vector3();
        inclinationAngle.x = inclination;

        transform.localEulerAngles = inclinationAngle;

        body = GetComponentInChildren<BodyBehavior>();

        if( circularOrbit )
        {
            CircularOrbitInit();
			CircularOrbitCalculatePosition();
		
			// TODO: Refactor. This is really bad lol.
			if( PlayerController.instance.followTarget != null )
			{
				if( PlayerController.instance.followTarget.transform == body.transform )
				{
					PlayerController.instance.WarpToPlanet( body.transform );
				}
			}
        }
    }

    void Update()
    {
        if( circularOrbit )
        {
            CircularOrbitUpdate();
        }
    }

    void CircularOrbitInit()
    {
        // For a circular orbit, radius is the mean between aphelion and perihelion
        float radius = ( aphelion + perihelion ) / 2.0f;

        LineRenderer lineRenderer = GetComponent<LineRenderer>();
		if( lineRenderer != null )
		{
			lineRenderer.numPositions = NUM_POINTS + 1;
			lineRenderer.startWidth = lineRenderer.endWidth = 1.25f;
			//lineRenderer.material = orbitLineMaterial;
			lineRenderer.useWorldSpace = false;

			float thetaStep = 2 * Mathf.PI / NUM_POINTS;
			for( int i = 0; i <= NUM_POINTS; i++ )
			{
				Vector3 pos = new Vector3();
				pos.x = radius * Mathf.Cos( thetaStep * i );
				pos.y = 0.0f;
				pos.z = radius * Mathf.Sin( thetaStep * i );
				
				lineRenderer.SetPosition( i, pos );
			}
		}
    }

    void CircularOrbitUpdate()
    {
        float velocity = 2 * Mathf.PI / period;
        theta += velocity * Time.deltaTime;
		
        CircularOrbitCalculatePosition();
    }
	
	void CircularOrbitCalculatePosition()
	{
		// For a circular orbit, radius is the mean between aphelion and perihelion
        float radius = ( aphelion + perihelion ) / 2.0f;
		
		float bx = radius * Mathf.Cos( theta );
        float by = 0.0f;
        float bz = radius * Mathf.Sin( theta );

        body.transform.localPosition = new Vector3( bx, by, bz );
	}

    void EllipticalOrbitUpdate()
    {
    }
}
