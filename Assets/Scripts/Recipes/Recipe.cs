using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Data/Recipe")]
public class Recipe : ScriptableObject
{
    public string recipeName = "Nome visibile";
    public Sprite sprite;
    [ColorUsage(showAlpha:false)]   public Color color = Color.white;
    public List<SO_Ingredient> ingredients;
    [Space(10)]
    public int value = 10;

    public bool CheckMask(List<SO_Ingredient> mask)
    {
        List<SO_Ingredient> tmpMask = new List<SO_Ingredient>();
        tmpMask.AddRange(mask);

        if (mask.Count != ingredients.Count)
            return false;

        foreach (SO_Ingredient ing in ingredients)
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

            foreach(SO_Ingredient ingr in recipe.ingredients)
            {
                if(!availableIngredients.Contains(ingr))
                    available = false;
                break;
            }

            if(available)
                availableRecipes.Add(recipe);
        }

        return availableRecipes;
    }
}