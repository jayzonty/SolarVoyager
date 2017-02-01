using UnityEngine;
using System.Collections;

public delegate void QueryStateChanged( GameState.QueryState oldState, GameState.QueryState newState );

public class GameState
{
    public enum NavigationMode
    {
        Free, Follow
    }
	
	public enum QueryState
	{
		Idle, Recording, Querying, Synthesizing
	}
	
	public enum Mode
	{
		Navigating, Dialogue
	}
    
    public static NavigationMode navigationMode = NavigationMode.Free;
	
	private static QueryState queryState = QueryState.Idle;
	
	private static Mode currentMode = Mode.Navigating;
	
	public static QueryState CurrentQueryState
	{
		get
		{
			return queryState;
		}
	}
	
	public static void SetQueryState( QueryState state )
	{
		queryState = state;
		
		Debug.Log( "Query State Changed: " + state.ToString() );
	}
	
	public static Mode CurrentMode
	{
		get
		{
			return currentMode;
		}
	}
	
	public static void SetCurrentMode( Mode mode )
	{
		currentMode = mode;
		
		Debug.Log( "Current mode changed to " + mode.ToString() );
	}
}
