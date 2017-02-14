using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public Dropdown planetsDropdown;
	public DialogueCanvasBehaviour dialogueCanvas;
	
	public VRContextMenu vrContextMenu;
	
	public ProgressWindowBehaviour progressWindow;
	
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
		if( ( dialogueCanvas != null ) && ( !dialogueCanvas.IsVisible ) )
		{
			dialogueCanvas.Show();
		}
	}
	
	public void HideDialogueBox()
	{
		if( dialogueCanvas != null )
		{
			dialogueCanvas.Close();
		}
	}
	
	public bool IsVRContextMenuVisible()
	{
		return ( ( vrContextMenu != null ) && ( vrContextMenu.IsVisible ) );
	}
	
	public void ShowVRContextMenu()
	{
		if( vrContextMenu != null )
		{
			vrContextMenu.Show();
		}
	}
	
	public void HideVRContextMenu()
	{
		if( vrContextMenu != null )
		{
			vrContextMenu.Hide();
		}
	}
	
	void Awake()
	{
		instance = this;
	}
	
	void Start()
	{
		if( dialogueCanvas != null )
		{
			dialogueCanvas.Close( false );
		}
		
		if( vrContextMenu != null )
		{
			vrContextMenu.Hide();
		}
	}
	
	void Update()
	{	
	}
}
