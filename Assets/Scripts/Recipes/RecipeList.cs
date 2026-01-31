using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RecipeList", menuName = "Data/RecipeList")]
public class RecipeList : ScriptableObject
{
    [SerializeField] public List<Recipe> recipeList;
}
