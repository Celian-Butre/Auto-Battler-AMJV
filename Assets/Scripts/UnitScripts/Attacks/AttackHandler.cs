using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MinecraftUnit))]
public class AttackHandler : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected float cooldown;
    [SerializeField] protected Collider attackShape;
    [SerializeField] protected float knockbackHorizontalForce;
    [SerializeField] protected float knockbackVerticalForce;
    
    protected MinecraftUnit _minecraftUnit;

    void Awake()
    {
        _minecraftUnit = GetComponent<MinecraftUnit>();
    }

    void Start()
    {
        InvokeRepeating(nameof(Attack), Random.Range(-cooldown*0.2f, cooldown*0.2f), cooldown);
    }

    
    /// <summary>
    /// Launch an Attack, and return true if it's possible to attack
    /// see what Units are in the attackShape, apply damage and knockback to those unit if they're ennemies
    /// </summary>
    public virtual bool Attack()
    {
        Collider[] targets = DetectTargets();
        bool hasHit = false;
        foreach (Collider target in targets)
        {
            if (!target.CompareTag("Unit")) continue;
            // GetComponent is expensive in performance, optimize here if it's slow
            AbstractUnit targetUnit = target.GetComponent<AbstractUnit>();
            
            // No friendly fire
            if (targetUnit.IsTeamA == _minecraftUnit.IsTeamA) continue;
            
            targetUnit.TakeDamage(damage);
            _minecraftUnit.Capacity.AddMana(damage);
            hasHit = true;
            
            Vector3 knockbackVector = knockbackHorizontalForce * (target.transform.position - transform.position).normalized 
                                      + knockbackVerticalForce *  Vector3.up;
            
            // logic specific if targetUnit is MinecraftUnit
            if (targetUnit is MinecraftUnit)
            {
                MinecraftUnit minecraftTarget = (MinecraftUnit)targetUnit;
                minecraftTarget.StartCoroutine(minecraftTarget.MovementHandler.TakeImpulse(knockbackVector));
            }
            
            
        }
        // Attack animation
        if (_minecraftUnit.Animator && hasHit)
        {
            _minecraftUnit.Animator.SetTrigger("Attack");
        }
        
        return hasHit;
    }

    private Collider[] DetectTargets()
    {
        Collider[] hitColliders;
    
        switch (attackShape)
        {
            case SphereCollider sphere:
                hitColliders = Physics.OverlapSphere(transform.position, sphere.radius, sphere.includeLayers);
                break;
            case BoxCollider box:
                hitColliders = Physics.OverlapBox(box.bounds.center, box.bounds.extents, box.transform.rotation, box.includeLayers);
                break;
            default:
                throw new ArgumentException("Only sphere or box are supported");
        }

        return hitColliders;
        
    }
    
     
}
