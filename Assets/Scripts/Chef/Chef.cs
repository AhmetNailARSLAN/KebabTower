using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chef : MonoBehaviour
{
    public float cookingSpeed;
    public float makeAmount;
    public float foodAmount;

    private void Start()
    {
        StartCoroutine(CookFood());
    }

    IEnumerator CookFood()
    {
        while (true)
        {
            yield return new WaitForSeconds(cookingSpeed);
            ServeFood();
        }
    }

    public void ServeFood()
    {
        foodAmount += makeAmount;
    }
}
