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

    private void Start()
    {
        MoveTable();
    }

    public override void TakeFood()
    {
        var foodToTake = Mathf.Min(carryCapacity, food.foodAmount);
        foodAmount += foodToTake;
        food.foodAmount -= foodToTake;
        FlipWaiter();
        MoveStorage();
    }

    public override void DeliverFood()
    {
        storage.foodAmount += foodAmount;
        foodAmount = 0;
        FlipWaiter();
        MoveTable();
    }

    void MoveTable()
    {
        // Yemeði almaya git.
        StartCoroutine(MoveToDestination(tablePos.position, () => TakeFood()));
    }

    void MoveStorage()
    {
        //yemeði teslim etmeye götür
        StartCoroutine(MoveToDestination(cashierPos.position, () => DeliverFood()));
    }

    void FlipWaiter()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}