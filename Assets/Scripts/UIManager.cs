using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI pizzacounttext,moneytext;


    // Start is called before the first frame update
    void Awake()
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

    public void UpdateText(TextMeshProUGUI text, string textvalue)
    {
        text.text = textvalue;
    }

    // Update is called once per frame
    void Update()
    {
        moneytext.text = Currency.DisplayCurrency(MoneyManager.instance.CurrentMoney);
    }

}
