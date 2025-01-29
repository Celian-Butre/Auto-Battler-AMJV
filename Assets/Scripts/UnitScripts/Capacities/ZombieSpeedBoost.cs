using UnityEngine;
using System.Collections;

public class ZombieSpeedBoost : BaseCapacity
{
    [SerializeField] float timeToBoost;
    [SerializeField] float boost;
    protected override bool CapacityCall()
    {
        MinecraftUnit minecraftUnit = _unit as MinecraftUnit;
        StartCoroutine(AddThenRemoveSpeed(minecraftUnit));
        return true;
    }
    
    private IEnumerator AddThenRemoveSpeed(MinecraftUnit minecraftUnit)
    {
        // Possibility float imprecision issues
        minecraftUnit.MovementHandler.speed *= boost;
        yield return new WaitForSeconds(timeToBoost);
        minecraftUnit.MovementHandler.speed /= boost;
    }
}
