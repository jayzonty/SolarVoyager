using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class DialogueWindowBehaviour : PopupWindowBehaviour
{
	private Dialogue dialogue;
	private int linePointer;
	
	private AudioSource audioSource = null;
	
	private Text textField;
	
	protected override void OnAwake()
	{
		base.OnAwake();
		
		textField = GetComponentInChildren<Text>();
		if( textField == null )
		{
			Debug.LogError( "DialogCanvas must have a child with a Text component!" );
		}
	}
	
	public void SetAudioSource( AudioSource audioSource )
	{
		this.audioSource = audioSource;
	}
	
	public void ClearAudioSource()
	{
		this.audioSource = null;
	}
	
	public void ShowText( string text )
	{
		textField.text = text;
	}
	
	public string GetText()
	{
		return textField.text;
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
}
