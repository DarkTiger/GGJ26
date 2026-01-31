using System.Collections.Generic;
using UnityEngine;

public class MixingTable : Machine
{
    [SerializeField] List<ProcessedIngredient> ingredients;
    [SerializeField] GameObject maskPrefab;
    [SerializeField] Recipe wrongRecipe;
    public override void OnInteract(Player player)
    {
        print("Interacting with mixing table: " + name);

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
            {/*
                print("combining ingredients");

                ProcessedIngredient newProIng;
                newProIng.ingredient = (player.CurrentItem as Ingredient).data;
                newProIng.status = (player.CurrentItem as Ingredient).status;
                ingredients.Add(newProIng);

                List<Recipe> recipeList = new List<Recipe>();
                recipeList = Recipe.CheckMask(ingredients);
                
                GameObject itemObj = player.CurrentItem.gameObject;
                Destroy(itemObj);
                GameObject maskObj = Instantiate(maskPrefab);
                maskObj.transform.parent = transform;

                if (recipeList.Count == 1)
                {
                    maskObj.GetComponent<Mask>().recipe = recipeList[0];
                    maskObj.GetComponent<Mask>().UpdateSprite();
                }
                else
                {
                    maskObj.GetComponent<Mask>().recipe = wrongRecipe;
                    maskObj.GetComponent<Mask>().UpdateSprite();
                }*/
            }
            else
            {
                print("getting back item");

                ingredients.Clear();
                GiveItem(player);
            }
        }
    }
}
