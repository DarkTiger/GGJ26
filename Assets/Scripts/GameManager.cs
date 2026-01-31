using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int money;

    private void Awake()
    {
        if (Instance != null) { Destroy(Instance); } else { Instance = this; }
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
}
