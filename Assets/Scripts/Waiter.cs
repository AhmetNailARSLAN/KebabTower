using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter : Worker
{
    public Table food;
    public Storage storage;

    public Transform tablePos;
    public Transform cashierPos;

    private bool isCarryingFood = false;

    private void Start()
    {
        MoveTable();
    }

    public override void TakeFood()
    {
        var foodToTake = Mathf.Min(carryCapacity, food.foodAmount);
        foodAmount += foodToTake;
        food.foodAmount -= foodToTake;
        isCarryingFood = true;
        MoveStorage();
    }

    public override void DeliverFood()
    {
        storage.foodAmount += foodAmount;
        foodAmount = 0;
        MoveTable();
    }

    void MoveTable()
    {
        // Yemeði almaya git.
        StartCoroutine(MoveToDestination(tablePos, () => TakeFood()));
    }

    void MoveStorage()
    {
        //yemeði teslim etmeye götür
        StartCoroutine(MoveToDestination(cashierPos, () => DeliverFood()));
    }

    public override IEnumerator MoveToDestination(Transform destination, Action onReachDestination)
    {
        // Hedefe ulaþana kadar hareket et.
        while (Vector3.Distance(transform.position, destination.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination.position, moveSpeed * Time.deltaTime);
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