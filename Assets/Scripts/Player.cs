using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] AudioClip grabClip;
    [SerializeField] AudioClip releaseClip;
    [SerializeField] float movementSpeed = 1f;

    public bool enableControls = true;

    public InputAction MoveAction { get; private set; }
    public InputAction InteractAction { get; private set; }
    public InputAction Interact2Action { get; private set; }
    public InputAction LookAction { get; private set; }
    public Interactable CurrentInteractable { get; set; }
    public Interactable CandidateInteractable { get; set; }
    public Item CurrentItem { get; set; }
    public bool Interating { get; private set; }
    public float LastVerticalValue { get; private set; } = 0f;
    public float LastHorizontalValue { get; private set; } = 0f;

    AudioSource footstepSource;
    PlayerInput playerInput;
    Rigidbody2D rb;
    Animator animator;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        footstepSource = GetComponent<AudioSource>();
        MoveAction = playerInput.actions["Move"];
        InteractAction = playerInput.actions["Interact"];
        Interact2Action = playerInput.actions["Interact2"];
        LookAction = playerInput.actions["Look"];

        transform.GetChild(0).gameObject.SetActive(playerInput.playerIndex == 0);
        transform.GetChild(1).gameObject.SetActive(playerInput.playerIndex == 1);
        animator = playerInput.playerIndex == 0? transform.GetChild(0).GetComponent<Animator>() : transform.GetChild(1).GetComponent<Animator>();

        FindFirstObjectByType<PlayerInputManager>().GetComponentInChildren<Canvas>().enabled = false;
    }

    private void FixedUpdate()
    {
        if(!enableControls)
            return;

        Vector2 moveValue = MoveAction.ReadValue<Vector2>();
        rb.AddForce(moveValue * movementSpeed, ForceMode2D.Impulse);
        LastHorizontalValue = moveValue.x;
        LastVerticalValue = moveValue.y;
        animator.SetFloat("Vertical", -LastVerticalValue);
        animator.SetFloat("Horizontal", LastHorizontalValue);

        if (moveValue.magnitude < 0.01f)
        {
            animator.SetFloat("Speed", 0f);
            footstepSource.volume = 0f;
        }
        else
        {
            animator.SetFloat("Speed", 0.75f);
            footstepSource.volume = 1f;

            if (CurrentItem)
            {
                CurrentItem.transform.localPosition = new Vector3((LastVerticalValue <= -0.1f? 0.5f : -0.1f) + LastHorizontalValue * 1.25f, LastVerticalValue + 0.5f, 0f);
            }
        }
    }

    private void Update()
    { 
        if (InteractAction.WasPressedThisFrame() && enableControls)
        {
            if (CurrentInteractable)
            {
                CurrentInteractable.OnDeInteract(this);
                AudioSource.PlayClipAtPoint(releaseClip, Camera.main.transform.position, 0.25f);
                CurrentInteractable = null;
                CandidateInteractable = null;
            }
            else if (CandidateInteractable)
            {
                if (CandidateInteractable is Item)
                {
                    CurrentItem = CandidateInteractable as Item;
                }

                AudioSource.PlayClipAtPoint(grabClip, Camera.main.transform.position, 0.25f);

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
            CurrentInteractable = CurrentItem;
        }
    }
}
