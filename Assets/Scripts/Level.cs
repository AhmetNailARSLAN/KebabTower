using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Chef chef;
    public Waiter waiter;
    public Storage storage;
    public Table table;

    public bool isLocked;

    GameObject openFloor;
    GameObject lockedFloor;

    // Start is called before the first frame update
    void Start()
    {
        
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
