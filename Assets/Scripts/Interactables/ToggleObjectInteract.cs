using UnityEngine;

public class ToggleObjectInteract : Interactable
{
    [SerializeField] GameObject target;

    Player instigatorPlayer;

    public override void OnInteract(Player player)
    {
        instigatorPlayer = player;
        target?.SetActive(!target.activeSelf);
    }

    public override void OnDeInteract(Player player)
    {
        instigatorPlayer = player;
        OnInteract(player);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponentInParent<Player>() == instigatorPlayer)
        {
            target.SetActive(false);
        }
    }
}