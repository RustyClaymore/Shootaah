using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector3 movementInput;
    private Vector3 mousePos;
    private bool attackButtonPressed;

    public static InputManager Instance { get; private set; }

    public Vector3 MovementInput { get => movementInput; }
    public Vector3 MousePos { get => mousePos; }
    public bool AttackButtonPressed { get => attackButtonPressed; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        movementInput.x = Input.GetAxis("Horizontal");
        movementInput.z = Input.GetAxis("Vertical");

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        attackButtonPressed = Input.GetKey(KeyCode.Space);
    }
}