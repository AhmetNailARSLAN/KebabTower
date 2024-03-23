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
    public virtual IEnumerator MoveToDestination(Vector3 destination, Action onReachDestination)
    {
        // Hedefe ula�ana kadar hareket et.
        while (Vector3.Distance(transform.position, destination) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Hedefe ula��nca belirtilen s�re kadar bekle.
        yield return new WaitForSeconds(waitTime);

        // Hedefe ula�t�, callback'i �a��r.
        Debug.Log("Reached destination, invoking callback.");
        onReachDestination?.Invoke();
    }
}
