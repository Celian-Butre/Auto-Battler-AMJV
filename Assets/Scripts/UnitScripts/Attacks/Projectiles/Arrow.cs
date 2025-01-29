using UnityEngine;

public class Arrow : ProjectileHandler
{
    [SerializeField] private float baseDamage;
    [SerializeField] private float baseKnockback;
    
    void OnCollisionEnter(Collision collision)
    {
        // Can be optimized with tags, but it add dependance beetween teams
        if (collision.gameObject.TryGetComponent<AbstractUnit>(out AbstractUnit unit))
        {
            if (unit is MinecraftUnit && unit.IsTeamA != FromTeamA) // No friendly fire
            {
                MinecraftUnit minecraftUnit = unit as MinecraftUnit;
                Vector3 knockback = RigidBody.linearVelocity * baseKnockback;
                minecraftUnit.StartCoroutine(minecraftUnit.MovementHandler.TakeImpulse(knockback));
            }
            unit.TakeDamage(baseDamage);
            _minecraftUnitOrigin.Capacity.AddMana(baseDamage);
        }
        
        Destroy(this.gameObject);
    }
}
