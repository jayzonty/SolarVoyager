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
            
        if( GameState.navigationMode == GameState.NavigationMode.Free )
        {
            // Handle position
            Vector3 movement = new Vector3();
            movement.x = Input.GetAxisRaw( "Horizontal" );
            movement.z = Input.GetAxisRaw( "Vertical" );
            
            PlayerController.instance.Move( movement );
        }
        
		if( Input.GetKeyDown( KeyCode.Space ) )
		{
			Cursor.lockState = ( Cursor.lockState == CursorLockMode.Locked ) ? CursorLockMode.None : CursorLockMode.Locked;
		}
	}
}
