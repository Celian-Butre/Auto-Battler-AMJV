using UnityEngine;
using System.Collections;

public class GolemDefense : BaseCapacity
{
    [SerializeField] private float armorGain;
    [SerializeField] private float buffTime;
    [SerializeField] private SphereCollider buffArea;

    protected override bool CapacityCall()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, buffArea.radius, buffArea.includeLayers);
        foreach (Collider target in hitColliders)
        {
            if (!target.CompareTag("Unit")) continue;
            AbstractUnit targetUnit = target.GetComponent<AbstractUnit>();
            if (targetUnit.IsTeamA == _unit.IsTeamA)
            {
                CoroutineManager.Instance.StartCoroutine(AddThenRemoveArmor(targetUnit));
            }
        }
        return hitColliders.Length > 0;
    }

    private IEnumerator AddThenRemoveArmor(AbstractUnit targetUnit)
    {
        targetUnit.AddArmor(armorGain);
        yield return new WaitForSeconds(buffTime);
        targetUnit.RemoveArmor(armorGain);
    }
}


