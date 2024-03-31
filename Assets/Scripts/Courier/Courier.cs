using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Courier : Worker
{
    [SerializeField] private GameObject prefab;

    public Cashier cashier;
    [SerializeField] private List<Courier> couriers;
    public List<Courier> Couriers => couriers;

    private Transform cashierTransform;
    private Transform startPosition;
    public Vector3 deliverPosition;
    private bool isCarryingFood;

    private void Start()
    {
        // deopa elemanlarýnýn pozisonu
        cashierTransform = cashier.transform;

        // kuryenin baþlangýç pozisyonnu
        startPosition = cashierTransform;

        TeleportStartPos();

        // teslim edeceði yerin pozisyonu
        Vector3 screenRight = new Vector3(Screen.width, 0, 0);
        Vector3 worldPos = new Vector3(Camera.main.ScreenToWorldPoint(screenRight).x + 2,transform.position.y, transform.position.z);
        deliverPosition = worldPos;

    }
    private void Update()
    {
        if (foodAmount == 0 && cashier.foodAmount > 0)
        {
            TakeFood();
        }
        else if (foodAmount > 0 && !isCarryingFood)
        {
            MoveToDeliver();
            isCarryingFood=true;
        }
    }

    void TeleportStartPos()
    {
        transform.position = startPosition.position;
    }

    void MoveToDeliver()
    {
        StartCoroutine(MoveToDestination(deliverPosition, () => DeliverFood()));
    }
    public override void TakeFood()
    {
        float foodToTake = Mathf.Min(carryCapacity, cashier.foodAmount);
        foodAmount += foodToTake;
        cashier.foodAmount -= foodToTake;
    }

    public override void DeliverFood()
    {
        MoneyManager.instance.SellFood(foodAmount);
        foodAmount=0;
        TeleportStartPos();
        isCarryingFood = false;
    }

}