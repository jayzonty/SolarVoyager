﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public Dropdown planetsDropdown;
	
	public DialogueWindowBehaviour dialogueWindow;
	public ProgressWindowBehaviour progressWindow;
	public ObjectivesWindowBehaviour objectivesWindow;
	
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
	
	void Awake()
	{
		instance = this;
	}
	
	void Start()
	{
	}
	
	void Update()
	{
	}
}
