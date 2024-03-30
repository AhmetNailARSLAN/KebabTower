using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaiterUI : MonoBehaviour
{
    public TextMeshProUGUI currentLevelTMP;

    private WaiterUpgrade waiterUpgrade;


    void Start()
    {
        waiterUpgrade = GetComponent<WaiterUpgrade>();
    }

    public void UpgradeElevator(BaseUpgrade upgrade, int currentlevel)
    {
        if (upgrade == waiterUpgrade)
        {
            currentLevelTMP.text = $"Level: {currentlevel}";
        }

    }

    private void OnEnable()
    {
        WaiterUpgrade.OnUpgrade += UpgradeElevator;
    }

    private void OnDisable()
    {
        WaiterUpgrade.OnUpgrade -= UpgradeElevator;
    }
}
