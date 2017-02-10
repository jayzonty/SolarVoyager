using UnityEngine;

public class KeyboardInputHandler : MonoBehaviour
{
    public bool invertY = true;

    public float minLookY = -60.0f;
    public float maxLookY = 60.0f;
    
    public bool lockCursor = true;
    
    public float lookSpeed = 30.0f;
    
    private Vector3 lookRotation;
    
	void Start()
	{
        if( lockCursor )
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        lookRotation = PlayerController.instance.GetEulerAngles();
	}
	
	void Update()
	{
        float invertValue = 1;
        if( invertY )
        {
            invertValue = -1;
        }
        
        // Handle rotation
        lookRotation.x += Input.GetAxis( "Mouse Y" ) * lookSpeed * Time.deltaTime * invertValue;
        lookRotation.x = Mathf.Clamp( lookRotation.x, minLookY, maxLookY );
        lookRotation.y += Input.GetAxis( "Mouse X" ) * lookSpeed * Time.deltaTime;
        
        PlayerController.instance.SetEulerAngles( lookRotation );
            
		// Handle position
		Vector3 movement = new Vector3();
		movement.x = Input.GetAxisRaw( "Horizontal" );
		movement.z = Input.GetAxisRaw( "Vertical" );
            
		if( movement.x != 0.0f || movement.z != 0.0f )
		{
			PlayerController.instance.Move( movement );
			
			if( GameState.navigationMode == GameState.NavigationMode.Follow )
			{
				GameState.navigationMode = GameState.NavigationMode.Free;
			}
		}
        
		if( Input.GetKeyDown( KeyCode.Space ) )
		{
			Cursor.lockState = ( Cursor.lockState == CursorLockMode.Locked ) ? CursorLockMode.None : CursorLockMode.Locked;
		}
		
		if( Input.GetKeyDown( KeyCode.Return ) )
		{
			if( UIManager.Instance.dialogueCanvas.HasNextLine() )
			{
				UIManager.Instance.dialogueCanvas.ShowNextLine();
			}
			else
			{
				UIManager.Instance.HideDialogueBox();
			}
		}
		
		if( Input.GetKeyDown( KeyCode.V ) )
		{
			VirtualAssistantBehaviour virtualAssistant = GameController.Instance.virtualAssistantBehaviour;
			if( virtualAssistant != null )
			{
				if( virtualAssistant.IsVisible )
				{
					virtualAssistant.Hide();
				}
				else
				{
					virtualAssistant.Show();
				}
			}
		}
	}
}
