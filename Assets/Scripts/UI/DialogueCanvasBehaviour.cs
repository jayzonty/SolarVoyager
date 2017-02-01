using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class DialogueCanvasBehaviour : MonoBehaviour
{
	private Dialogue dialogue;
	private int linePointer;
	
	private AudioSource audioSource = null;
	
	private Text textField;
	
	void Awake()
	{
		textField = GetComponentInChildren<Text>();
		if( textField == null )
		{
			Debug.LogError( "DialogCanvas must have a child with a Text component!" );
		}
	}
	
	void Start()
	{
	}
	
	void Update()
	{
	}
	
	public void SetAudioSource( AudioSource audioSource )
	{
		this.audioSource = audioSource;
	}
	
	public void ClearAudioSource()
	{
		this.audioSource = null;
	}
	
	public void InitializeDialogue( Dialogue dialogue )
	{
		this.dialogue = dialogue;
		
		Debug.Log( "Initializing dialogue with " + dialogue.LineCount + " lines." );
		
		ResetLinePointer();
	}
	
	public void ShowNextLine()
	{
		if( ( dialogue != null ) && HasNextLine() )
		{
			DialogueLine currentLine = dialogue.GetDialogueLine( linePointer );
			
			textField.text = currentLine.text;
			
			if( ( audioSource != null ) && ( currentLine.audioClip != null ) )
			{
				// If the audioSource is already playing something, stop it.
				audioSource.Stop();
				
				audioSource.clip = currentLine.audioClip;
				audioSource.Play();
			}
			
			// Increment line pointer.
			linePointer++;
		}
	}
	
	public bool HasNextLine()
	{
		return ( dialogue != null ) && ( linePointer < dialogue.LineCount );
	}
	
	public void ResetLinePointer()
	{
		linePointer = 0;
	}
	
	public void Close()
	{
		if( audioSource != null )
		{
			audioSource.Stop();
		}
	}
}
