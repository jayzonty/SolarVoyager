using UnityEngine;
using System.Collections;

public class WandController : SteamVR_TrackedController
{
    public SteamVR_Controller.Device Controller
    {
        get
        {
            return SteamVR_Controller.Input( (int)controllerIndex );
        }
    }

    public Vector3 Velocity
    {
        get
        {
            return Controller.velocity;
        }
    }

    public Vector3 AngularVelocity
    {
        get
        {
            return Controller.angularVelocity;
        }
    }

    public float GetTriggerAxis()
    {
        if( Controller == null )
        {
            return 0.0f;
        }

        return Controller.GetAxis( Valve.VR.EVRButtonId.k_EButton_Axis1 ).x;
    }

    public Vector2 GetTouchpadAxis()
    {
        if( Controller == null )
        {
            return Vector2.zero;
        }

        return Controller.GetAxis( Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad );
    }

	// TODO: Uncomment?
	/*protected override void Start()
	{
        base.Start();
	}
	
	protected override void Update()
	{
        base.Update();
	}*/
}
