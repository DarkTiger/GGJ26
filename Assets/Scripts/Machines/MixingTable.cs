using System.Collections.Generic;
using UnityEngine;

public class MixingTable : Machine
{
    [SerializeField] List<ProcessedIngredient> ingredients;
    [SerializeField] GameObject maskPrefab;
    [SerializeField] Recipe wrongRecipe;
    [SerializeField] Sprite workingSprite;
    public override void OnInteract(Player player)
    {
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("WAIT"))
            return;

        if (ingredients.Count == 0)
        {
            if (player.CurrentItem != null && player.CurrentItem is Ingredient)
            {
                print("adding ingredient:" + (player.CurrentItem as Ingredient).data);

                ProcessedIngredient newProIng;
                newProIng.ingredient = (player.CurrentItem as Ingredient).data;
                newProIng.status = (player.CurrentItem as Ingredient).status;
                ingredients.Add(newProIng);
                IngestItem(player, true);
            }
            else
            {
                print("not carrying ingredient!");
                // avvisare player?
            }
        }
        else
        {
            if (player.CurrentItem != null && player.CurrentItem is Ingredient)
            {
                print("combining ingredients");

                ProcessedIngredient newProIng;
                newProIng.ingredient = (player.CurrentItem as Ingredient).data;
                newProIng.status = (player.CurrentItem as Ingredient).status;
                ingredients.Add(newProIng);

                Recipe recipe = Recipe.GetRecipeFromIngredients(ingredients, out bool partial);

                if (!partial)
                {
                    foreach(Transform child in transform)
                    {
                        Destroy(child.gameObject);
                    }
                    
                    Destroy(player.CurrentItem.gameObject);
                    player.CurrentItem = null;
                    player.CurrentInteractable = null;

                    GameObject maskObj = Instantiate(maskPrefab);
                    itemInside = maskObj;
                    itemInside.GetComponent<Collider2D>().enabled = false;

                    maskObj.transform.parent = transform;
                    maskObj.transform.position = transform.position;
                    maskObj.GetComponent<Mask>().recipe = recipe? recipe: wrongRecipe;
                    maskObj.GetComponent<Mask>().UpdateSprite();
                }
            }
            else
            {
                print("getting back item");

                ingredients.Clear();
                GiveItem(player);
            }
        }
    }

    protected override void UseWorkingSprite()
    {
        if (workingSprite == null)
            return;

        spriteRenderer.sprite = workingSprite;
    }
}
