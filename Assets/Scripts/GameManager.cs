using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Elevator elevator;
    public List<GameObject> floors;
    public GameObject levelPrefab;

    public float powerMultiplier = 1.2f;
    public float chefmakeAmountMultiplier = 10f;
    public float movementMultiplier = 1.2f;
    public float levelCostMultiplier = 3.2f;
    public float levelHeight = 10.0f;

    private Vector3 nextLevelPosition = Vector3.zero;

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

    private void CreateNewLevel(Vector3 lastPosition)
    {
        lastPosition = new Vector3(lastPosition.x, lastPosition.y + levelHeight, lastPosition.z);

        GameObject newLevelObject = Instantiate(levelPrefab, lastPosition, Quaternion.identity);
        floors.Add(newLevelObject);
        UpdateLevelFeatures(newLevelObject);
    }

    private void UpdateLevelFeatures(GameObject newLevelObject)
    {
        powerMultiplier *= 2f;
        movementMultiplier *= 1.2f;
        chefmakeAmountMultiplier *= 2f;
        levelCostMultiplier *= powerMultiplier;
        Level newLevel = newLevelObject.GetComponent<Level>();

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
        Level levelScript = lastFloor.GetComponent<Level>();

        if (levelScript.UnlockCost < MoneyManager.instance.CurrentMoney)
        {
            CreateNewLevel(lastFloor.transform.position);
            levelScript.isLocked = false;
            MoneyManager.instance.RemoveMoney((int)levelScript.UnlockCost);
            elevator.storageList.Add(levelScript.storage);
        }
    }
}
