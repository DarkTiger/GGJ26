using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;

    public InputAction MoveAction { get; private set; }
    public InputAction InteractAction { get; private set; }

    PlayerInput playerInput;
    Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        MoveAction = playerInput.actions["Move"];
        InteractAction = playerInput.actions["Interact"];  
    }

    private void FixedUpdate()
    {
        rb.AddForce(MoveAction.ReadValue<Vector2>() * movementSpeed, ForceMode2D.Impulse);
    }
}
