using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private TextMeshProUGUI panelTitle;
    [SerializeField] private GameObject[] stats;
    [SerializeField] private Image panelIcon;
    

    [Header("Buttons")]
    [SerializeField] private GameObject[] upgradeCountButtons;

    [Header("Buttons Colors")]
    [SerializeField] private Color buttonEnabledColor;
    [SerializeField] private Color buttonDisabledColor;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI upgradeCostText;
    [SerializeField] private TextMeshProUGUI currentStat1;
    [SerializeField] private TextMeshProUGUI currentStat2;
    [SerializeField] private TextMeshProUGUI currentStat3;

    [Header("Upgraded")]
    [SerializeField] private TextMeshProUGUI statUpgraded1;
    [SerializeField] private TextMeshProUGUI statUpgraded2;
    [SerializeField] private TextMeshProUGUI statUpgraded3;

    [Header("Titles")]
    [SerializeField] private TextMeshProUGUI stat1title;
    [SerializeField] private TextMeshProUGUI stat2title;
    [SerializeField] private TextMeshProUGUI stat3title;

    [Header("Images")]
    [SerializeField] private Image stat1icon;
    [SerializeField] private Image stat2icon;
    [SerializeField] private Image stat3icon;

    [Header("Chef Icons")]
    [SerializeField] private Sprite chefIcon;
    [SerializeField] private Sprite cookingIcon;
    [SerializeField] private Sprite cookingSpeedIcon;

    [Header("Elevator Icons")]
    [SerializeField] private Sprite elevatorIcon;
    [SerializeField] private Sprite movementIcon;
    [SerializeField] private Sprite capasityIcon;
    [SerializeField] private Sprite waitSpeedIcon;

    private BaseUpgrade currentUpgrade;
    private Worker currentworker;

    public int TimesToUpgrade { get; set; }


    void Start()
    {
        ActivateButton(0);
        TimesToUpgrade = 1;
    }

    public void Upgrade()
    {
        if (MoneyManager.instance.CurrentMoney >= currentUpgrade.UpgradeCost)
        {
            currentUpgrade.Upgrade(TimesToUpgrade);
            if (currentUpgrade is ChefUpgrade)
            {
                UpdateChefPanel(currentUpgrade);
            }
            else if (currentUpgrade is ElevatorUpgrade)
            {
                UpdateElevatorPanel(currentUpgrade);
            }
            else if (currentUpgrade is CourierUpgrade)
            {
                UpdateCourierPanel(currentUpgrade);
            }
            else if (currentUpgrade is WaiterUpgrade)
            {
                UpdateWaiterPanel(currentUpgrade);
            }
        }
    }

    public void OpenUpgradePanel(bool status)
    {
        upgradePanel.SetActive(status);
    }

    #region Upgrade Count Buttons

    public void UpgradeX1()
    {
        ActivateButton(0);
        TimesToUpgrade = 1;
        UpdateUpgradeValues();
    }

    public void UpgradeX10()
    {
        ActivateButton(1);
        TimesToUpgrade = CanUpgradeManyTimes(10, currentUpgrade) ? 10 : 0;
        UpdateUpgradeValues();
    }

    public void UpgradeX50()
    {
        ActivateButton(2);
        TimesToUpgrade = CanUpgradeManyTimes(50, currentUpgrade) ? 50 : 0;
        UpdateUpgradeValues();
    }

    public void UpgradeMax()
    {
        ActivateButton(3);
        TimesToUpgrade = CalculateUpgradeCount(currentUpgrade);
        UpdateUpgradeValues();
    }

    #endregion

    #region Help Method

    private float GetUpgradeCost(int amount, BaseUpgrade upgrade)
    {
        float cost = 0;
        float upgradecost = upgrade.UpgradeCost;

        for (int i = 0; i < amount; i++)
        {
            cost += upgradecost;
            upgradecost *= upgrade.UpgradeCostMultiplier;
        }
        return cost;
    }

    public bool CanUpgradeManyTimes(int upgradeAmount,BaseUpgrade upgrade)
    {
        int count = CalculateUpgradeCount(upgrade);
        if (count > upgradeAmount)
        {
            return true;
        }
        return false;
    }

    public void ActivateButton(int buttonIndex)
    {
        for (int i = 0; i < upgradeCountButtons.Length; i++)
        {
            upgradeCountButtons[i].GetComponent<Image>().color = buttonDisabledColor;
            upgradeCountButtons[i].GetComponent<Button>().interactable = true;
        }
        upgradeCountButtons[buttonIndex].GetComponent<Button>().interactable = false;
        upgradeCountButtons[buttonIndex].GetComponent<Image>().color = buttonEnabledColor;
        upgradeCountButtons[buttonIndex].transform.DOPunchPosition(transform.localPosition + new Vector3(0, -15f, 0), 0.5f, 10, 0.5f).Play();

    }

    public void UpdateUpgradeValues()
    {
        // Her bir özellik için güncellenmiþ deðerleri hesapla
        float updatedCapasity = CalculateUpdatedCapasity();
        float updatedMoveSpeed = CalculateUpdatedMoveSpeed();
        float updatedWaitTime = CalculateUpdatedWaitTime();

        // Güncellenmiþ deðerleri UI'ya yansýt
        if (currentworker is Chef)
        {
            //chefin deðerleri
        }
        else
        {
            statUpgraded1.text = Currency.DisplayCurrency(updatedCapasity);
            if (currentUpgrade.CurrentLevel % 5 == 0)
            {
                statUpgraded2.text = updatedMoveSpeed.ToString();
                statUpgraded3.text = updatedWaitTime.ToString();
            }
            else 
            {
                statUpgraded2.text = currentworker.moveSpeed.ToString();
                statUpgraded3.text = currentworker.waitTime.ToString();
            }

        }

        upgradeCostText.text = Currency.DisplayCurrency(GetUpgradeCost(TimesToUpgrade, currentUpgrade));
    }

    #endregion

    #region Calculate Methods
    public int CalculateUpgradeCount(BaseUpgrade upgrade)
    {
        int count = 0;
        int currentMoney = MoneyManager.instance.CurrentMoney;
        int upgradeCost = (int)upgrade.UpgradeCost;
        for (int i = currentMoney; i >= 0; i -= upgradeCost)
        {
            count++;
            upgradeCost *= (int)upgrade.UpgradeCostMultiplier;
        }
        return count;
    }
    private float CalculateUpdatedCapasity()
    {
        float currentCapasity = currentworker.carryCapacity;
        float capasityMTP = currentUpgrade.CapasityMultiplier;
        float updatedCapasity = currentCapasity;

        for (int i = 0; i < TimesToUpgrade; i++)
        {
            updatedCapasity *= capasityMTP;
        }

        return updatedCapasity;
    }
    private int CalculateUpdatedMoveSpeed()
    {
        int currentSpeed = (int)currentworker.moveSpeed;
        float moveSpeedMTP = currentUpgrade.MoveSpeedMultiplier;
        int updatedMoveSpeed = currentSpeed;

        for (int i = 0; i < TimesToUpgrade; i++)
        {
            if (currentUpgrade.CurrentLevel % 5 == 0)
                updatedMoveSpeed += (int)Math.Abs((updatedMoveSpeed * moveSpeedMTP) - updatedMoveSpeed);
        }

        return updatedMoveSpeed;
    }
    private float CalculateUpdatedWaitTime()
    {
        int currentWaitTime = (int)currentworker.waitTime;
        float waitTimeReducer = currentUpgrade.WaitTimeReducer;
        float updatedWaitTime = currentWaitTime;

        for (int i = 0; i < TimesToUpgrade; i++)
        {
            if (currentUpgrade.CurrentLevel % 5 == 0)
                updatedWaitTime -= waitTimeReducer;
        }

        return updatedWaitTime;
    }
    #endregion

    #region Update Elevator Panel
    public void UpdateElevatorPanel(BaseUpgrade elevatorUpgrade)
    {
        Elevator elevator = elevatorUpgrade.Elevator;
        panelTitle.text = $"Elevator Level: {elevatorUpgrade.CurrentLevel}";

        stats[2].SetActive(true);

        // Update Icons
        panelIcon.sprite = elevatorIcon;
        stat1icon.sprite = capasityIcon;
        stat2icon.sprite = movementIcon;
        stat3icon.sprite = waitSpeedIcon;

        // Update stat Titles
        stat1title.text = "Carry Capasity";
        stat2title.text = "Movement Speed";
        stat3title.text = "Loading Speed";

        // Update current Stats
        Debug.Log(elevator.moveSpeed);
        currentStat1.text = Currency.DisplayCurrency(elevator.carryCapacity);
        currentStat2.text = $"{elevator.moveSpeed}";
        currentStat3.text = $"{elevator.waitTime}";

        // Update Upgraded Stats
        UpdateUpgradeValues();

    }
    #endregion

    #region Update Chef Panel


    public void UpdateChefPanel(BaseUpgrade chefUpgrade)
    {
        panelTitle.text = $"Chef Level: {chefUpgrade.CurrentLevel}";
        stats[2].SetActive(false);

        // Update Icons
        panelIcon.sprite = elevatorIcon;

        // Update Stat Texts 
        stat1title.text = "Cook Amount";
        stat2title.text = "Cook Speed";

    }
    #endregion

    #region Update Courier Panel
    public void UpdateCourierPanel(BaseUpgrade upgrade)
    {
        Courier courier = upgrade.Courier;
        panelTitle.text = $"Courier Level: {upgrade.CurrentLevel}";

        stats[2].SetActive(true);

        upgradeCostText.text = $"Cost\n{upgrade.UpgradeCost}";

        // Update Icons
        panelIcon.sprite = elevatorIcon;
        stat1icon.sprite = capasityIcon;
        stat2icon.sprite = movementIcon;
        stat3icon.sprite = waitSpeedIcon;

        // Update stat Titles
        stat1title.text = "Carry Capasity";
        stat2title.text = "Movement Speed";
        stat3title.text = "Loading Speed";

        // Update current Stats
        Debug.Log(courier.moveSpeed);
        currentStat1.text = Currency.DisplayCurrency(courier.carryCapacity);
        currentStat2.text = $"{courier.moveSpeed}";
        currentStat3.text = $"{courier.waitTime}";

        // Update carry capasity Upgraded
        int currentCapasity = (int)courier.carryCapacity;
        int capasityMTP = (int)upgrade.CapasityMultiplier;
        int capasityAdded = Math.Abs(currentCapasity - (currentCapasity * capasityMTP));
        Debug.Log(capasityAdded);
        statUpgraded1.text = Currency.DisplayCurrency(capasityAdded);

        // Update move speed Upgraded
        float currentMSpeed = courier.moveSpeed;
        float moveSpeedMTP = upgrade.MoveSpeedMultiplier;
        float moveSpeedAdded = Math.Abs(currentMSpeed - (currentMSpeed * moveSpeedMTP));
        if ((upgrade.CurrentLevel + 1) % 10 == 0)
        {
            statUpgraded2.text = $"+{moveSpeedAdded}/s";
        }
        else
        {
            statUpgraded2.text = "0";
        }

        // Update Load Time
        float loadTimeAdded = upgrade.WaitTimeReducer;
        if ((upgrade.CurrentLevel + 1) % 10 == 0)
        {
            statUpgraded3.text = $"{loadTimeAdded}";
        }
        else
        {
            statUpgraded3.text = "0";
        }
    }
    #endregion

    #region Update Waiter Panel
    public void UpdateWaiterPanel(BaseUpgrade upgrade)
    {
        Waiter waiter = upgrade.Waiter;
        panelTitle.text = $"Waiter Level: {upgrade.CurrentLevel}";

        stats[2].SetActive(true);

        upgradeCostText.text = $"Cost\n{Currency.DisplayCurrency(upgrade.UpgradeCost)}";

        // Update Icons
        panelIcon.sprite = elevatorIcon;
        stat1icon.sprite = capasityIcon;
        stat2icon.sprite = movementIcon;
        stat3icon.sprite = waitSpeedIcon;

        // Update stat Titles
        stat1title.text = "Carry Capasity";
        stat2title.text = "Movement Speed";
        stat3title.text = "Loading Speed";

        // Update current Stats
        Debug.Log(waiter.moveSpeed);
        currentStat1.text = Currency.DisplayCurrency(waiter.carryCapacity);
        currentStat2.text = $"{waiter.moveSpeed}";
        currentStat3.text = $"{waiter.waitTime}";

        // Update carry capasity Upgraded
        int currentCapasity = (int)waiter.carryCapacity;
        int capasityMTP = (int)upgrade.CapasityMultiplier;
        int capasityAdded = Math.Abs(currentCapasity - (currentCapasity * capasityMTP));
        Debug.Log(capasityAdded);
        statUpgraded1.text = $"{capasityAdded}";

        // Update move speed Upgraded
        float currentMSpeed = waiter.moveSpeed;
        float moveSpeedMTP = upgrade.MoveSpeedMultiplier;
        float moveSpeedAdded = Math.Abs(currentMSpeed - (currentMSpeed * moveSpeedMTP));
        if ((upgrade.CurrentLevel + 1) % 10 == 0)
        {
            statUpgraded2.text = $"+{moveSpeedAdded}/s";
        }
        else
        {
            statUpgraded2.text = "0";
        }

        // Update Load Time
        float loadTimeAdded = upgrade.WaitTimeReducer;
        if ((upgrade.CurrentLevel + 1) % 10 == 0)
        {
            statUpgraded3.text = $"{loadTimeAdded}";
        }
        else
        {
            statUpgraded3.text = "0";
        }
    }
    #endregion

    #region Events
    private void ChefUpgradeRequest(ChefUpgrade upgrade)
    {
        List<Floor> floorList = FloorManager.instance.Floors;
        for (int i = 0; i < floorList.Count; i++)
        {
        }
        currentUpgrade = upgrade;
        //currentworker = upgrade.Chef;
    }

    private void ElevatorUpgradeRequest(ElevatorUpgrade elevatorUpgrade)
    {
        currentworker = elevatorUpgrade.Elevator;
        currentUpgrade = elevatorUpgrade;
        OpenUpgradePanel(true);
        UpdateElevatorPanel(elevatorUpgrade);
    }

    private void CourierUpgradeRequest(CourierUpgrade upgrade)
    {
        currentUpgrade = upgrade;
        currentworker = upgrade.Courier;
        OpenUpgradePanel(true);
        UpdateCourierPanel(upgrade);
    }

    private void WaiterUpgradeRequest(WaiterUpgrade upgrade)
    {
        currentUpgrade = upgrade;
        currentworker = upgrade.Waiter;
        OpenUpgradePanel(true);
        UpdateWaiterPanel(upgrade);
    }

    private void OnEnable()
    {
        ChefUI.OnUpgradeRequest += ChefUpgradeRequest;
        ElevatorUI.OnUpgradeRequest += ElevatorUpgradeRequest;
        CourierUI.OnUpgradeRequest += CourierUpgradeRequest;
        WaiterUI.OnUpgradeRequest += WaiterUpgradeRequest;
    }

    private void OnDisable()
    {
        ChefUI.OnUpgradeRequest -= ChefUpgradeRequest;
        ElevatorUI.OnUpgradeRequest -= ElevatorUpgradeRequest;
        CourierUI.OnUpgradeRequest -= CourierUpgradeRequest;
        WaiterUI.OnUpgradeRequest -= WaiterUpgradeRequest;
    }
    #endregion
}
