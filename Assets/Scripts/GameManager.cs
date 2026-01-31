using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int money,happyGoblin,angryGoblin;

    public bool IsGameOver = false;

    [SerializeField]public List<Recipe> availableRecipe;

    private void Awake()
    {
        if (Instance != null) { Destroy(Instance); } else { Instance = this; }

        availableRecipe = new List<Recipe>();
    }

    public int GetMoney()
    {
        return money;
    }

    public void IncreaseMoney(int value)
    {
        money += value;
    }

    public void DecreaseMoney(int value)
    {
        money -= value;
    }

    public void AddHappyGoblin()
    {
        happyGoblin++;
    }

    public int GetHappy()
    {
        return happyGoblin;
    }

    public void AddAngryGoblin()
    {
        angryGoblin++;
    }

    public int GetAngry()
    {
        return angryGoblin;
    }

    public void GameOver()
    {
        IsGameOver = true;
    }
}
