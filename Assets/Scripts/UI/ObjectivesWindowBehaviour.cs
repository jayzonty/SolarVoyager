using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ObjectivesWindowBehaviour : PopupWindowBehaviour
{
	public Toggle gotoObjectiveToggle;
	public Toggle generalFactsObjectiveToggle;
	public Toggle randomFactObjectiveToggle;
	
	public string gotoTarget;
	private string generalFactsTarget;
	private string randomFactTarget;
	private string randomFactAttribute;
	
	protected override void OnAwake()
	{
		base.OnAwake();
		
		GameState.FlagStateChanged += HandleFlagStateChanged;
	}
	
	protected override void OnStart()
	{
		Debug.Log( LocalizationManager.Instance.GetString( "gotoObjectiveTemplate" ) );
		
		gotoTarget = "mars";
		generalFactsTarget = "earth";
		randomFactTarget = "jupiter";
		randomFactAttribute = "temperature";
		
		
		Show();
	}
	
	private void HandleFlagStateChanged( string flag, bool oldValue, bool newValue )
	{
		if( string.Compare( flag, "gotoObjective" ) == 0 )
		{
			gotoObjectiveToggle.isOn = newValue;
		}
		else if( string.Compare( flag, "generalFactsObjective" ) == 0 )
		{
			generalFactsObjectiveToggle.isOn = newValue;
		}
		else if( string.Compare( flag, "randomFactObjective" ) == 0 )
		{
			randomFactObjectiveToggle.isOn = newValue;
		}
	}
}
