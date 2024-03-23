using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void TakeFood();
    void DeliverFood();

    IEnumerator MoveToDestination(Transform destination, Action onReachDestination);
}