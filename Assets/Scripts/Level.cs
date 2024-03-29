using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;

public class Level : MonoBehaviour
{
    public Chef chef;
    public Waiter waiter;
    public Storage storage;

    public bool isLocked;

    public GameObject openFloor;
    public GameObject lockedFloor;
    public GameObject UnlockButton;

    public float UnlockCost = 1f;

    // Start is called before the first frame update
    void Start()
    {
        UnlockButton.GetComponentInChildren<TextMeshProUGUI>().text = UnlockCost.ToString();
    }

    public void Unlock()
    {
        GameManager.instance.UnlockLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocked)
        {
            lockedFloor.SetActive(true);
            openFloor.SetActive(false);
        }
        else
        {
            lockedFloor.SetActive(false);
            openFloor.SetActive(true);
        }
    }
}
