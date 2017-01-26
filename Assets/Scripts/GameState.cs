using UnityEngine;
using System.Collections;

public class GameState
{
    public enum NavigationMode
    {
        Free, Follow
    }
    
    public static NavigationMode navigationMode = NavigationMode.Free;
}
