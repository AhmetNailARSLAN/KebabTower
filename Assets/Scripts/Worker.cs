using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Worker : MonoBehaviour, IInteractable
{
    public float carryCapacity;
    public float foodAmount;
    public float moveSpeed;
    public float waitTime = 0.4f;

    public int upgradeLevel = 1; // Yükseltme seviyesi.
    public int upgradeCost = 100; // Yükseltme maliyeti.

    public abstract void TakeFood(float amount);
    public abstract void DeliverFood(float amount);

    public virtual void Upgrade()
    {
        // Yükseltme iþlemi gerçekleþtiðinde upgradeLevel ve upgradeCost deðerlerini artýr.

        upgradeLevel++;
        upgradeCost *= 2;
    }
}
