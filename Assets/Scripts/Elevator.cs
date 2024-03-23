using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Cashier cashier;

    public TextMeshProUGUI foodText;

    public float carryCapacity;
    public float moveSpeed;
    public float waitTime;
    public float foodAmount;

    public int currentFloor;

    public List<Storage> storageList;

    public bool isCollectingFood;
    public bool isWaiting;
    public bool isFull;


    private void Start()
    {
        storageList = new List<Storage>();
        currentFloor = 0;

        // Her katmandaki kasiyerleri bulun.
        foreach (GameObject cashierObj in GameObject.FindGameObjectsWithTag("Storage"))
        {
            storageList.Add(cashierObj.GetComponent<Storage>());
        }
        storageList.Reverse();
    }

    private void Update()
    {
        if (!isCollectingFood && !isWaiting && !isFull)
        {
            // Yiyecek ara.
            if (FindFood())
            {
                isCollectingFood = true;
            }
        }
        else if (isCollectingFood && !isWaiting)
        {
            MoveStorage();
        }
        else if (!isCollectingFood && !isWaiting && isFull)
        {
            MoveCashier();
        }

        foodText.text = foodAmount.ToString();
    }

    private bool FindFood()
    {
        // Kasiyerlerin yiyecek ihtiyacýný kontrol edin.
        if (storageList[currentFloor].HasFood())
        {
            return true;
        }
        return false;
    }

    private void CollectFood()
    {
        // Kasiyerden yiyecek al.
        float collectedFood = storageList[currentFloor].TakeFood(carryCapacity);
        foodAmount += collectedFood;

        if (carryCapacity == foodAmount)
        {
            // Asansör doluysa, satýþ departmanýna git.
            isCollectingFood = false;
            isFull = true;
        }
    }
    void MoveStorage()
    {
        // Yemeði taþý.
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, storageList[currentFloor].transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);

        if (Mathf.Abs(transform.position.y - storageList[currentFloor].transform.position.y) < 0.1f)
        {
            // Yemeði al.
            CollectFood();
            isWaiting = true;
            StartCoroutine(WaitAndGoToNextFloor());
        }
    }
    private void MoveCashier()
    {
        // Yemeði taþý.
        transform.position = Vector3.MoveTowards(transform.position, 
            new Vector3(transform.position.x, cashier.transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);

        if (Mathf.Abs(transform.position.y - cashier.transform.position.y) < 0.1f)
        {
            // Yiyecekleri teslim et.
            cashier.ReceiveFood(foodAmount);

            // Asansörü boþalt.
            foodAmount = 0;

            // Tekrar yiyecek aramaya baþla.
            isCollectingFood = true;
            isWaiting = false;
        }
    }

    IEnumerator WaitAndGoToNextFloor()
    {
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;

        currentFloor++;
        if (currentFloor >= storageList.Count)
        {
            currentFloor = 0;
            isCollectingFood = false;
            isFull= true;
        }

    }
}