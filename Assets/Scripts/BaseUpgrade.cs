using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUpgrade : MonoBehaviour
{
    public static Action<BaseUpgrade, int> OnUpgrade;

    [Header("Upgrades")]
    [SerializeField] protected float capasityMultiplier;
    [SerializeField] protected float moveSpeedMultiplier;
    [SerializeField] protected float waitTimeReducer;
    [SerializeField] protected float makeAmountMultiplier;
    [SerializeField] protected float cookingSpeedMultiplier;

    [Header("Costs")]
    [SerializeField] private float initialUpgradeCost;
    [SerializeField] private float upgradeCostMultiplier;

    protected Elevator elevator;
    protected Waiter waiter;
    protected Chef chef;
    protected Courier courier;

    public int CurrentLevel { get; set; }
    public float UpgradeCost { get; set; }
    public float CapasityMultiplier => capasityMultiplier;
    public float MoveSpeedMultiplier => moveSpeedMultiplier;
    public float WaitTimeReducer => waitTimeReducer;
    public float MakeAmountMultiplier => makeAmountMultiplier;
    public float CookingSpeedMultiplier => cookingSpeedMultiplier;
    public float UpgradeCostMultiplier => upgradeCostMultiplier;

    public Elevator Elevator => elevator;
    public Courier Courier => courier;
    public Chef Chef => chef;
    public Waiter Waiter => waiter;



    private void Start()
    {
        elevator = GetComponent<Elevator>();
        chef = GetComponent<Chef>();    
        waiter = GetComponent<Waiter>();    
        courier = GetComponent<Courier>();    

        CurrentLevel = 1;
        UpgradeCost = initialUpgradeCost;
    }

    public virtual void Upgrade(int upgradeAmount)
    {
        if (upgradeAmount > 0)
        {
            for (int i = 0; i < upgradeAmount; i++)
            {
                UpgradeSuccess();
                UpdateUpgradeValues();
                RunUpgrade();
            }
        }

    }

    protected virtual void UpgradeSuccess()
    {
        if (MoneyManager.instance.CurrentMoney >= UpgradeCost)
        {
            MoneyManager.instance.RemoveMoney((int)UpgradeCost);
            CurrentLevel++;
            OnUpgrade?.Invoke(this, CurrentLevel);
        }
        else
        {
            // UI Managerdean para yetersiz uyarýsý çðaýr
            Debug.Log("Para yetersiz");
        }


    }

    protected virtual void UpdateUpgradeValues()
    {
        // Deðerleri güncelle
        UpgradeCost *= upgradeCostMultiplier;
    }

    protected virtual void RunUpgrade()
    {
        // Upgrade Logic
    }


}
