using UnityEngine;

public class Mortar : Machine
{
    protected override IngredientStatus finalState => IngredientStatus.POWDER;

    protected override void UseWorkingSprite()
    {
        Sprite targetSprite = itemInside.GetComponent<Ingredient>().data.mortarSprite;
        
        if (targetSprite == null)
            return;

        spriteRenderer.sprite = targetSprite;
    }
}