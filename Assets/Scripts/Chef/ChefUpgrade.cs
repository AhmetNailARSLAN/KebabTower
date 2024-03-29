using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefUpgrade : BaseUpgrade
{
    protected override void RunUpgrade()
    {
        if (chef != null)
        {
            chef.makeAmount *= makeAmountMultiplier;

            if (CurrentLevel % 10 == 0)
            {
                // Þefin sayýsýný artýr
                chef.cookingSpeed -= waitTimeReducer;
            }
        }
    }
}
