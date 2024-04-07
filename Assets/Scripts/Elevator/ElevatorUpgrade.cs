using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorUpgrade : BaseUpgrade
{
    protected override void RunUpgrade()
    {
        if (elevator != null)
        {
            elevator.carryCapacity *= capasityMultiplier;

            if (CurrentLevel % 5 == 0)
            {
                // 10 levelde bir de�i�ecek �eyler
                elevator.moveSpeed *= moveSpeedMultiplier;
                elevator.waitTime -= waitTimeReducer;
            }
        }
    }
}
