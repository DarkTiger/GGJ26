using System.Collections.Generic;
using UnityEngine;

public class RecipeTest : MonoBehaviour
{
    public List<ItemName> availableItems;
    public RecipeList allRecipes;
    public List<Recipe> availableRecipes;
    public bool check = false;

    // Update is called once per frame
    void Update()
    {
        if(check)
        {
            check = false;
            availableRecipes = Recipe.CheckRecipeAvailability(allRecipes.recipeList, availableItems);
        }
    }
}