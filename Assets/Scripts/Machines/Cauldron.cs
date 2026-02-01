using System.Net.NetworkInformation;
using UnityEngine;

public class Cauldron : Machine
{
    public override IngredientStatus finalState => IngredientStatus.COOKED;

    protected override void UseWorkingSprite()
    {
        Sprite targetSprite = itemInside.GetComponent<Ingredient>().data.cauldronSprite;

        if (targetSprite == null)
            return;

        spriteRenderer.sprite = targetSprite;
    }

    public override void FinishWorking()
    {
        popUp.Show();
        Ingredient ing = itemInside.GetComponent<Ingredient>();
        popUp.UpdateFG(ing.data.GetSprite(IngredientStatus.COOKED));
    }
}
