using TMPro;
using UnityEngine;

public class Cashier : MonoBehaviour
{
    public float foodAmount;
    public TextMeshProUGUI foodText;

    private void Start()
    {
        foodAmount = 0;
    }
    public bool HasFood()
    {
        return foodAmount > 0;
    }
    private void Update()
    {
        foodText.text = foodAmount.ToString();
    }

    public void ReceiveFood(float amount)
    {
        foodAmount += amount;
    }
}