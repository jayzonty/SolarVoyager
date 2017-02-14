using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IWindowBehaviour : MonoBehaviour
{
	protected bool isVisible;
	public bool IsVisible
	{
		get
		{
			return isVisible;
		}
	}
	
	void Awake()
	{
		OnAwake();
	}
	
	void Start()
	{
		OnStart();
	}
	
	void Update()
	{
		OnUpdate();
	}
	
	public void Show( bool animate = true )
	{
		if( isVisible )
		{
			return;
		}
		
		isVisible = true;
		
		OnShow( animate );
	}
	
	public void Close( bool animate = true )
	{
		if( !isVisible )
		{
			return;
		}
		
		isVisible = false;
		
		OnClose( animate );
	}
	
	protected abstract void OnAwake();
	protected abstract void OnStart();
	protected abstract void OnUpdate();
	
	protected abstract void OnShow( bool animate );
	protected abstract void OnClose( bool animate );
}
