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

    public int upgradeLevel = 1; // Yükseltme seviyesi.
    public int upgradeCost = 100; // Yükseltme maliyeti.

    public abstract void TakeFood();
    public abstract void DeliverFood();

    public virtual void Upgrade()
    {
        // Yükseltme iþlemi gerçekleþtiðinde upgradeLevel ve upgradeCost deðerlerini artýr.

        upgradeLevel++;
        upgradeCost *= 2;
    }
    public virtual IEnumerator MoveToDestination(Vector3 destination, Action onReachDestination)
    {
        // Hedefe ulaþana kadar hareket et.
        while (Vector3.Distance(transform.position, destination) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Hedefe ulaþýnca belirtilen süre kadar bekle.
        yield return new WaitForSeconds(waitTime);

        // Hedefe ulaþtý, callback'i çaðýr.
        Debug.Log("Reached destination, invoking callback.");
        onReachDestination?.Invoke();
    }
}
