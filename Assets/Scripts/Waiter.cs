using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter : Worker
{
    public Table food;
    public Storage cashier;

    public Transform tablePos;
    public Transform cashierPos;

    private bool isCarryingFood = false;

    private void Start()
    {
        MoveTable();
    }

    public override void TakeFood(float amount)
    {
        var foodToTake = Mathf.Min(carryCapacity, food.foodAmount);
        foodAmount += foodToTake;
        food.foodAmount -= foodToTake;
        isCarryingFood = true;
        MoveStorage();
    }

    public override void DeliverFood(float amount)
    {
        var foodToDeliver = Mathf.Min(foodAmount, cashier.foodAmount);
        cashier.foodAmount += foodToDeliver;
        foodAmount -= foodToDeliver;
        isCarryingFood = false;
        MoveTable();
    }

    void MoveTable()
    {
        // Yemeði almaya git.
        StartCoroutine(MoveToDestination(tablePos, () => TakeFood(carryCapacity)));
    }

    void MoveStorage()
    {
        //yemeði teslim etmeye götür
        StartCoroutine(MoveToDestination(cashierPos, () => DeliverFood(foodAmount)));
    }

    IEnumerator MoveToDestination(Transform destination, Action onReachDestination)
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
        FlipWaiter();
        Debug.Log("Reached destination, invoking callback.");
        onReachDestination?.Invoke();
    }

    void FlipWaiter()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}