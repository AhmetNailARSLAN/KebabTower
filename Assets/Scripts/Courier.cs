using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Courier : Worker
{
    public Cashier cashier;

    private Transform cashierTransform;
    private Transform startPosition;
    private bool isCarryingFood;
    Transform targetPos;

    private void Start()
    {
        cashierTransform = cashier.transform;
        startPosition = cashierTransform;
        targetPos.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f));

        // Kurye baþlangýç pozisyonuna ýþýnlanýr.
        TeleportStartPos();
        MoveToDeliver();

    }

    private void Update()
    {
    }

    void TeleportStartPos()
    {
        transform.position = startPosition.position;
    }

    void MoveToDeliver()
    {
        StartCoroutine(MoveToDestination(targetPos, () => DeliverFood()));
    }

    public override void TakeFood()
    {
        var foodToTake = Mathf.Min(carryCapacity, cashier.foodAmount);
        foodAmount += foodToTake;
        cashier.foodAmount -= foodToTake;
        isCarryingFood = true;
        MoveToDeliver();
    }

    public override void DeliverFood()
    {
        GameManager.instance.SellFood(foodAmount);
        TeleportStartPos();
    }

    public override IEnumerator MoveToDestination(Transform destination, Action onReachDestination)
    {
        // Hedefe ulaþana kadar hareket et.
        while (Vector3.Distance(transform.position, destination.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination.position, moveSpeed * Time.deltaTime);
            yield return new WaitForSeconds(0.4f);
            yield return null;
        }

        // Hedefe ulaþýnca belirtilen süre kadar bekle.
        yield return new WaitForSeconds(waitTime);

        // Hedefe ulaþtý, callback'i çaðýr.
        Debug.Log("Reached destination, invoking callback.");
        onReachDestination?.Invoke();
    }
}