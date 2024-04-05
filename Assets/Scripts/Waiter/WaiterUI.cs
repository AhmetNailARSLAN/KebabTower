using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaiterUI : MonoBehaviour
{
    public static Action<WaiterUpgrade> OnUpgradeRequest;

    public TextMeshProUGUI currentLevelTMP;

    private WaiterUpgrade waiterUpgrade;


    void Start()
    {
        waiterUpgrade = GetComponent<WaiterUpgrade>();
    }

    public void UpdateRequest()
    {
        OnUpgradeRequest?.Invoke(waiterUpgrade);
    }

}
