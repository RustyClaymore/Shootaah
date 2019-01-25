using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothSpeed = 0.2f;
    public Vector3 offset;

    private Transform playerTarget;

    void Start()
    {
        playerTarget = SessionManager.Instance.CurrentPlayer.transform;
    }
    
    void FixedUpdate()
    {
        Vector3 desiredPosition = playerTarget.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(playerTarget);
    }
}
