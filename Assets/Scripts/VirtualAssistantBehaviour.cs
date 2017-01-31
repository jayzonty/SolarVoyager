using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( AudioSource ) )]
public class VirtualAssistantBehaviour : MonoBehaviour
{
	public Transform player;
	
	private AudioSource audioSource;
	
    // Use this for initialization
    void Start()
    {
		audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
		// Attempt to follow player
		Vector3 playerPos = player.position;		
		transform.position = playerPos + player.right * 2.0f;
    }
	
	public void Speak( AudioClip clip )
	{
		audioSource.clip = clip;
		audioSource.Play();
	}
}
