using UnityEngine;
using UnityEngine.InputSystem;

public class Box : Interactable
{
    [SerializeField] GameObject pieMenuGameobject;
    [SerializeField] PieMenu pieMenu;
    [SerializeField] Sprite boxClosed, boxOpen;
    [SerializeField] AudioClip boxClip;

    public bool IsOpen = false;
    Transform playerTransform;
    Player instigatorPlayer;

    public override void OnInteract(Player player)
    {
        instigatorPlayer = player;
        playerTransform = player.gameObject.transform;
        player.enableControls = false;
        player.Interact2Action.started += CloseMenu;
        base.OnInteract(player);
        if (!IsOpen)
        {
            AudioSource.PlayClipAtPoint(boxClip, Camera.main.transform.position, 0.5f);
            pieMenuGameobject.SetActive(true);
            IsOpen = true;
            pieMenu.SetPlayer(player);
        }
    }
    public override void OnDeInteract(Player player)
    {
        if (IsOpen)
        {
            CloseMenu(new InputAction.CallbackContext());
        }
        else
        {
            OnInteract(player);
        }
    }

    void CloseMenu(InputAction.CallbackContext ctx)
    {
        pieMenuGameobject.SetActive(false);
        IsOpen = false;
        pieMenu.RemovePlayer();
        instigatorPlayer.enableControls = true;
        instigatorPlayer.Interact2Action.performed -= CloseMenu;
    }

    private void Update()
    {
        if (IsOpen)
        {
            if (Vector2.Distance(transform.position, playerTransform.position) > 5f)
            {
                AudioSource.PlayClipAtPoint(boxClip, Camera.main.transform.position, 0.5f);
                pieMenuGameobject.SetActive(false);
                IsOpen = false;
                pieMenu.RemovePlayer();
            }
        }
    }

    void OnDisable()
    {
        if (instigatorPlayer)
            instigatorPlayer.Interact2Action.performed -= CloseMenu;
    }
}
