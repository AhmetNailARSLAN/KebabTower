using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float money;
    public float Money { get => money; }

    public float foodCost;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

    }

    public void AddMoney(float amount)
    {
        money += amount;
    }
    public void SellFood(float amount)
    {
        // Yiyecekleri sat.
        money += amount * foodCost;
    }

    public void BuyUpgrade(Upgrade upgrade)
    {
        if (money >= upgrade.cost)
        {
            money -= upgrade.cost;
            upgrade.ApplyUpgrade();
        }
    }
}
