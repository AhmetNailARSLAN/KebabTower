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

        // Kurye ba�lang�� pozisyonuna ���nlan�r.
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
        // Kurye yiyecek alma s�resini bekler.
        yield return new WaitForSeconds(1f);

        // Kurye kasadan yiyecek al�r.
        float takenFoodAmount = Mathf.Min(carryCapacity, cashier.foodAmount);
        foodAmount += takenFoodAmount;
        cashier.foodAmount -= takenFoodAmount;

        // Kurye yiyecek ta��maya ba�lar.
        isCarryingFood = true;
    }

    void MoveToDeliver()
    {
        // Kurye ekran�n sa��na do�ru hareket eder.
        transform.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

        if (transform.position.x > Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x)
        {
            // Kurye elindeki yiyece�i satar
            GameManager.instance.SellFood(foodAmount);

            // Ba�lang�� pozisyonuna ���nlan�r.
            isCarryingFood = false;
            TeleportStartPos();
        }
    }
}