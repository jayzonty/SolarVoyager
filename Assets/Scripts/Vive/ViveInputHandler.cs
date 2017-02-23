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
	
	private LineRenderer leftControllerLineRenderer;
	
	void Awake()
	{
		leftControllerLineRenderer = leftController.GetComponent<LineRenderer>();
	}

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
		if( leftControllerLineRenderer != null )
		{
			leftControllerLineRenderer.enabled = isMoving;
			leftControllerLineRenderer.SetPosition( 0, leftController.transform.position );
			leftControllerLineRenderer.SetPosition( 1, leftController.transform.position + leftController.transform.forward * 20.0f );
		}
		
		if( isMoving )
		{	
			PlayerController.instance.Move( leftController.transform.forward );
		}
	}
	
	void OnTriggerClicked( object sender, ClickedEventArgs e )
	{
		if( IsLeftController( e.controllerIndex ) )
		{
			isMoving = true;
			
			GameState.navigationMode = GameState.NavigationMode.Free;
		}
		
	}
	
	void OnTriggerUnclicked( object sender, ClickedEventArgs e )
	{
		if( IsLeftController( e.controllerIndex ) )
		{
			isMoving = false;
		}
	}
	
	void OnTouchPadClicked( object sender, ClickedEventArgs e )
	{
		if( IsLeftController( e.controllerIndex ) )
		{
			if( GameState.CurrentQueryState == GameState.QueryState.Idle )
			{
				GameController.Instance.StartQuery();
			}
		}
	}
	
	void OnTouchPadUnclicked( object sender, ClickedEventArgs e )
	{
		if( IsLeftController( e.controllerIndex ) )
		{
			if( GameState.CurrentQueryState == GameState.QueryState.Recording )
			{
				GameController.Instance.FinalizeQuery();
			}
		}
	}
	
	void OnMenuButtonClicked( object sender, ClickedEventArgs e )
	{
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
