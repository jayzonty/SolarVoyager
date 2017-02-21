using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ObjectivesWindowBehaviour : PopupWindowBehaviour
{
	public Toggle gotoObjectiveToggle;
	public Toggle generalFactsObjectiveToggle;
	public Toggle randomFactObjectiveToggle;
	
	protected override void OnAwake()
	{
		base.OnAwake();
		
		GameState.FlagStateChanged += HandleFlagStateChanged;
	}
	
	protected override void OnStart()
	{	
		gotoObjectiveToggle.GetComponentInChildren<Text>().text = string.Format( LocalizationManager.GetString( Messages.GOTO_OBJECTIVE_TEMPLATE ), GameController.Instance.gotoTarget );
		generalFactsObjectiveToggle.GetComponentInChildren<Text>().text = string.Format( LocalizationManager.GetString( Messages.GENERAL_FACTS_OBJECTIVE_TEMPLATE ), GameController.Instance.generalFactsTarget );
		randomFactObjectiveToggle.GetComponentInChildren<Text>().text = string.Format( LocalizationManager.GetString( Messages.RANDOM_FACT_OBJECTIVE_TEMPLATE ), GameController.Instance.randomFactAttribute, GameController.Instance.randomFactTarget );
		
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
