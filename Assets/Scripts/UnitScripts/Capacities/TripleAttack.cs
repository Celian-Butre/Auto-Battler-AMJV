using UnityEngine;
using System.Collections;

public class TripleAttack : BaseCapacity
{
    protected override bool CapacityCall()
    {
        MinecraftUnit minecraftUnit = _unit as MinecraftUnit;
        StartCoroutine(TripleAttackRoutine(minecraftUnit));
        return true;
    }

    private IEnumerator TripleAttackRoutine(MinecraftUnit minecraftUnit)
    {
        minecraftUnit.AttackHandler.Attack();
        yield return new WaitForSeconds(0.1f);
        minecraftUnit.AttackHandler.Attack();
        yield return new WaitForSeconds(0.1f);
        minecraftUnit.AttackHandler.Attack();
    }
    
    
}
