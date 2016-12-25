using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour
{
    public enum NavigationMode
    {
        Free, Follow
    }
    
    public static NavigationMode navigationMode = NavigationMode.Free;
    
	void Start()
	{
	}
	
	void Update()
	{
	}
}
