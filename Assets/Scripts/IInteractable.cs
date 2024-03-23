using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void TakeFood(float amount);
    void DeliverFood(float amount);
}