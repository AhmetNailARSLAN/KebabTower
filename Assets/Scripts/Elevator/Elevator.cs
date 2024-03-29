using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Elevator : Worker
{
    public Cashier cashier;

    public TextMeshProUGUI foodText;

    public int currentFloor;
    float freeSpace;

    public List<Storage> storageList;

    public bool isCollectingFood;
    public bool isWaiting;
    public bool isFull;

    public GameObject firstFloor;

    private void Start()
    {
        storageList = new List<Storage>();
        currentFloor = 0;
        freeSpace = carryCapacity;

        Storage storage = firstFloor.GetComponentInChildren<Storage>();
        storageList.Add(storage);

        MoveStorage();
    }

    private void Update()
    {
        foodText.text = foodAmount.ToString();
    }

    public override void TakeFood()
    {
        Storage storage = storageList[currentFloor];
        float foodToTake = Mathf.Min(freeSpace, storage.foodAmount);
        freeSpace -= foodToTake;
        foodAmount += foodToTake;
        storage.foodAmount -= foodToTake;

        if (freeSpace == 0 || currentFloor >= storageList.Count -1)
        {
            currentFloor = 0;
            MoveCashier();
        }
        else
        {
            currentFloor++;
            MoveStorage();
        }
    }

    public override void DeliverFood()
    {
        freeSpace = carryCapacity;
        cashier.ReceiveFood(foodAmount);
        foodAmount = 0;
        MoveStorage();
    }

    void MoveStorage()
    {
        Debug.Log(currentFloor);
        StartCoroutine(MoveToDestination(TargetPos(storageList[currentFloor].transform.position), () => TakeFood()));
    }

    private void MoveCashier()
    {
        StartCoroutine(MoveToDestination(TargetPos(cashier.transform.position), () => DeliverFood()));
    }

    public void NewStorage(GameObject floor)
    {
        Storage storage = floor.GetComponentInChildren<Storage>();
        storageList.Add(storage);
    }

    Vector3 TargetPos(Vector3 _position)
    {
        _position = new Vector3(transform.position.x, _position.y, transform.position.z);
        return _position;
    }
}