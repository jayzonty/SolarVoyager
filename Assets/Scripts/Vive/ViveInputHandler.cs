using UnityEngine;
using System.Collections;

public class ViveInputHandler : MonoBehaviour
{
    //public Transform playerTest;
	public PlayerController playerController;

    public SteamVR_TrackedController leftController;
    public SteamVR_TrackedController rightController;
	
	private uint recordingController;
	
	// TODO: Remove lol
	private Vector3 moveDirection;
	private bool isMoving = false;

	void Start()
	{
		// Register touch pad clicked event
		leftController.PadClicked += OnTouchPadClicked;
		rightController.PadClicked += OnTouchPadClicked;
		
		// Register touch pad unclicked event
		leftController.PadUnclicked += OnTouchPadUnclicked;
		rightController.PadUnclicked += OnTouchPadUnclicked;
		
		// Register menu clicked event
		leftController.MenuButtonClicked += OnMenuButtonClicked;
		rightController.MenuButtonClicked += OnMenuButtonClicked;
	}
	
	void Update()
	{
		if( isMoving )
		{
			PlayerController.instance.Move( moveDirection );
		}
	}
	
	void OnTouchPadClicked( object sender, ClickedEventArgs e )
	{
		if( ( -0.3f <= e.padX && e.padX <= 0.3f ) && ( -0.3f <= e.padY && e.padY <= 0.3f ) ) // Center button
		{
			if( GameState.CurrentQueryState == GameState.QueryState.Idle )
			{
				recordingController = e.controllerIndex;
				
				GameController.Instance.StartQuery();
			}
		}
		else if( ( e.padX < -0.3f ) && ( -0.3f <= e.padY && e.padY <= 0.3f ) ) // Left button
		{
			Debug.Log( "Left button" );
			if( UIManager.Instance.vrContextMenu.IsVisible )
			{
				UIManager.Instance.vrContextMenu.Close();
				
				GameObject[] planets = GameObject.FindGameObjectsWithTag( "Planet" );
				foreach( GameObject go in planets )
				{
					if( go.name.Equals( "Earth" ) )
					{
						PlayerController.instance.WarpToPlanet( go.GetComponentInChildren<BodyBehavior>().gameObject.transform );
						break;
					}
				}
			}
			else if( GameState.CurrentMode == GameState.Mode.Navigating )
			{
				if( GameState.navigationMode == GameState.NavigationMode.Follow )
				{
					GameState.navigationMode = GameState.NavigationMode.Free;
				}
				
				moveDirection = -Vector3.right;
				isMoving = true;
			}
		}
		else if( ( e.padX > 0.3f ) && ( -0.3f <= e.padY && e.padY <= 0.3f ) ) // Right button
		{
			Debug.Log( "Right button" );
			
			if( GameState.CurrentMode == GameState.Mode.Navigating )
			{
				if( GameState.navigationMode == GameState.NavigationMode.Follow )
				{
					GameState.navigationMode = GameState.NavigationMode.Free;
				}
				
				moveDirection = Vector3.right;
				isMoving = true;
			}
		}
		else if( ( -0.3f <= e.padX && e.padX <= 0.3f ) && ( e.padY < -0.3f ) ) // Down button
		{
			Debug.Log( "Down button" );
			
			if( GameState.CurrentMode == GameState.Mode.Navigating )
			{
				if( GameState.navigationMode == GameState.NavigationMode.Follow )
				{
					GameState.navigationMode = GameState.NavigationMode.Free;
				}
				
				moveDirection = -Vector3.forward;
				isMoving = true;
			}
		}
		else if( ( -0.3f <= e.padX && e.padX <= 0.3f ) && ( e.padY > 0.3f ) ) // Up button
		{
			Debug.Log( "Up button" );
			
			if( UIManager.Instance.vrContextMenu.IsVisible )
			{
				UIManager.Instance.vrContextMenu.Close();
				
				GameObject[] planets = GameObject.FindGameObjectsWithTag( "Planet" );
				foreach( GameObject go in planets )
				{
					if( go.name.Equals( "Earth" ) )
					{
						PlayerController.instance.Follow( go.GetComponentInChildren<BodyBehavior>().gameObject.transform );
						break;
					}
				}
			}
			else if( GameState.CurrentMode == GameState.Mode.Navigating )
			{
				if( GameState.navigationMode == GameState.NavigationMode.Follow )
				{
					GameState.navigationMode = GameState.NavigationMode.Free;
				}
				
				moveDirection = Vector3.forward;
				isMoving = true;
			}
		}
	}
	
	void OnTouchPadUnclicked( object sender, ClickedEventArgs e )
	{
		if( ( GameState.CurrentQueryState == GameState.QueryState.Recording ) && ( recordingController == e.controllerIndex ) )
		{
			GameController.Instance.FinalizeQuery();
		}
		
		isMoving = false;
	}
	
	void OnMenuButtonClicked( object sender, ClickedEventArgs e )
	{
		if( UIManager.Instance.vrContextMenu.IsVisible )
		{
			UIManager.Instance.vrContextMenu.Close();
		}
		else
		{
			UIManager.Instance.vrContextMenu.Show();
		}
	}
	
	bool IsLeftController( uint index )
	{
		return ( index == leftController.controllerIndex );
	}
	
	bool IsRightController( uint index )
	{
		return ( index == rightController.controllerIndex );
	}
}
