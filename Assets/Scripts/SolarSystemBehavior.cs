using UnityEngine;

using System;
using System.Collections.Generic;

public class SolarSystemBehavior : MonoBehaviour
{
	// For now, solve the two body problem
	public BodyBehavior sun;
	
	private List<BodyBehavior> bodies;
	
	public double G = 6.674;//E-11;
	
	private bool firstRun = true;

    public int numPoints = 36;
	
	void Start()
	{
		sun.position = new Vector3D( sun.transform.position );

        bodies = new List<BodyBehavior>();
	}
	
	void FixedUpdate()
	{
        //NewtonSimulation();
	}
    
    void NewtonSimulation()
    {
        double h = (double)Time.deltaTime;
		
		foreach( BodyBehavior body in bodies )
		{
			Vector3D R = body.position - sun.position;
			double r = R.Magnitude + Double.Epsilon;
			
			//Vector3D A1 = R * ( G * body2.mass / ( r * r * r ) );
			Vector3D A2 = -R * ( G * sun.mass / ( r * r * r ) );
			
			if( firstRun )
			{
				//body1.position = body1.position + ( body1.velocity * 0.5 * h ) + ( A1 * h * h * 0.25 );
				body.position = body.position + ( body.velocity * 0.5 * h ) + ( A2 * h * h * 0.25 );
				firstRun = false;
			}
			else
			{
				//body1.position = body1.position + ( body1.velocity * 0.5 * h );
				body.position = body.position + ( body.velocity * 0.5 * h );
			}
			
			//body1.velocity = body1.velocity + ( A1 * h );
			//body1.position = body1.position + ( body1.velocity * 0.5 * h );
			//body1.transform.position = new Vector3( (float)body1.position.x, (float)body1.position.y, (float)body1.position.z );
			
			body.velocity = body.velocity + ( A2 * h );
			body.position = body.position + ( body.velocity * 0.5 * h );
			body.transform.position = new Vector3( (float)body.position.x, (float)body.position.y, (float)body.position.z );
		}
        
        /*for( int i = 0; i < bodies.Count; i++ )
        {
            bodies[i].acceleration.x = 0.0;
            bodies[i].acceleration.y = 0.0;
            bodies[i].acceleration.z = 0.0;
            
            for( int j = 0; j < bodies.Count; j++ )
            {
                if( i == j )
                {
                    continue;
                }
                
                Vector3D r = bodies[j].position - bodies[i].position;
                double distance = r.Magnitude;
                
                double accel = G * bodies[j].mass / ( distance * distance * distance );
                bodies[i].acceleration = bodies[i].acceleration + r * accel;
            }
        }
        
        for( int i = 0; i < bodies.Count; i++ )
        {
            bodies[i].velocity = bodies[i].velocity + bodies[i].acceleration * h;
            bodies[i].position = bodies[i].position + bodies[i].velocity * h * 0.5;
            
            bodies[i].transform.position = new Vector3( (float)bodies[i].position.x, (float)bodies[i].position.y, (float)bodies[i].position.z );
        }*/
    }
}
