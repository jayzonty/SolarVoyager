using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Dialogue
{
	private List<DialogueLine> lines;
	
	public Dialogue()
	{
		lines = new List<DialogueLine>();
	}
	
	public int LineCount
	{
		get
		{
			return lines.Count;
		}
	}
	
	public void AddDialogueLine( string text )
	{
		AddDialogueLine( text, null );
	}
	
	public void AddDialogueLine( string text, AudioClip audioClip )
	{
		DialogueLine line;
		line.text = text;
		line.audioClip = audioClip;
		
		AddDialogueLine( line );
	}
	
	public void AddDialogueLine( DialogueLine line )
	{
		lines.Add( line );
	}
	
	public DialogueLine GetDialogueLine( int index )
	{
		return lines[index];
	}
	
	public string GetDialogueText( int index )
	{
		return lines[index].text;
	}
	
	public AudioClip GetAudioClip( int index )
	{
		return lines[index].audioClip;
	}
}
