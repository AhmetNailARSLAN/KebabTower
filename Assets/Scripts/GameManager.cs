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
    public GameObject levelPrefab; // Kat prefab�n� s�r�kleyip b�rakabiliriz
    public float powerMultiplier = 1.2f; // Katlar�n g�� �arpan�
    public float levelHeight = 10.0f; // Katlar�n y�ksekli�i
    private Vector3 nextLevelPosition = Vector3.zero; // Yeni kat�n pozisyonu



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

        // Kat prefab�n� belirli bir pozisyonda �rnekleyin
        GameObject newLevelObject = Instantiate(levelPrefab, nextLevelPosition, Quaternion.identity);
        floors.Add(newLevelObject);

        // Yeni kat�n Level komponentini al�n
        Level newLevel = newLevelObject.GetComponent<Level>();

        // �arpan� kullanarak kat�n �zelliklerini g��lendirin
        newLevel.chef.cookingSpeed *= powerMultiplier;
        newLevel.chef.makeAmount *= powerMultiplier;
        newLevel.waiter.waitTime -= 0.5f;
        newLevel.waiter.carryCapacity *= powerMultiplier;
        newLevel.waiter.moveSpeed *= powerMultiplier;

        // �arpan� ve yeni kat�n pozisyonunu art�r�n
        powerMultiplier *= 1.2f;
    }

}
