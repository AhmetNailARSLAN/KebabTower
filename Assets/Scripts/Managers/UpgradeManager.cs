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
    

    private Chef selectedChef;
    private ChefUpgrade selectedChefUpgrade;

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
        upgradeCostText.text = $"{currentUpgrade.UpgradeCost}";
    }

    public void UpgradeX10()
    {
        ActivateButton(1);
        TimesToUpgrade = CanUpgradeManyTimes(10, currentUpgrade) ? 10 : 0;
        upgradeCostText.text = GetUpgradeCost(10, currentUpgrade).ToString();
    }

    public void UpgradeX50()
    {
        ActivateButton(2);
        TimesToUpgrade = CanUpgradeManyTimes(50, currentUpgrade) ? 50 : 0;
        upgradeCostText.text = GetUpgradeCost(50, currentUpgrade).ToString();
    }

    public void UpgradeMax()
    {
        ActivateButton(3);
        TimesToUpgrade = CalculateUpgradeCount(currentUpgrade);
        upgradeCostText.text = GetUpgradeCost(TimesToUpgrade, currentUpgrade).ToString();
    }

    #endregion

    #region Help Method

    private int  GetUpgradeCost(int amount, BaseUpgrade upgrade)
    {
        int cost = 0;
        int upgradecost = (int) upgrade.UpgradeCost;

        for (int i = 0; i < amount; i++)
        {
            cost += upgradecost;
            upgradecost *= (int) upgrade.UpgradeCostMultiplier;
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
        }

        upgradeCountButtons[buttonIndex].GetComponent<Image>().color = buttonEnabledColor;
        upgradeCountButtons[buttonIndex].transform.DOPunchPosition(transform.localPosition + new Vector3(0, -15f, 0), 0.5f, 10, 0.5f).Play();

    }

    public int CalculateUpgradeCount(BaseUpgrade upgrade)
    {
        int count = 0;
        int currentMoney = MoneyManager.instance.CurrentMoney;
        int upgradeCost = (int)upgrade.UpgradeCost;
        for (int i = currentMoney; i >= 0; i -= upgradeCost)
        {
            count++;
            upgradeCost *= (int) upgrade.UpgradeCostMultiplier;
        }
        return count;
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
        currentStat1.text = $"{elevator.carryCapacity}";
        currentStat2.text = $"{elevator.moveSpeed}";
        currentStat3.text = $"{elevator.waitTime}";

        // Update carry capasity Upgraded
        int currentCapasity = (int)elevator.carryCapacity; 
        int capasityMTP = (int)elevatorUpgrade.CapasityMultiplier; 
        int capasityAdded = Math.Abs(currentCapasity - (currentCapasity * capasityMTP));
        Debug.Log(capasityAdded);
        statUpgraded1.text = $"{capasityAdded}";

        // Update move speed Upgraded
        float currentMSpeed = elevator.moveSpeed;
        float moveSpeedMTP = elevatorUpgrade.MoveSpeedMultiplier;
        float moveSpeedAdded = Math.Abs(currentMSpeed - (currentMSpeed * moveSpeedMTP));
        if ((elevatorUpgrade.CurrentLevel +1) % 10 == 0)
        {
            statUpgraded2.text = $"+{moveSpeedAdded}/s";
        }

        // Update Load Time
        float loadTimeAdded = elevatorUpgrade.WaitTimeReducer;
        if ((elevatorUpgrade.CurrentLevel + 1) % 10 == 0)
        {
            statUpgraded3.text = $"{loadTimeAdded}";
        }

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

    #region Events
    private void ChefUpgradeRequest(Chef chef, ChefUpgrade chefUpgrade)
    {
        List<Floor> floorList = FloorManager.instance.Floors;
        for (int i = 0; i < floorList.Count; i++)
        {
        }
        currentUpgrade = chefUpgrade;
    }

    private void ElevatorUpgradeRequest(ElevatorUpgrade elevatorUpgrade)
    {
        OpenUpgradePanel(true);
        UpdateElevatorPanel(elevatorUpgrade);
        currentUpgrade = elevatorUpgrade;
    }


    private void OnEnable()
    {
        ChefUI.OnUpgradeRequest += ChefUpgradeRequest;
        ElevatorUI.OnUpgradeRequest += ElevatorUpgradeRequest;
    }

    private void OnDisable()
    {
        ChefUI.OnUpgradeRequest -= ChefUpgradeRequest;
        ElevatorUI.OnUpgradeRequest -= ElevatorUpgradeRequest;
    }
    #endregion
}
