using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;

    public InputAction MoveAction { get; private set; }
    public InputAction InteractAction { get; private set; }
    public Interactable CurrentInteractable { get; private set; }

    PlayerInput playerInput;
    Rigidbody2D rb;
    public Interactable candidateInteractable;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        MoveAction = playerInput.actions["Move"];
        InteractAction = playerInput.actions["Interact"];

        transform.GetChild(0).gameObject.SetActive(playerInput.playerIndex == 0);
        transform.GetChild(1).gameObject.SetActive(playerInput.playerIndex == 1);

        FindFirstObjectByType<PlayerInputManager>().GetComponentInChildren<Canvas>().enabled = false;
    }

    private void FixedUpdate()
    {
        rb.AddForce(MoveAction.ReadValue<Vector2>() * movementSpeed, ForceMode2D.Impulse);
    }

    private void Update()
    { 
        if (InteractAction.WasPressedThisFrame())
        {
            if (CurrentInteractable)
            {
                CurrentInteractable.OnDeInteract(this);
                CurrentInteractable = null;
                candidateInteractable = null;
            }
            else if (candidateInteractable)
            {
                candidateInteractable.OnInteract(this);
                CurrentInteractable = candidateInteractable;
                candidateInteractable = null;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Interactable interactable))
        {
            if (!interactable.InteractedBy)           
            {
                candidateInteractable = interactable;
                interactable.OnEntering(this);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Interactable interactable))
        {
            interactable.OnExit(this);
            interactable.InteractedBy = null;
            candidateInteractable = null;
        }
    }
}
