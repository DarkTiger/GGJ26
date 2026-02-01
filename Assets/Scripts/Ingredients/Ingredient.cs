using UnityEngine;

public class Ingredient : Item
{
    public SO_Ingredient data;
    SpriteRenderer spriteRenderer;
    [SerializeField] public IngredientStatus status;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateSprite();
    }

    public void ChangeState(IngredientStatus state)
    {
        status = state;
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

    public void UpdateSprite()
    {
        switch(status)
        {
            case IngredientStatus.BASE:
                spriteRenderer.sprite = data.baseSprite;
                break;
            case IngredientStatus.COOKED:
                spriteRenderer.sprite = data.cookedSprite;
                break;
            case IngredientStatus.POWDER:
                spriteRenderer.sprite = data.powderSprite;
                break;
        }
    }
}