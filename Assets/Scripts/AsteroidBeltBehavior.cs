using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBeltBehavior : MonoBehaviour {

	public GameObject asteroidPrefab;
	public int clusterNumber = 100;
	public float radius = 2400;

	// Use this for initialization
	void Start () {

		for( int i = 0; i <= clusterNumber; i++ )
		{
			Vector3 pos = new Vector3();
			float angle = Random.Range (0, Mathf.PI * 2);
			pos.x = radius * Mathf.Cos( angle);
			pos.y = 0.0f;
			pos.z = radius * Mathf.Sin( angle);


			GameObject go =	Instantiate (asteroidPrefab);
			go.transform.localPosition = pos;
			go.transform.SetParent (this.transform, true);

		}

	}
	
	
}
