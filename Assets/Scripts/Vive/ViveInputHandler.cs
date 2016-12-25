using UnityEngine;
using System.Collections;

public class ViveInputHandler : MonoBehaviour
{
    public Transform playerTest;

    public WandController leftController;
    public WandController rightController;

	void Start()
	{
	}
	
	void Update()
	{
        Vector3 movement = new Vector3();

        if( playerTest != null )
        {
            if( leftController.triggerPressed )
            {
            }

            if( rightController.triggerPressed )
            {
            }
        }
	}
}
