using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Transform[] Reactors { get => reactors; set => reactors = value; }

    private new Rigidbody rigidbody;
    private PlayerTargetSystem targetSystem;

    private float speed;

    private Transform[] reactors;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        targetSystem = GetComponent<PlayerTargetSystem>();
        speed = PlayerManager.Instance.PlayerData.speed.levelValues[PlayerManager.Instance.CurrentUpgradeLevels.speed];
    }

    void FixedUpdate()
    {
        if (!SessionManager.Instance.IsGameStarted && SessionManager.Instance.IsGamePaused)
        {
            return;
        }

        Move();
        if (targetSystem.IsAiming && targetSystem.TargetEnemy != null)
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
        {
            rigidbody.velocity = Vector3.zero;
            return;
        }
        
        rigidbody.MovePosition(transform.position + InputManager.Instance.MovementInput.normalized * Time.fixedDeltaTime * speed);
    }

    private void LookForward()
    {
        if (!IsMoving())
            return;

        Quaternion rotation = Quaternion.LookRotation(InputManager.Instance.MovementInput, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.3f);

        RotateReactors(rotation);
    }

    private void LookAtTarget()
    {
        Vector3 targetDirection = targetSystem.TargetEnemy.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.7f);

        if (IsMoving())
        {
            Quaternion newReactorRotation = Quaternion.LookRotation(InputManager.Instance.MovementInput, Vector3.up);
            RotateReactors(newReactorRotation);
        }
    }

    private void RotateReactors(Quaternion rotation)
    {
        foreach (Transform reactor in reactors)
        {
            reactor.rotation = Quaternion.Lerp(reactor.rotation, rotation, 0.05f);
        }
    }
}