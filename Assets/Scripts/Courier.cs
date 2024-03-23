using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Courier : MonoBehaviour
{
    public Cashier cashier;

    public float foodAmount;
    public float moveSpeed;
    public float carryCapacity;

    private Transform cashierTransform;
    private Transform startPosition;
    private bool isCarryingFood;

    private void Start()
    {
        cashierTransform = cashier.transform;
        startPosition = cashierTransform;

        // Kurye baþlangýç pozisyonuna ýþýnlanýr.
        TeleportStartPos();
    }

    private void Update()
    {
        if (!isCarryingFood)
        {
            if (cashier.HasFood())
            {
                StartCoroutine(TakeFood());
            }
        }
        else
        {
            MoveToDeliver();
        }
    }

    void TeleportStartPos()
    {
        transform.position = startPosition.position;
    }

    private IEnumerator TakeFood()
    {
        // Kurye yiyecek alma süresini bekler.
        yield return new WaitForSeconds(1f);

        // Kurye kasadan yiyecek alýr.
        float takenFoodAmount = Mathf.Min(carryCapacity, cashier.foodAmount);
        foodAmount += takenFoodAmount;
        cashier.foodAmount -= takenFoodAmount;

        // Kurye yiyecek taþýmaya baþlar.
        isCarryingFood = true;
    }

    void MoveToDeliver()
    {
        // Kurye ekranýn saðýna doðru hareket eder.
        transform.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

        if (transform.position.x > Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x)
        {
            // Kurye elindeki yiyeceði satar
            GameManager.instance.SellFood(foodAmount);

            // Baþlangýç pozisyonuna ýþýnlanýr.
            isCarryingFood = false;
            TeleportStartPos();
        }
    }
}