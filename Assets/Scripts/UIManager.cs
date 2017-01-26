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
