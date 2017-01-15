using UnityEngine;

public class OrbitBehavior : MonoBehaviour
{
    public static readonly int NUM_POINTS = 180;
    public float inclination;
    public float period;

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
        lineRenderer.numPositions = NUM_POINTS + 1;
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

    void CircularOrbitUpdate()
    {
        // For a circular orbit, radius is the mean between aphelion and perihelion
        float radius = ( aphelion + perihelion ) / 2.0f;

        float velocity = 2 * Mathf.PI / period;

        theta += velocity * Time.deltaTime;

        body.position.x = radius * Mathf.Cos( theta );
        body.position.y = 0.0;
        body.position.z = radius * Mathf.Sin( theta );

        body.transform.position = new Vector3( (float)body.position.x, (float)body.position.y, (float)body.position.z );
    }

    void EllipticalOrbitUpdate()
    {
    }
}
