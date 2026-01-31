using UnityEngine;

public class Well : Interactable
{
    public override void OnInteract(Player player)
    {
        if (player.CandidateInteractable == this && player.CurrentItem && player.CurrentItem is Bucket)
        {
            (player.CurrentItem as Bucket).SetFull(true);
        }
    }
}
