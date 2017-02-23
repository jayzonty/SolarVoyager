using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameController : MonoBehaviour
{
	private static GameController instance = null;
	public static GameController Instance
	{
		get
		{
			return instance;
		}
	}
	
	public VirtualAssistantBehaviour virtualAssistantBehaviour;
	
	public string target;
	
	public Transform playerTransform;
	
	public string gotoTarget;
	public string stateTarget;
	public string randomFactTarget;
	public string randomFactAttribute;
	
	private string[] Shuffle( string[] orig )
	{
		string[] ret = new string[orig.Length];
		Array.Copy( orig, ret, orig.Length );
		
		for( int i = 0; i < ret.Length; i++ )
		{
			string temp = ret[i];
			int r = UnityEngine.Random.Range( i, ret.Length );
			
			ret[i] = ret[r];
			ret[r] = temp;
		}
		
		return ret;
	}
	
	void Awake()
	{
		instance = this;
		
		// Shuffle planets list
		string[] shuffledPlanets = Shuffle( Constants.PLANET_KEYS );
		gotoTarget = shuffledPlanets[0];
		
		stateTarget = shuffledPlanets[1];
		
		int r = UnityEngine.Random.Range( 0, Constants.ATTRIBUTE_KEYS.Length );
		randomFactTarget = shuffledPlanets[2];
		randomFactAttribute = Constants.ATTRIBUTE_KEYS[r];
	}
	
	void Start()
	{
	}
	
	void Update()
	{
	}
	
	public GameObject GetPlanet( string planetName )
	{
		GameObject[] planets = GameObject.FindGameObjectsWithTag( "Planet" );
		foreach( GameObject go in planets )
		{
			if( string.Compare( go.name.ToLower(), planetName.ToLower() ) == 0 )
			{
				return go;
			}
		}
		
		return null;
	}
	
	public void StartQuery()
	{
		GameState.SetQueryState( GameState.QueryState.Recording );
		
		if( UIManager.Instance.dialogueWindow.IsVisible )
		{
			UIManager.Instance.dialogueWindow.Close();
		}
		
		if( UIManager.Instance.progressWindow != null && UIManager.Instance.progressWindow.gameObject.activeInHierarchy )
		{
			UIManager.Instance.progressWindow.Text = LocalizationManager.GetString( Messages.STATUS_RECORDING );
			UIManager.Instance.progressWindow.Show();
		}
		
		SpeechRecording.StartRecording();
	}
	
	public void FinalizeQuery()
	{
		SpeechRecording.StopRecording();
		
		AudioClip clip = SpeechRecording.GetMostRecentClip();
		
		// Uncomment to use sample query for testing purposes.
		//clip = Resources.Load( "Sounds/tell-me-about-earth-jp-tts" ) as AudioClip;
		//clip = Resources.Load( "Sounds/how-hot-is-planet-earth-tts" ) as AudioClip;
		//clip = Resources.Load( "Sounds/how-hot-is-this-planet-tts" ) as AudioClip;
		//clip = Resources.Load( "Sounds/how-big-is-planet-venus-tts" ) as AudioClip;
		//clip = Resources.Load( "Sounds/go-to-mars-tts" ) as AudioClip;
		
		// For now, consider "this" planet as the nearest planet from the player (euclid distance)
		BodyBehavior[] planets = GameObject.FindObjectsOfType<BodyBehavior>();
		BodyBehavior nearestPlanet = null;
		float minDist = float.MaxValue;
		foreach( BodyBehavior planet in planets )
		{
			float dist = Vector3.Distance( playerTransform.position, planet.transform.position );
			if( dist < minDist )
			{
				nearestPlanet = planet;
				minDist = dist;
			}
		}
		
		SpeechQueryParams speechQueryParams;
		if( nearestPlanet != null )
		{
			speechQueryParams.planet = nearestPlanet.planetName.ToLower();
		}
		else
		{
			speechQueryParams.planet = "none";
		}
		
		Debug.Log( "SpeechQueryParams: " + JsonUtility.ToJson( speechQueryParams ) );
		
		if( clip == null )
		{	
			Debug.LogError( "Something went wrong with the Microphone!" );
			
			GameState.SetQueryState( GameState.QueryState.Idle );
		}
		else
		{
			SpeechQueryHandler speechQueryHandler = new SpeechQueryHandler( clip, speechQueryParams );
			speechQueryHandler.SpeechQueryFinished += HandleSpeechQueryFinished;
			
			// Show a dialog box indicating that the system is currently querying.
			if( UIManager.Instance.progressWindow != null && UIManager.Instance.progressWindow.gameObject.activeInHierarchy )
			{
				UIManager.Instance.progressWindow.Text = LocalizationManager.GetString( Messages.STATUS_PROCESSING );
				//UIManager.Instance.progressWindow.Show();
			}
			
			GameState.SetQueryState( GameState.QueryState.Querying );
		
			StartCoroutine( speechQueryHandler.PerformQuery() );
		}
	}
	
	public void CancelQuery()
	{
		SpeechRecording.StopRecording();
	}
	
	private void HandleSpeechQueryFinished( string rawTextResponse, SpeechQueryResponse response )
	{
		// If there is a valid response...
		if( response != null )
		{
			if( string.Compare( response.queryType, "move" ) == 0 )
			{
				GameObject go = GetPlanet( response.targetPlanet );
				if( go != null )
				{
					if( string.Compare( response.targetPlanet, gotoTarget ) == 0 )
					{
						GameState.SetFlag( "gotoObjective" );
					}
					
					PlayerController.instance.WarpToPlanet( go.GetComponentInChildren<BodyBehavior>().transform );
				}
			}
			else
			{
				// Proceed with synthesizing the response to speech.
				SpeechSynthesisOptions options;
				
				//options.locale = "en-US";
				options.locale = "ja-JP";
				options.voiceType = SpeechSynthesisOptions.VoiceType.Female;
				
				SpeechSynthesisHandler speechSynthesisHandler = new SpeechSynthesisHandler( options );
				speechSynthesisHandler.SpeechSynthesisFinished += HandleSpeechSynthesisFinished;
				
				GameState.SetQueryState( GameState.QueryState.Synthesizing );
				
				StartCoroutine( speechSynthesisHandler.Synthesize( response.answer ) );
				
				if( ( string.Compare( response.queryType, "state" ) == 0 ) && ( string.Compare( response.targetPlanet, stateTarget ) == 0 ) )
				{
					GameState.SetFlag( "stateObjective" );
				}
				else if( ( string.Compare( response.queryType, randomFactAttribute ) == 0 ) && ( string.Compare( response.targetPlanet, randomFactTarget ) == 0 ) )
				{
					GameState.SetFlag( "randomFactObjective" );
				}
			}
		}
		
		GameState.SetQueryState( GameState.QueryState.Idle );
		
		UIManager.Instance.progressWindow.Close();
	}
	
	private void HandleSpeechSynthesisFinished( string transcription, AudioClip clip )
	{
		if( clip != null )
		{
			GameState.SetQueryState( GameState.QueryState.Idle );
			virtualAssistantBehaviour.Speak( transcription, clip );
		}
	}
}
