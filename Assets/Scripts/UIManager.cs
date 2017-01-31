using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public Dropdown planetsDropdown;
	
	private static UIManager instance;
	
	public static UIManager Instance
	{
		get
		{
			return instance;
		}
	}
	
	public void DebugQueryButtonPressed()
	{
		
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
	
	void Awake()
	{
		instance = this;
	}
	
	// Use this for initialization
	void Start()
	{
	}
	
	// Update is called once per frame
	void Update()
	{	
	}
}
