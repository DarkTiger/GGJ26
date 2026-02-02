using System.Net.NetworkInformation;
using UnityEngine;

public class Cauldron : Machine
{
    protected override IngredientStatus finalState => IngredientStatus.COOKED;

    protected override void UseWorkingSprite()
    {
        Sprite targetSprite = itemInside.GetComponent<Ingredient>().data.cauldronSprite;

        if (targetSprite == null)
            return;

        spriteRenderer.sprite = targetSprite;
    }
}
