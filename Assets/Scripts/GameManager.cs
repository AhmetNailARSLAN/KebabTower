using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float money;
    public float Money { get => money; }

    public float foodCost;

    public List<GameObject> floors;
    public GameObject levelPrefab; // Kat prefabýný sürükleyip býrakabiliriz
    public float powerMultiplier = 1.2f; // Katlarýn güç çarpaný
    public float levelHeight = 10.0f; // Katlarýn yüksekliði
    private Vector3 nextLevelPosition = Vector3.zero; // Yeni katýn pozisyonu



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

    }

    public void AddMoney(float amount)
    {
        money += amount;
    }
    public void SellFood(float amount)
    {
        // Yiyecekleri sat.
        money += amount * foodCost;
    }

    public void BuyUpgrade(float cost)
    {
        if (money >= cost)
        {
            money -= cost;
        }
    }
    public void AddNewLevel()
    {
        Vector3 lastFloorPos = floors[floors.Count - 1].transform.position;
        nextLevelPosition = new Vector3(lastFloorPos.x,lastFloorPos.y + levelHeight, lastFloorPos.z);

        // Kat prefabýný belirli bir pozisyonda örnekleyin
        GameObject newLevelObject = Instantiate(levelPrefab, nextLevelPosition, Quaternion.identity);
        floors.Add(newLevelObject);

        // Yeni katýn Level komponentini alýn
        Level newLevel = newLevelObject.GetComponent<Level>();

        // Çarpaný kullanarak katýn özelliklerini güçlendirin
        newLevel.chef.cookingSpeed *= powerMultiplier;
        newLevel.chef.makeAmount *= powerMultiplier;
        newLevel.waiter.waitTime -= 0.5f;
        newLevel.waiter.carryCapacity *= powerMultiplier;
        newLevel.waiter.moveSpeed *= powerMultiplier;

        // Çarpaný ve yeni katýn pozisyonunu artýrýn
        powerMultiplier *= 1.2f;
    }

}
