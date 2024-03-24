using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Elevator elevator;
    public List<GameObject> floors;
    public GameObject levelPrefab;

    public float Money { get; private set; }
    public float foodCost;
    public float powerMultiplier = 1.2f;
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

    public void AddMoney(float amount)
    {
        Money += amount;
    }

    public void SellFood(float amount)
    {
        Money += amount * foodCost;
    }

    public void BuyUpgrade(float cost)
    {
        if (Money >= cost)
        {
            Money -= cost;
        }
    }

    public void CreateNewLevel()
    {
        Vector3 lastFloorPos = floors[floors.Count - 1].transform.position;
        nextLevelPosition = new Vector3(lastFloorPos.x, lastFloorPos.y + levelHeight, lastFloorPos.z);

        GameObject newLevelObject = Instantiate(levelPrefab, nextLevelPosition, Quaternion.identity);
        floors.Add(newLevelObject);
        elevator.NewStorage(newLevelObject);
        UpdateLevelFeatures(newLevelObject);
    }

    private void UpdateLevelFeatures(GameObject newLevelObject)
    {
        powerMultiplier *= 1.2f;
        Level newLevel = newLevelObject.GetComponent<Level>();
        newLevel.chef.cookingSpeed *= powerMultiplier;
        newLevel.chef.makeAmount *= powerMultiplier;
        newLevel.waiter.waitTime -= 0.5f;
        newLevel.waiter.carryCapacity *= powerMultiplier;
        newLevel.waiter.moveSpeed *= powerMultiplier;
    }
}
