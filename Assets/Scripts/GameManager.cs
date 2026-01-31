using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int money,happyGoblin,angryGoblin;

    public bool IsGameOver = false;

    [SerializeField]public List<Recipe> availableRecipe;
    [SerializeField] public RecipeList recipeList;
    [SerializeField] List<SO_Ingredient> so_ingredient;

    private void Awake()
    {
        if (Instance != null) { Destroy(Instance); } else { Instance = this; }

        availableRecipe = new List<Recipe>();
        SetAvaiableRecipe();
    }

    private void Start()
    {
        //so_ingredient = new List<SO_Ingredient>();
        SetAvaiableRecipe(); //Test
    }

    public int GetMoney()
    {
        return money;
    }

    public void IncreaseMoney(int value)
    {
        money += value;
        TopBarUI.Instance.UpdateTopBar();
    }

    public void DecreaseMoney(int value)
    {
        money -= value;
        TopBarUI.Instance.UpdateTopBar();
    }

    public void AddHappyGoblin()
    {
        happyGoblin++;
        TopBarUI.Instance.UpdateTopBar();
    }

    public int GetHappy()
    {
        return happyGoblin;
    }

    public void AddAngryGoblin()
    {
        angryGoblin++;
        TopBarUI.Instance.UpdateTopBar();
    }

    public int GetAngry()
    {
        return angryGoblin;
    }

    public void GameOver()
    {
        IsGameOver = true;
    }

    public void AddNewIngredient(SO_Ingredient ingredient)
    {
        so_ingredient.Add(ingredient);
        SetAvaiableRecipe();
    }

    public void SetAvaiableRecipe()
    {
        availableRecipe = Recipe.CheckRecipeAvailability(recipeList.recipeList, so_ingredient);
    }
     public List<Recipe> GetAvaiableRecipe()
    {
        return availableRecipe;
    }
}
