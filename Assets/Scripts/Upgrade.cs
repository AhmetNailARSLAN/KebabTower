using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Upgrade
{
    public int level = 1; // Yükseltme seviyesi.
    public float cost = 100; // Yükseltme maliyeti.
    public float costMultiplier = 1.15f; // Maliyet artýþ oraný.

    public void ApplyUpgrade()
    {
        // Yükseltme seviyesini artýr ve maliyeti oranda artýr.
        level++;
        cost *= costMultiplier;
    }
}
