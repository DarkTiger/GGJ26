using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;

    public InputAction MoveAction { get; private set; }
    public InputAction InteractAction { get; private set; }
    public InputAction Interact2Action { get; private set; }
    public InputAction LookAction { get; private set; }
    public Interactable CurrentInteractable { get; set; }
    public Interactable CandidateInteractable { get; set; }
    public Item CurrentItem { get; set; }
    public bool Interating { get; private set; }

    PlayerInput playerInput;
    Rigidbody2D rb;
    


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        MoveAction = playerInput.actions["Move"];
        InteractAction = playerInput.actions["Interact"];
        Interact2Action = playerInput.actions["Interact2"];
        LookAction = playerInput.actions["Look"];

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
                CandidateInteractable = null;
            }
            else if (CandidateInteractable)
            {
                if (CandidateInteractable is Item)
                {
                    CurrentItem = CandidateInteractable as Item;
                }

                CurrentInteractable = CandidateInteractable;
                CandidateInteractable.OnInteract(this);
                CandidateInteractable = null;
            }
            else if (CurrentItem)
            {
                CurrentItem.OnDeInteract(this);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Interactable interactable))
        {
            if (!interactable.InteractedBy)           
            {
                CandidateInteractable = interactable;
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
            CandidateInteractable = null;
        }
    }
}
