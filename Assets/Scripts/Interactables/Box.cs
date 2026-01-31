using UnityEditor;
using UnityEngine;

public class Box : Interactable
{
    [SerializeField] GameObject pieMenuGameobject;
    [SerializeField] PieMenu pieMenu;
    [SerializeField] Sprite boxClosed, boxOpen;
    public bool IsOpen = false;
    public override void OnInteract(Player player)
    {
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
}
