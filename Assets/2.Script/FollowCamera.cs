using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lib;

    public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;// The target to follow
    
    [SerializeField] private bool useFixedUpdate = false; // Use FixedUpdate for smoother physics-based movement
    private void LateUpdate()
    {
        if (useFixedUpdate)
        {
            // Use FixedUpdate for smoother physics-based movement
            FollowTarget();
        }
    }
    void FollowTarget()
    {
        if (target == null) return; // If no target, do nothing
        Vector3 desiredPosition = target.position + Data.CameraOffSet;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, Data.CameraSpeed);
        transform.position = smoothedPosition;
    }
}
