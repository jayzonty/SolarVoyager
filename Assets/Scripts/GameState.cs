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
    
    public static NavigationMode navigationMode = NavigationMode.Free;
	
	private static QueryState queryState = QueryState.Idle;
	
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
}
