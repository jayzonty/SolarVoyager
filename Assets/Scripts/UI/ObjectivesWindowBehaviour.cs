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
		
		// TODO: Generate random targets
		gotoTarget = "mars";
		generalFactsTarget = "earth";
		randomFactTarget = "jupiter";
		randomFactAttribute = "temperature";
		
		gotoObjectiveToggle.GetComponentInChildren<Text>().text = string.Format( LocalizationManager.Instance.GetString( Messages.GOTO_OBJECTIVE_TEMPLATE ), gotoTarget );
		generalFactsObjectiveToggle.GetComponentInChildren<Text>().text = string.Format( LocalizationManager.Instance.GetString( Messages.GENERAL_FACTS_OBJECTIVE_TEMPLATE ), generalFactsTarget );
		randomFactObjectiveToggle.GetComponentInChildren<Text>().text = string.Format( LocalizationManager.Instance.GetString( Messages.RANDOM_FACT_OBJECTIVE_TEMPLATE ), randomFactAttribute, randomFactTarget );
		
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
