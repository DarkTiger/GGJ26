using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ProcessedIngredient
{
    public SO_Ingredient ingredient;
    public IngredientStatus status;
}

[CreateAssetMenu(fileName = "Recipe", menuName = "Data/Recipe")]
public class Recipe : ScriptableObject
{
    public string recipeName = "Nome visibile";
    public Sprite sprite;
    [ColorUsage(showAlpha:false)]   public Color color = Color.white;
    public List<ProcessedIngredient> ingredients;
    [Space(10)]
    public int value = 10;

    public bool CheckMask(List<ProcessedIngredient> ingrList)
    {
        List<ProcessedIngredient> tmpMask = new List<ProcessedIngredient>();
        tmpMask.AddRange(ingrList);

        if (ingrList.Count != ingredients.Count)
            return false;

        foreach (ProcessedIngredient ing in ingredients)
        {
            if (!tmpMask.Contains(ing))
                return false;
            else
                tmpMask.Remove(ing);
        }
        return true;
    }

    public static List<Recipe> CheckRecipeAvailability(List<Recipe> allRecipes, List<SO_Ingredient> availableIngredients)
    {
        List<Recipe> availableRecipes = new List<Recipe>();

        foreach (Recipe recipe in allRecipes)
        {
            bool available = true;

            foreach(ProcessedIngredient ingr in recipe.ingredients)
            {
                if(!availableIngredients.Contains(ingr.ingredient))
                    available = false;
                break;
            }

            if(available)
                availableRecipes.Add(recipe);
        }

        return availableRecipes;
    }

    public static Recipe GetRecipeFromIngredients(List<ProcessedIngredient> ingrList)
    {
        if(GameManager.Instance == null)
            return null;

        List<ProcessedIngredient> tmpIngrList = new List<ProcessedIngredient>();

        foreach(Recipe recipe in GameManager.Instance.recipeList.recipeList)
        {
            tmpIngrList.Clear();
            tmpIngrList.AddRange(ingrList);

            if (ingrList.Count == recipe.ingredients.Count)
            {
                foreach (ProcessedIngredient correctIngr in recipe.ingredients)
                {
                    if (tmpIngrList.Contains(correctIngr))
                    {
                        tmpIngrList.Remove(correctIngr);
                    }
                }
                if (tmpIngrList.Count == 0)
                    return recipe;
            }
        }
        return null;
    }

    public static Recipe GetRecipeFromIngredients(List<ProcessedIngredient> ingrList, out bool partialRecipe)
    {
        if (GameManager.Instance == null)
        {
            partialRecipe = false; return null;
        }

        List<ProcessedIngredient> tmpIngrList = new List<ProcessedIngredient>();

        foreach (Recipe recipe in GameManager.Instance.recipeList.recipeList)
        {
            tmpIngrList.Clear();
            tmpIngrList.AddRange(ingrList);

            foreach (ProcessedIngredient correctIngr in recipe.ingredients)
            {
                if (tmpIngrList.Contains(correctIngr))
                {
                    tmpIngrList.Remove(correctIngr);
                }
            }
            if (tmpIngrList.Count == 0)
            {
                partialRecipe = ingrList.Count == recipe.ingredients.Count ? false : true;
                return recipe;
            }
        }
        partialRecipe = false; return null;
    }
}