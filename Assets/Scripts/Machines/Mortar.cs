using UnityEngine;

public class Mortar : Machine
{
    public override IngredientStatus finalState => IngredientStatus.POWDER;

    protected override void UseWorkingSprite()
    {
        Sprite targetSprite = itemInside.GetComponent<Ingredient>().data.mortarSprite;
        
        if (targetSprite == null)
            return;

        spriteRenderer.sprite = targetSprite;
    }
    public override void FinishWorking()
    {
        popUp.Show();
        Ingredient ing = itemInside.GetComponent<Ingredient>();
        popUp.UpdateFG(ing.data.GetSprite(IngredientStatus.POWDER));
    }
}