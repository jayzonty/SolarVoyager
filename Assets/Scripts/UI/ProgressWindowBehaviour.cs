using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ProgressWindowBehaviour : PopupWindowBehaviour
{
	private Text progressText;

	protected override void OnAwake()
	{
		base.OnAwake();
		
		progressText = GetComponentInChildren<Text>();
		if( progressText == null )
		{
			Debug.LogError( "Progress text is null!" );
		}
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
