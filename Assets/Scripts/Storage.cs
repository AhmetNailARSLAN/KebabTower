using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Storage : MonoBehaviour
{
    public TextMeshProUGUI foodText;

    public float foodAmount;
    private void Update()
    {
        foodText.text = foodAmount.ToString();
    }
}
