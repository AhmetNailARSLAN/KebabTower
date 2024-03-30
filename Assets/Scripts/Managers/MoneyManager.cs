using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;

    [SerializeField] private int testMoney;
    [SerializeField] private int foodPrice;

    public int CurrentMoney { get; set; }
    public int FoodPrice { get => foodPrice; }
    private readonly string MONEY_KEY = "MoneyKey";

    private void Awake()
    {
        instance = this;
        PlayerPrefs.DeleteAll();
    }

    private void Start()
    {
        LoadMoney();
    }

    private void LoadMoney()
    {
        CurrentMoney = PlayerPrefs.GetInt(MONEY_KEY, testMoney);
    }

    public void AddMoney(int amount)
    {
        CurrentMoney += amount;
        PlayerPrefs.SetInt(MONEY_KEY, CurrentMoney);
        PlayerPrefs.Save();
    }

    public void RemoveMoney(int amount)
    {
        if (amount < CurrentMoney)
        {
            CurrentMoney -= amount;
            PlayerPrefs.SetInt(MONEY_KEY, CurrentMoney);
            PlayerPrefs.Save();
        }
        else
        {
            // UI Managerdean para yetersiz uyarýsý çðaýr
            Debug.Log("Para yetersiz");
        }

    }

    public void SellFood(float amount)
    {
        AddMoney((int)(amount *= FoodPrice));
    }


}
