using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaiterUpgrade : BaseUpgrade
{
    protected override void RunUpgrade()
    {
        if (waiter != null)
        {
            waiter.carryCapacity *= capasityMultiplier;

            if (CurrentLevel % 5 == 0)
            {
                // 10 levelde bir deðiþecek þeyler
                waiter.moveSpeed *= moveSpeedMultiplier;
                waiter.waitTime -= waitTimeReducer;
            }
        }
    }
}
