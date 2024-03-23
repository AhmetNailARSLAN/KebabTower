using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef : MonoBehaviour
{
    public Table targetFood;
    public float cookingSpeed;
    public float makeAmount;

    private void Start()
    {
        StartCoroutine(CookFood());
    }

    IEnumerator CookFood()
    {
        while (true)
        {
            Debug.Log("Çalýþtý");
            yield return new WaitForSeconds(cookingSpeed);
            targetFood.ServeFood(makeAmount);
        }
    }
}
