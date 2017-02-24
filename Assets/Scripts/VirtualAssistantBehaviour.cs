using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualAssistantBehaviour : MonoBehaviour
{
	private Animator animator;
	
	public enum AnimationType
	{
		Idle, Yes, No, Wakarimasen, Thanks
	}
	
	private AnimationType animation = AnimationType.Idle;
	public AnimationType Animation
	{
		get
		{
			return animation;
		}
		set
		{
			animation = value;
		}
	}
	
	void Awake()
	{
		animator = GetComponentInChildren<Animator>();
	}
	
    void Start()
    {
    }

    void Update()
    {
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
		
		if( animator != null )
		{
			animator.SetTrigger( animation.ToString() );
		}
	}
}
