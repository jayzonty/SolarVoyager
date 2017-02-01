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
	
	public void StartQuery()
	{
		GameState.SetQueryState( GameState.QueryState.Recording );
		
		SpeechRecording.StartRecording();
	}
	
	public void FinalizeQuery()
	{
		SpeechRecording.StopRecording();
		
		AudioClip clip = SpeechRecording.GetMostRecentClip();
		
		// Uncomment to use sample query for testing purposes.
		//clip = Resources.Load( "Sounds/how-big-is-planet-mars" ) as AudioClip;
		//clip = Resources.Load( "Sounds/how-hot-is-planet-earth-tts" ) as AudioClip;
		//clip = Resources.Load( "Sounds/how-hot-is-this-planet-tts" ) as AudioClip;
		
		SpeechQueryParams speechQueryParams;
		speechQueryParams.planet = target;
		
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
			// Proceed with synthesizing the response to speech.
			SpeechSynthesisOptions options;
			
			//options.locale = "en-US";
			options.locale = "ja-JP";
			options.voiceType = SpeechSynthesisOptions.VoiceType.Female;
			
			SpeechSynthesisHandler speechSynthesisHandler = new SpeechSynthesisHandler( options );
			speechSynthesisHandler.SpeechSynthesisFinished += HandleSpeechSynthesisFinished;
			
			GameState.SetQueryState( GameState.QueryState.Synthesizing );
			
			StartCoroutine( speechSynthesisHandler.Synthesize( response.response ) );
		}
		else
		{
			GameState.SetQueryState( GameState.QueryState.Idle );
		}
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
