using UnityEngine;

public class Mask : Item
{
    public Recipe recipe;

    private void Awake()
    {
        UpdateSprite();
    }

    public void UpdateSprite()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = recipe.sprite;
    }

    public override void OnDeInteract(Player player)
    {
        if (player.CandidateInteractable is Goblin)
        {
            return;
        }
        base.OnDeInteract(player);
    }
}
