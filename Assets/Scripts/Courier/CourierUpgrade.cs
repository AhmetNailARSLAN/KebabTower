using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourierUpgrade : BaseUpgrade
{
    protected override void RunUpgrade()
    {
        if (courier != null)
        {
            courier.carryCapacity *= capasityMultiplier;
            if (CurrentLevel % 10 == 0)
            {
                courier.moveSpeed *= moveSpeedMultiplier;
                courier.waitTime -= waitTimeReducer;
                //bir kurye daha eklenecek
            }
        }

    }
}
