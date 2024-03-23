using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Storage : MonoBehaviour
{
    public TextMeshProUGUI foodText;

    public float foodAmount;
    public bool HasFood()
    {
        return foodAmount > 0;
    }
    public float TakeFood(float amount)
    {
        float takenFoodAmount = Mathf.Min(foodAmount, amount);
        foodAmount -= takenFoodAmount;
        return takenFoodAmount;
    }
    private void Update()
    {
        foodText.text = foodAmount.ToString();
    }
}
