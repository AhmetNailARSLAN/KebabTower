using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Worker : MonoBehaviour, IInteractable
{
    public float carryCapacity;
    public float foodAmount;
    public float moveSpeed;
    public float waitTime = 0.4f;

    public int upgradeLevel = 1; // Y�kseltme seviyesi.
    public int upgradeCost = 100; // Y�kseltme maliyeti.

    public abstract void TakeFood();
    public abstract void DeliverFood();

    public virtual void Upgrade()
    {
        // Y�kseltme i�lemi ger�ekle�ti�inde upgradeLevel ve upgradeCost de�erlerini art�r.

        upgradeLevel++;
        upgradeCost *= 2;
    }

    public abstract IEnumerator MoveToDestination(Transform destination, Action onReachDestination);
}
