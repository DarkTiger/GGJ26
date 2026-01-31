using UnityEngine;

public class Bucket : Item
{
    public override void Use(Player player)
    {
        Debug.Log("Bucket used");   
        
        base.Use(player);
    }

    public override void OnDeInteract(Player player)
    {
        Debug.Log("Bucket released");

        if (player.CandidateInteractable is CampSlot) return;

        base.OnDeInteract(player);
    }
}