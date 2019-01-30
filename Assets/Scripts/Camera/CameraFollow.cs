using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float smoothSpeed;
    private Vector3 offset;
    private Transform playerTarget;

    void Start()
    {
        smoothSpeed = 0.2f;
        offset = new Vector3(50, 80, -150);

        playerTarget = SessionManager.Instance.CurrentPlayer.transform;

        transform.position = playerTarget.position + offset;
        transform.LookAt(playerTarget);
    }

    private void Update()
    {
        if (SessionManager.Instance.IsGameStarted && !SessionManager.Instance.IsGameEnded)
        {
            offset = Vector3.Lerp(offset, new Vector3(0, 35, -20), 0.02f);
        }
        else if (SessionManager.Instance.IsGameEnded)
        {
            offset = Vector3.Lerp(offset, new Vector3(-50, 5, -5), 0.02f);
        }
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = playerTarget.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(playerTarget);
    }
}
