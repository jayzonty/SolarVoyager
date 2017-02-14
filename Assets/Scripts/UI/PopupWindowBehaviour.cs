using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( CanvasGroup ) )]
[RequireComponent( typeof( Animator ) )]
public class PopupWindowBehaviour : IWindowBehaviour
{
	public const string WINDOW_SHOW_TRIGGER = "WindowShowTrigger";
	public const string WINDOW_HIDE_TRIGGER = "WindowHideTrigger";
	
	protected CanvasGroup canvasGroup;
	protected Animator animator;
	
	protected override void OnAwake()
	{
		animator = GetComponent<Animator>();
		if( animator == null )
		{
			Debug.LogError( "RAWR" );
		}
		
		canvasGroup = GetComponent<CanvasGroup>();
		if( canvasGroup == null )
		{
			Debug.LogError( "ASDF" );
		}
	}
	
	protected override void OnStart()
	{
	}
	
	protected override void OnUpdate()
	{
	}
	
	protected override void OnShow( bool animate )
	{
		animator.enabled = animate;
		
		if( animate )
		{
			animator.SetTrigger( WINDOW_SHOW_TRIGGER );
		}
		else
		{
			canvasGroup.alpha = 1.0f;
		}
	}
	
	protected override void OnClose( bool animate )
	{
		animator.enabled = animate;
		
		if( animate )
		{
			animator.SetTrigger( WINDOW_HIDE_TRIGGER );
		}
		else
		{
			canvasGroup.alpha = 0.0f;
		}
	}
}
