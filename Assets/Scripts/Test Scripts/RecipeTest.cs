using System.Collections.Generic;
using UnityEngine;

public class RecipeTest : MonoBehaviour
{
    public List<SO_Ingredient> availableItems;
    public RecipeList allRecipes;
    public List<Recipe> availableRecipes;
    public bool check = false;
    public bool partialRecipe = false;
    public List<ProcessedIngredient> ingrList;

    // Update is called once per frame
    void Update()
    {
        if(check)
        {
            check = false;
            //availableRecipes = Recipe.CheckRecipeAvailability(allRecipes.recipeList, availableItems);
            print(Recipe.GetRecipeFromIngredients(ingrList, out partialRecipe));
        }
    }
}