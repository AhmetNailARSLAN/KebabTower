using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Table : MonoBehaviour
{
    public float foodAmount;
    public bool readyToServe;

    public TextMeshProUGUI foodtext;
    

    private void Update()
    {
        if (foodAmount >= 1)
        {
            readyToServe = true;
        }
        foodtext.text = foodAmount.ToString();
    }

    public void ServeFood(float foodamount)
    {
        foodAmount += foodamount;
        readyToServe = false;
    }
}
