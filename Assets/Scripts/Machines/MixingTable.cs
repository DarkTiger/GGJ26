using System.Collections.Generic;
using UnityEngine;

public class MixingTable : Machine
{
    [SerializeField] List<ProcessedIngredient> ingredients;
    [SerializeField] GameObject maskPrefab;
    [SerializeField] Recipe wrongRecipe;
    [SerializeField] Sprite workingSprite;

    bool recipeReady;

    public override void OnInteract(Player player)
    {
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("WAIT"))
            return;

        if(player.CurrentItem == null && recipeReady)
        {
            recipeReady = false;

            ingredients.Clear();
            popUp.Hide();
            GiveItem(player);
        }

        if (ingredients.Count == 0)
        {
            if (player.CurrentItem != null && player.CurrentItem is Ingredient)
            {
                print("adding ingredient:" + (player.CurrentItem as Ingredient).data);

                ProcessedIngredient newProIng;
                newProIng.ingredient = (player.CurrentItem as Ingredient).data;
                newProIng.status = (player.CurrentItem as Ingredient).status;
                ingredients.Add(newProIng);
                IngestItem(player);
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
                animator.Play("WORK");

                ProcessedIngredient newProIng;
                newProIng.ingredient = (player.CurrentItem as Ingredient).data;
                newProIng.status = (player.CurrentItem as Ingredient).status;
                ingredients.Add(newProIng);

                Recipe recipe = Recipe.GetRecipeFromIngredients(ingredients, out bool partial);
                if(recipe == null)
                {
                    foreach (Transform child in transform)
                    {
                        if (child.gameObject.GetComponent<PopUp>() || child.gameObject.GetComponent<Mask>())
                            continue;
                        Destroy(child.gameObject);
                    }
                    ingredients.Clear();
                    return;
                }

                if (!partial)
                {
                    recipeReady = true;

                    foreach(Transform child in transform)
                    {
                        if (child.gameObject.GetComponent<PopUp>())
                            continue;
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
                    maskObj.gameObject.SetActive(false);
                    maskObj.GetComponent<Mask>().recipe = recipe? recipe: wrongRecipe;
                    maskObj.GetComponent<Mask>().UpdateSprite();
                }
            }
            else
            {
                //print("getting back item");
                //ingredients.Clear();
                //popUp.Hide();
                //GiveItem(player);
            }
        }
    }

    public override void FinishWorking()
    {
        Mask mask = itemInside.GetComponent<Mask>();

        if(mask == null)
        {
            animator.Play("WAIT");
            popUp.Hide();
            return;
        }

        popUp.Show();
        popUp.UpdateFG(mask.recipe.sprite);
        
        if(ingredients.Count == 0)
        {
            spriteRenderer.sprite = initialSprite;
        }
    }

    protected override void UseWorkingSprite()
    {
        if (workingSprite == null)
            return;

        spriteRenderer.sprite = workingSprite;
    }
}
