using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private PlayerDataSO playerData;
    private new Rigidbody rigidbody;
    private PlayerTargetSystem targetSystem;

    public PlayerDataSO PlayerData { get => playerData; set => playerData = value; }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        targetSystem = GetComponent<PlayerTargetSystem>();
    }

    void FixedUpdate()
    {
        Move();
        if (targetSystem.IsAiming)
        {
            LookAtTarget();
        }
        else
        {
            LookForward();
        }
    }

    public bool IsMoving()
    {
        return !InputManager.Instance.MovementInput.Equals(Vector3.zero);
    }

    private void Move()
    {
        if (!IsMoving())
            return;

        rigidbody.MovePosition(transform.position + InputManager.Instance.MovementInput.normalized * Time.fixedDeltaTime * playerData.speed);
    }

    private void LookForward()
    {
        if (!IsMoving())
            return;

        Quaternion rotation = Quaternion.LookRotation(InputManager.Instance.MovementInput, Vector3.up);
        transform.rotation = rotation;

    }

    private void LookAtTarget()
    {
        Vector3 targetDirection = targetSystem.TargetEnemy.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        transform.rotation = rotation;
    }
}