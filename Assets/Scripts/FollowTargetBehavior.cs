using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetBehavior : MonoBehaviour
{
    public Transform target;

    public float smoothing = 2.0f;

    public Vector3 offset;

    public bool manualOffset = false;

    // Use this for initialization
    void Start()
    {
        if( !manualOffset && ( target != null ) )
        {
            offset = target.position - transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if( target != null )
        {
            float step = smoothing * Time.deltaTime;
            transform.position = Vector3.MoveTowards( target.position, target.position + offset, step );
        }
    }
}
