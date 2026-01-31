using System.Collections.Generic;
using UnityEngine;

public class MixingTable : Machine
{
    [SerializeField] List<SO_Ingredient> ingredients;
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


                ingredients.Add((player.CurrentItem as Ingredient).data);

                IngestItem(player);
                
                player.CurrentItem.OnDeInteract(player);
                itemInside = player.CurrentItem.gameObject;
                itemInside.transform.parent = transform;
                itemInside.transform.localPosition = Vector3.zero;
                player.CurrentItem = null;
                player.CurrentInteractable = null;
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

                ingredients.Add((player.CurrentItem as Ingredient).data);
                List<Recipe> recipeList = new List<Recipe>();
                recipeList = Recipe.CheckRecipeAvailability(GameManager.Instance.availableRecipe, ingredients);
                
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
                }
            }
            else
            {
                print("getting back item");
                itemInside.GetComponent<Item>().OnInteract(player);
                player.CurrentItem = itemInside.GetComponent<Item>();
                player.CurrentInteractable = itemInside.GetComponent<Item>();
            }
        }
    }
}
