using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter : Worker
{
    public Chef chef;
    public Storage storage;

    public Transform tablePos;
    public Transform cashierPos;

    private void Start()
    {
        MoveChef();
    }

    public override void TakeFood()
    {
        var foodToTake = Mathf.Min(carryCapacity, chef.foodAmount);
        foodAmount += foodToTake;
        chef.foodAmount -= foodToTake;
        FlipWaiter();
        MoveStorage();
    }

    public override void DeliverFood()
    {
        storage.foodAmount += foodAmount;
        foodAmount = 0;
        FlipWaiter();
        MoveChef();
    }

    void MoveChef()
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