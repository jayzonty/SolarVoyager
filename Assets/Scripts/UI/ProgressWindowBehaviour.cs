using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ProgressWindowBehaviour : MonoBehaviour
{
	private Text progressText;
	
	private Animator animator;

	void Awake()
	{
		progressText = GetComponentInChildren<Text>();
		if( progressText == null )
		{
			Debug.LogError( "Progress text is null!" );
		}
		
		animator = GetComponent<Animator>();
		if( animator == null )
		{
			Debug.LogError( "Animator is null!" );
		}
	}
	
	void Start()
	{	
	}
	
	void Update()
	{
	}
	
	public void Show()
	{
		animator.SetTrigger( "MenuShowTrigger" );
	}
	
	public void Close()
	{
		animator.SetTrigger( "MenuHideTrigger" );
	}
	
	public string Text
	{
		get
		{
			return progressText.text;
		}
		
		set
		{
			progressText.text = value;
		}
	}
}
