using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public delegate void OnFlagStateChanged( string flag, bool oldValue, bool newValue );

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
	
	private static HashSet<string> flags;
	
	public static event OnFlagStateChanged FlagStateChanged;
	
	static GameState()
	{
		flags = new HashSet<string>();
	}
	
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
	
	public static void SetFlag( string flag )
	{
		if( !GetFlag( flag ) )
		{
			flags.Add( flag );
			
			if( FlagStateChanged != null )
			{
				FlagStateChanged( flag, false, true );
			}
		}
	}
	
	public static void UnsetFlag( string flag )
	{
		if( GetFlag( flag ) )
		{
			flags.Remove( flag );
			
			if( FlagStateChanged != null )
			{
				FlagStateChanged( flag, true, false );
			}
		}
	}
	
	public static bool GetFlag( string flag )
	{
		return flags.Contains( flag );
	}
	
	public static void ClearFlags()
	{
		flags.Clear();
	}
}
