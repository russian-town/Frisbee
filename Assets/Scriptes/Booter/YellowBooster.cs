using UnityEngine;

public class YellowBooster : Booster
{
    protected override int GetBoostCount()
    {
        return CurrentFrisbeeCount * BoostCount - CurrentFrisbeeCount;
    }
}
