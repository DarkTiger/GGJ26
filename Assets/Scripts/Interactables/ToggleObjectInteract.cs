using UnityEngine;

public class ToggleObjectInteract : Interactable
{
    [SerializeField] GameObject target;
    public override void OnInteract(Player player)
    {
        target?.SetActive(!target.activeSelf);
    }

    public override void OnDeInteract(Player player)
    {
        OnInteract(player);
    }
}