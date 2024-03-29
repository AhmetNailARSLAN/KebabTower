using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ElevatorUI : MonoBehaviour
{
    public TextMeshProUGUI foodCountTMP;
    public TextMeshProUGUI currentLevelTMP;

    private Elevator elevator;
    private ElevatorUpgrade elevatorUpgrade;


    // Start is called before the first frame update
    void Start()
    {
        elevator = GetComponent<Elevator>();
        elevatorUpgrade = GetComponent<ElevatorUpgrade>();
    }

    // Update is called once per frame
    void Update()
    {
        foodCountTMP.text = elevator.foodAmount.ToString();
    }

    public void UpgradeElevator(BaseUpgrade upgrade, int currentlevel)
    {
        if (upgrade == elevatorUpgrade)
        {
            currentLevelTMP.text = $"Level: {currentlevel}";
        }

    }

    private void OnEnable()
    {
        ElevatorUpgrade.OnUpgrade += UpgradeElevator;
    }

    private void OnDisable()
    {
        ElevatorUpgrade.OnUpgrade -= UpgradeElevator;
    }
}
