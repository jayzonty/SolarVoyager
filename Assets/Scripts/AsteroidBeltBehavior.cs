using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBeltBehavior : MonoBehaviour
{
	public GameObject asteroidPrefab;
	public int clusterNumber = 100;
	public float radius = 2400;
	
	public float depthVariance = 50.0f;
	public float heightVariance = 20.0f;

	// Use this for initialization
	void Start()
	{
		for( int i = 0; i <= clusterNumber; i++ )
		{
			Vector3 pos = new Vector3();
			float angle = Random.Range ( 0, Mathf.PI * 2 );
			
			float diff = Random.Range( -depthVariance, depthVariance );
			
			pos.x = ( radius + diff ) * Mathf.Cos( angle );
			pos.y = Random.Range( -heightVariance, heightVariance );
			pos.z = ( radius + diff ) * Mathf.Sin( angle );

			GameObject go =	Instantiate (asteroidPrefab, pos, Random.rotation );
			go.transform.SetParent (this.transform);
		}
	}
}
