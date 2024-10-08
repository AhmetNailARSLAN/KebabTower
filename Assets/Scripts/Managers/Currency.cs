using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public static string DisplayCurrency(int money)
    {
        string[] suffixes = { "", "K", "M", "B", "T", "AA"}; // Suffixleri tanımla

        // Money değeri ile hangi suffixin kullanılacağını belirle
        int suffixIndex = 0;
        while (money >= 1000 && suffixIndex < suffixes.Length - 1)
        {
            money /= 1000;
            suffixIndex++;
        }
        Debug.Log(money);
        return $"{money:F2}{suffixes[suffixIndex]}";
    }
    public static string DisplayCurrency(float money)
    {
        string[] suffixes = { "", "K", "M", "B", "T", "AA" }; // Suffixleri tanımla

        // Money değeri ile hangi suffixin kullanılacağını belirle
        int suffixIndex = 0;
        while (money >= 1000 && suffixIndex < suffixes.Length - 1)
        {
            money /= 1000;
            suffixIndex++;
        }
        Debug.Log(money);
        return $"{money:F2}{suffixes[suffixIndex]}";
    }
}
