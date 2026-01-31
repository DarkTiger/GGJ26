using UnityEngine;

public class Ingredient : Item
{
    public SO_Ingredient data;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeState(IngredientStatus state)
    {
        data.currentStatus = state;
    }

    public override void OnInteract(Player player)
    {
        base.OnInteract(player);
    }

    public override void OnDeInteract(Player player)
    {
        if(player.CandidateInteractable is Machine)
        {
            player.CandidateInteractable.OnInteract(player);
        }
        else if(player.CandidateInteractable == null)
        {
            base.OnDeInteract(player);
        }
    }

    public override void Use(Player player)
    {
        base.Use(player);
    }
}