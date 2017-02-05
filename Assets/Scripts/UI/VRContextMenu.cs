using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class VRContextMenu : MonoBehaviour
{
	private CanvasGroup canvasGroup;
	private bool isVisible;
	
	void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();
	}
	
	void Start()
	{
	}
	
	void Update()
	{	
	}
	
	public bool IsVisible
	{
		get
		{
			return isVisible;
		}
	}
	
	public void Show()
	{
		if( canvasGroup != null )
		{
			canvasGroup.alpha = 1.0f;
			canvasGroup.blocksRaycasts = true;
			
			isVisible = true;
		}
	}
	
	public void Hide()
	{
		if( canvasGroup != null )
		{
			canvasGroup.alpha = 0.0f;
			canvasGroup.blocksRaycasts = false;
		
			isVisible = false;
		}
	}
}
