using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( AudioSource ) )]
public class VirtualAssistantBehaviour : MonoBehaviour
{
	public Transform player;
	
	private AudioSource audioSource;
	
	private MeshRenderer meshRenderer;
	
	void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		
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
		DialogueCanvasBehaviour dialogueCanvas = UIManager.Instance.dialogueCanvas;
		if( dialogueCanvas != null )
		{
			// TODO: Original question should be stored somewhere else.
			string originalQuestion = dialogueCanvas.GetText();
			
			Dialogue dialogue = new Dialogue();
			dialogue.AddDialogueLine( originalQuestion + "\n\nAnswer: " + text, clip );
			
			GameState.SetCurrentMode( GameState.Mode.Dialogue );
			
			dialogueCanvas.InitializeDialogue( dialogue );
			dialogueCanvas.SetAudioSource( audioSource );
			
			UIManager.Instance.ShowDialogueBox();
			
			dialogueCanvas.ShowNextLine();
		}
		else
		{
			audioSource.clip = clip;
			audioSource.Play();
		}
	}
}
