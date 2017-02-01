using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public Dropdown planetsDropdown;
	
	public DialogueCanvasBehaviour dialogueCanvas;
	
	private static UIManager instance;
	public static UIManager Instance
	{
		get
		{
			return instance;
		}
	}
	
	public void OnQueryButtonPressed()
	{
		if( GameState.CurrentQueryState == GameState.QueryState.Idle )
		{
			GameController.Instance.StartQuery();
		}
		else if( GameState.CurrentQueryState == GameState.QueryState.Recording )
		{
			GameController.Instance.FinalizeQuery();
		}
	}
	
	public void ShowDialogueBox()
	{
		dialogueCanvas.gameObject.SetActive( true );
	}
	
	public void HideDialogueBox()
	{
		dialogueCanvas.Close();
		
		dialogueCanvas.gameObject.SetActive( false );
	}
	
	void Awake()
	{
		instance = this;
		
		dialogueCanvas.gameObject.SetActive( false );
	}
	
	void Start()
	{
	}
	
	void Update()
	{	
	}
}
