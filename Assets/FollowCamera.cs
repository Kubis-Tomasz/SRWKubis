using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public Transform target;
    public Vector3 offsetPosition;
    public bool lookAtCharacter = true;
    [Range(0,0.2F)]
    public float smoothSpeed = .01F;
    public Vector3 LookAtCoordinates;
    [Range(0,1)]
    public float position = 0F;

    private bool lookAtPrevious = true;

    private void Update()
    {
        if(target == null)
        {
            Debug.LogWarning("Missing target ref!", this);
            return;
        }

        if (lookAtPrevious != lookAtCharacter)
        {
            if (lookAtCharacter == true)
                position = 0;
            else
                position = 1;
        }

        transform.position = target.TransformPoint(offsetPosition);
        Vector3 smoothedPosition = Vector3.Lerp(LookAtCoordinates, target.position, position);
        transform.LookAt(smoothedPosition);

        if (lookAtCharacter == true)
        {
            if (position > 1)
                position = 1;
            if (position < 1)
                position += smoothSpeed;
        }
        else
        {
            if (position < 0)
                position = 0;
            if (position > 0)
                position -= smoothSpeed;
        }

        lookAtPrevious = lookAtCharacter;

        if (Input.GetKeyDown(KeyCode.Q))
            lookAtCharacter = !lookAtCharacter;

    }
}
