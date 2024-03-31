using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CourierUI : MonoBehaviour
{
    public static Action<CourierUpgrade> OnUpgradeRequest;

    public TextMeshProUGUI foodCountTMP;
    public TextMeshProUGUI currentLevelTMP;

    private Courier courier;
    private CourierUpgrade courierUpgrade;


    // Start is called before the first frame update
    void Start()
    {
        courier = GetComponent<Courier>();
        courierUpgrade = GetComponent<CourierUpgrade>();
    }

    // Update is called once per frame
    void Update()
    {
        //foodCountTMP.text = courier.foodAmount.ToString();
    }

    public void UpgradeElevator(BaseUpgrade upgrade, int currentlevel)
    {
        if (upgrade == courierUpgrade)
        {
            currentLevelTMP.text = $"Level: {currentlevel}";
        }

    }
    public void RequestUpgrade()
    {
        OnUpgradeRequest.Invoke(courierUpgrade);
    }

}
