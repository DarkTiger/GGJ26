using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Box : Interactable
{
    [SerializeField] GameObject pieMenuGameobject;
    [SerializeField] PieMenu pieMenu;
    [SerializeField] Sprite boxClosed, boxOpen;
    public bool IsOpen = false;
    Transform playerTransform;
    public override void OnInteract(Player player)
    {
        playerTransform = player.gameObject.transform;
        base.OnInteract(player);
        if (!IsOpen)
        {
            pieMenuGameobject.SetActive(true);
            IsOpen = true;
            pieMenu.SetPlayer(player);
        }
    }
    public override void OnDeInteract(Player player)
    {
        if (IsOpen)
        {
            pieMenuGameobject.SetActive(false);
            IsOpen = false;
            pieMenu.RemovePlayer();
        }
    }

    private void Update()
    {
        if (IsOpen)
        {
            if(Vector2.Distance(transform.position, playerTransform.position) > 5f)
            {
                pieMenuGameobject.SetActive(false);
                IsOpen = false;
                pieMenu.RemovePlayer();
            }
        }
    }
}
