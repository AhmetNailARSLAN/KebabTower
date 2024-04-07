using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChefUI : MonoBehaviour
{
    public static Action<ChefUpgrade> OnUpgradeRequest;

    public TextMeshProUGUI foodCountTMP;
    public TextMeshProUGUI currentLevelTMP;

    private Chef chef;
    private ChefUpgrade chefUpgrade;


    // Start is called before the first frame update
    void Start()
    {
        chef = GetComponent<Chef>();
        chefUpgrade = GetComponent<ChefUpgrade>();
    }

    // Update is called once per frame
    void Update()
    {
        foodCountTMP.text = chef.foodAmount.ToString();
    }

    public void UpgradeRequest()
    {
        OnUpgradeRequest?.Invoke(chefUpgrade);
    }

    public void UpgradeChef(BaseUpgrade upgrade, int currentlevel)
    {
        if (upgrade == chefUpgrade)
        {
            currentLevelTMP.text = $"Level: {currentlevel}";
        }

    }

    private void OnEnable()
    {
        ChefUpgrade.OnUpgrade += UpgradeChef;
    }

    private void OnDisable()
    {
        ChefUpgrade.OnUpgrade -= UpgradeChef;
    }
}
