using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialSceneBehaviour : MonoBehaviour
{
	public ProgressWindowBehaviour progressWindow;
	public ProgressWindowBehaviour virtualAssistantProgressWindow;
	
	public DialogueWindowBehaviour dialogueWindow;
	
	public SteamVR_TrackedController leftController;
	
	public VirtualAssistantBehaviour virtualAssistantBehaviour;
	
	private bool tutorialStarted = false;
	
	private Dialogue dialogue1;
	private Dialogue dialogue2;
	private Dialogue dialogue3;
	
	private bool dialogueDone = false;
	
	private bool waitingForEarthSize = false;
	private bool waitingForGotoNeptune = false;
	
	void Start()
	{
		dialogue1 = new Dialogue();
		for( int i = 1; i <= 6; i++ )
		{
			dialogue1.AddDialogueLine( LocalizationManager.GetString( "tutorial_" + i ) );
		}
		
		dialogue2 = new Dialogue();
		for( int i = 7; i <= 10; i++ )
		{
			dialogue2.AddDialogueLine( LocalizationManager.GetString( "tutorial_" + i ) );
		}

		dialogue3 = new Dialogue();
		for( int i = 11; i <= 13; i++ )
		{
			dialogue3.AddDialogueLine( LocalizationManager.GetString( "tutorial_" + i ) );
		}
		
		leftController.MenuButtonClicked += OnMenuButtonClicked;
		
		leftController.PadClicked += OnTouchPadClicked;
		leftController.PadUnclicked += OnTouchPadUnclicked;
	}
	
	void Update()
	{
		if( !tutorialStarted )
		{
			if( leftController.gameObject.activeInHierarchy )
			{
				progressWindow.Close();
				
				tutorialStarted = true;
				
				StartCoroutine( DoTutorial() );
			}
			else
			{
				progressWindow.Text = LocalizationManager.GetString( "controllerWait" );
				progressWindow.Show();
			}
		}
	}
	
	private IEnumerator DoTutorial()
	{
		dialogueWindow.InitializeDialogue( dialogue1 );
		dialogueWindow.ShowNextLine();
		
		dialogueWindow.Show();
		
		dialogueDone = false;
		while( !dialogueDone )
		{
			yield return null;
		}
		
		// At this point, wait for user to ask for earth size.
		waitingForEarthSize = true;
		
		yield return null;
	}
	
	private IEnumerator SecondPart()
	{
		// Wait for earth size dialogue to finish.
		while( !dialogueDone )
		{
			yield return null;
		}
		
		dialogueWindow.InitializeDialogue( dialogue2 );
		dialogueWindow.ShowNextLine();
		
		dialogueWindow.Show();
		
		dialogueDone = false;
		while( !dialogueDone )
		{
			yield return null;
		}
		
		// At this point, wait for user to go to neptune.
		waitingForGotoNeptune = true;
		
		yield return null;
	}
	
	private IEnumerator FinishTutorial()
	{
		dialogueWindow.InitializeDialogue( dialogue3 );
		dialogueWindow.ShowNextLine();
		
		dialogueWindow.Show();
		
		dialogueDone = false;
		while( !dialogueDone )
		{
			yield return null;
		}
		
		// Load MainScene after dialogue
		SceneManager.LoadScene( "MainScene" );
		
		yield return null;
	}
	
	private void OnMenuButtonClicked( object sender, ClickedEventArgs e )
	{
		if( tutorialStarted )
		{
			if( dialogueWindow.gameObject.activeInHierarchy )
			{
				if( dialogueWindow.HasNextLine() )
				{
					dialogueWindow.ShowNextLine();
				}
				else
				{
					dialogueDone = true;
				}
			}
		}
	}
	
	private void OnTouchPadClicked( object sender, ClickedEventArgs e )
	{
		if( !waitingForEarthSize && !waitingForGotoNeptune )
		{
			return;
		}
		
		if( virtualAssistantProgressWindow.gameObject.activeInHierarchy )
		{
			virtualAssistantProgressWindow.Text = LocalizationManager.GetString( Messages.STATUS_RECORDING );
			virtualAssistantProgressWindow.Show();
		}
			
		SpeechRecording.StartRecording();
	}
	
	private void OnTouchPadUnclicked( object sender, ClickedEventArgs e )
	{
		if( !waitingForEarthSize && !waitingForGotoNeptune )
		{
			return;
		}
		
		SpeechRecording.StopRecording();
		
		AudioClip clip = SpeechRecording.GetMostRecentClip();
		
		SpeechQueryParams speechQueryParams;
		speechQueryParams.planet = "earth";
		
		if( clip == null )
		{	
			Debug.LogError( "Something went wrong with the Microphone!" );
		}
		else
		{
			SpeechQueryHandler speechQueryHandler = new SpeechQueryHandler( clip, speechQueryParams );
			speechQueryHandler.SpeechQueryFinished += HandleSpeechQueryFinished;
			
			// Show a dialog box indicating that the system is currently querying.
			if( virtualAssistantProgressWindow.gameObject.activeInHierarchy )
			{
				virtualAssistantProgressWindow.Text = LocalizationManager.GetString( Messages.STATUS_PROCESSING );
			}
			
			GameState.SetQueryState( GameState.QueryState.Querying );
		
			StartCoroutine( speechQueryHandler.PerformQuery() );
		}
	}
	
	private GameObject GetPlanet( string planetName )
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
	
	private void HandleSpeechQueryFinished( string rawTextResponse, SpeechQueryResponse response )
	{
		// If there is a valid response...
		if( response != null )
		{
			if( string.Compare( response.queryType, "move" ) == 0 )
			{
				if( waitingForGotoNeptune && ( string.Compare( response.targetPlanet, "neptune" ) == 0 ) )
				{
					waitingForGotoNeptune = false;
					
					GameObject go = GetPlanet( response.targetPlanet );
					if( go != null )
					{	
						PlayerController.instance.WarpToPlanet( go.GetComponentInChildren<BodyBehavior>().transform );
					}
					
					StartCoroutine( FinishTutorial() );
				}
			}
			else if( ( string.Compare( response.queryType, "size" ) == 0 ) && ( string.Compare( response.targetPlanet, "earth" ) == 0 ) )
			{
				waitingForEarthSize = false;
				
				StartCoroutine( SecondPart() );
			}
		}
		
		virtualAssistantProgressWindow.Close();
	}
}
