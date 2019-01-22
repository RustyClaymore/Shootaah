using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector3 movementInput;
    private Vector3 mousePos;
    private bool mouseButton1Clicked;

    public static InputManager Instance { get; private set; }

    public Vector3 MovementInput { get => movementInput; }
    public Vector3 MousePos { get => mousePos; }
    public bool MouseButton1Clicked { get => mouseButton1Clicked; }

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

        mouseButton1Clicked = Input.GetMouseButtonDown(0);
    }
}