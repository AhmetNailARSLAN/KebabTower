using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public static FloorManager instance;
    [SerializeField] private Floor levelPrefab;

    [Header("Floors")]
    [SerializeField] private List<Floor> floors;

    public List<Floor> Floors => floors;


    public Elevator elevator;

    public float powerMultiplier = 1.2f;
    public float chefmakeAmountMultiplier = 10f;
    public float movementMultiplier = 1.2f;
    public float levelCostMultiplier = 3.2f;

    public float levelHeight = 10.0f;
    private int currentFloorIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance.gameObject);
            instance = this;
        }
    }

    void Start()
    {
        floors[0].FloorID = 0;
    }

    private void CreateNewLevel(Vector3 lastPosition)
    {
        lastPosition = new Vector3(lastPosition.x, lastPosition.y + levelHeight, lastPosition.z);
        Floor newLevel = Instantiate(levelPrefab, lastPosition, Quaternion.identity);

        currentFloorIndex++;
        floors.Add(newLevel);

        UpdateLevelFeatures(newLevel);

    }

    private void UpdateLevelFeatures(Floor newLevel)
    {
        powerMultiplier *= 2f;
        movementMultiplier *= 1.2f;
        chefmakeAmountMultiplier *= 2f;
        levelCostMultiplier *= powerMultiplier;

        // Çalýþan özelliklerii yükselt
        newLevel.chef.makeAmount *= chefmakeAmountMultiplier;
        newLevel.waiter.waitTime -= 0.2f;
        newLevel.waiter.carryCapacity *= powerMultiplier;
        newLevel.waiter.moveSpeed *= movementMultiplier;

        // Kat özelliklerini deðiþtir
        newLevel.UnlockCost = Mathf.Round(newLevel.UnlockCost * levelCostMultiplier);
        newLevel.isLocked = true;
    }

    public void UnlockLevel()
    {
        var lastFloor = floors[floors.Count - 1];
        Floor levelScript = lastFloor.GetComponent<Floor>();

        if (levelScript.UnlockCost < MoneyManager.instance.CurrentMoney)
        {
            CreateNewLevel(lastFloor.transform.position);
            levelScript.isLocked = false;
            MoneyManager.instance.RemoveMoney((int)levelScript.UnlockCost);
            elevator.storageList.Add(levelScript.storage);
        }
    }
}
