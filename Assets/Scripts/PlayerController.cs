using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance = null;

    private Camera playerCamera = null;
	
    public float followSpeed = 50.0f;

    public Transform followTarget;

    private AudioSource audioSource;
	
	private MovementBehaviour movementBehaviour;
    
    public void Move( Vector3 direction )
    {	
		if( movementBehaviour != null )
		{
			movementBehaviour.Move( playerCamera.transform.localToWorldMatrix * direction );
		}
    }

    public void DebugFollow()
    {
		int index = UIManager.Instance.planetsDropdown.value;
		string planet = UIManager.Instance.planetsDropdown.options[index].text;
		
		GameObject[] planets = GameObject.FindGameObjectsWithTag( "Planet" );
		foreach( GameObject go in planets )
		{
			if( go.name.Equals( planet ) )
			{
				Follow( go.GetComponentInChildren<BodyBehavior>().gameObject.transform );
				break;
			}
		}
        //Follow( debugTargetFollow );
    }

    public void Follow( Transform target )
    {
        followTarget = target;
        GameState.navigationMode = GameState.NavigationMode.Follow;
    }
	
	public void DebugWarp()
	{
		int index = UIManager.Instance.planetsDropdown.value;
		string planet = UIManager.Instance.planetsDropdown.options[index].text;
		
		GameObject[] planets = GameObject.FindGameObjectsWithTag( "Planet" );
		foreach( GameObject go in planets )
		{
			if( go.name.Equals( planet ) )
			{
				WarpToPlanet( go.GetComponentInChildren<BodyBehavior>().gameObject.transform );
				break;
			}
		}
	}
	
	public void WarpToPlanet( Transform target )
	{
		BodyBehavior body = target.gameObject.GetComponentInChildren<BodyBehavior>();
		if( body != null )
		{
			Vector3 targetPos = body.transform.position + body.GetOffsetVector();
			transform.position = targetPos;

			Follow( target );
		}
	}
    
    public Vector3 GetEulerAngles()
    {
        return playerCamera.transform.localEulerAngles;
    }

    // Note: Do not set the euler angles directly using transform.localEulerAngles
    // TODO: Implement a better way to do this.
    public void SetEulerAngles( Vector3 eulerAngles )
    {
        playerCamera.transform.localEulerAngles = eulerAngles;
    }
    
    void Awake()
    {
        // TODO: Implement a better way to do this
        if( instance == null )
        {
            instance = this;
        }
        else
        {
            Debug.LogError( "There should only be one instance of PlayerController!" );
        }

        playerCamera = GetComponentInChildren<Camera>();

        audioSource = GetComponentInChildren<AudioSource>();
		
		movementBehaviour = GetComponent<MovementBehaviour>();
    }

	void Start()
	{
	}
 
	void Update ()
	{
        if( ( GameState.navigationMode == GameState.NavigationMode.Follow ) && ( followTarget != null ) )
        {
			BodyBehavior body = followTarget.gameObject.GetComponentInChildren<BodyBehavior>();
			if( body != null )
			{
				float step = followSpeed * Time.deltaTime;
				Vector3 targetPos = body.transform.position + body.GetOffsetVector();

				transform.position = Vector3.MoveTowards( transform.position, targetPos, step );
			}
        }
	}
}
