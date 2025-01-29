using UnityEngine;

public class NeutralBehaviour : AbstractBehaviour
{
    [SerializeField] private float distanceGoal = 0.0f;
    protected override void MoveAction()
    {
        Unit.MovementHandler.UpdateNearest();
        Vector3 targetPos = Unit.MovementHandler.TargetUnit.transform.position;
        Vector3 goalPos = targetPos + (transform.position - targetPos).normalized * distanceGoal;
        Unit.MovementHandler.MoveTowards(goalPos);
    }

    protected override void AttackAction()
    {
        //Unit.AttackHandler.Attack();
    }
}
