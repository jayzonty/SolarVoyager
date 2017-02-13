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
	
	private CanvasGroup canvasGroup;
	private Animator animator;
	
	void Awake()
	{
		textField = GetComponentInChildren<Text>();
		if( textField == null )
		{
			Debug.LogError( "DialogCanvas must have a child with a Text component!" );
		}
		
		canvasGroup = GetComponent<CanvasGroup>();
		animator = GetComponent<Animator>();
	}
	
	void Start()
	{
	}
	
	void Update()
	{
	}
	
	public void Show()
	{
		Show( true );
	}
	
	public void Show( bool animate )
	{
		if( animate )
		{
			animator.SetTrigger( "MenuShowTrigger" );
		}
		else
		{
			canvasGroup.alpha = 1.0f;
		}
	}
	
	public void Close()
	{
		Close( true );
	}
	
	public void Close( bool animate )
	{
		if( audioSource != null )
		{
			audioSource.Stop();
		}
		
		if( animate )
		{
			animator.SetTrigger( "MenuHideTrigger" );
		}
		else
		{
			canvasGroup.alpha = 0.0f;
		}
	}
	
	public bool IsVisible
	{
		get
		{
			return ( canvasGroup.alpha > 0.0f );
		}
	}
	
	public float Alpha
	{
		get
		{
			return canvasGroup.alpha;
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
	
	protected void OnShow()
	{
	}
	
	protected void OnHide()
	{
	}
}
