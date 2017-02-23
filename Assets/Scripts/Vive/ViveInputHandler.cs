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
		
		// Register trigger clicked event
		leftController.TriggerClicked += OnTriggerClicked;
		rightController.TriggerClicked += OnTriggerClicked;
		
		// Register trigger unclicked event
		leftController.TriggerUnclicked += OnTriggerUnclicked;
		rightController.TriggerUnclicked += OnTriggerUnclicked;
	}
	
	void Update()
	{
		if( isMoving )
		{
			moveDirection.x = leftController.controllerState.rAxis0.x;
			moveDirection.z = leftController.controllerState.rAxis0.y;
			moveDirection.y = 0.0f;
			
			PlayerController.instance.Move( moveDirection );
		}
	}
	
	void OnTriggerClicked( object sender, ClickedEventArgs e )
	{
		if( IsLeftController( e.controllerIndex ) )
		{
			if( GameState.CurrentQueryState == GameState.QueryState.Idle )
			{
				GameController.Instance.StartQuery();
			}
		}
	}
	
	void OnTriggerUnclicked( object sender, ClickedEventArgs e )
	{
		if( IsLeftController( e.controllerIndex ) )
		{
			if( GameState.CurrentQueryState == GameState.QueryState.Recording )
			{
				GameController.Instance.FinalizeQuery();
			}
		}
	}
	
	void OnTouchPadClicked( object sender, ClickedEventArgs e )
	{
		if( IsLeftController( e.controllerIndex ) )
		{
			isMoving = true;
			
			GameState.navigationMode = GameState.NavigationMode.Free;
		}
	}
	
	void OnTouchPadUnclicked( object sender, ClickedEventArgs e )
	{
		if( IsLeftController( e.controllerIndex ) )
		{
			isMoving = false;
		}
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
