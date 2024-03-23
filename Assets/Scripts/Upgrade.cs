using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Upgrade
{
    public int level = 1; // Y�kseltme seviyesi.
    public float cost = 100; // Y�kseltme maliyeti.
    public float costMultiplier = 1.15f; // Maliyet art�� oran�.

    public void ApplyUpgrade()
    {
        // Y�kseltme seviyesini art�r ve maliyeti oranda art�r.
        level++;
        cost *= costMultiplier;
    }
}
