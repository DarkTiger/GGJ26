using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ProcessedIngredient
{
    public ItemName itemName;
    public ItemState ingredientState;
}

[CreateAssetMenu(fileName = "Recipe", menuName = "Data/Recipe")]
public class Recipe : ScriptableObject
{
    public List<ProcessedIngredient> ingredients;

    static bool Compare(List<ProcessedIngredient> mask, List<ProcessedIngredient> recipe)
    {
        List<ProcessedIngredient> tmpMask = new List<ProcessedIngredient>();
        tmpMask.AddRange(mask);

        if(mask.Count != recipe.Count)
            return false;

        foreach (ProcessedIngredient ing in recipe)
        {
            if (!tmpMask.Contains(ing))
                return false;
            else
                tmpMask.Remove(ing);
        }
        return true;
    }
    public bool CheckMask(List<ProcessedIngredient> mask)
    {
        List<ProcessedIngredient> tmpMask = new List<ProcessedIngredient>();
        tmpMask.AddRange(mask);

        if (mask.Count != ingredients.Count)
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

    public static List<Recipe> CheckRecipeAvailability(List<Recipe> allRecipes, List<ItemName> availableIngredients)
    {
        List<Recipe> availableRecipes = new List<Recipe>();

        foreach (Recipe recipe in allRecipes)
        {
            bool available = true;

            foreach(ProcessedIngredient ingr in recipe.ingredients)
            {
                if(!availableIngredients.Contains(ingr.itemName))
                    available = false;
                break;
            }

            if(available)
                availableRecipes.Add(recipe);
        }

        return availableRecipes;
    }
}

