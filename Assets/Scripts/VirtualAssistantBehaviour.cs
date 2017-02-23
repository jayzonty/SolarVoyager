using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualAssistantBehaviour : MonoBehaviour
{
	public Transform player;
	
	private MeshRenderer meshRenderer;
	
	void Awake()
	{
		meshRenderer = GetComponentInChildren<MeshRenderer>();
	}
	
    void Start()
    {
    }

    void Update()
    {
		if( player != null )
		{
			// Attempt to follow player
			Vector3 playerPos = player.position;
			//transform.position = playerPos + Vector3.forward;
		}
    }
	
	public void Show()
	{
		if( meshRenderer != null )
		{
			meshRenderer.enabled = true;
		}
	}
	
	public void Hide()
	{
		if( meshRenderer != null )
		{
			meshRenderer.enabled = false;
		}
	}
	
	public bool IsVisible
	{
		get
		{
			return meshRenderer.enabled;
		}
	}
	
	public void Speak( string text, AudioClip clip )
	{
		AudioSource audioSource = PlayerController.instance.GetComponentInChildren<AudioSource>();
		DialogueWindowBehaviour dialogueWindow = UIManager.Instance.dialogueWindow;
		if( dialogueWindow != null && dialogueWindow.gameObject.activeInHierarchy )
		{
			Dialogue dialogue = new Dialogue();
			dialogue.AddDialogueLine( text, clip );
			
			GameState.SetCurrentMode( GameState.Mode.Dialogue );
			
			dialogueWindow.InitializeDialogue( dialogue );
			dialogueWindow.SetAudioSource( audioSource );
			
			dialogueWindow.Show();
			dialogueWindow.ShowNextLine();
		}
		else
		{
			audioSource.clip = clip;
			audioSource.Play();
		}
	}
}
