using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Should be attached to the Arrow, not the skeleton
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class ProjectileHandler : MonoBehaviour
{
    [SerializeField] protected float _lifeSpan = 8.0f;
    protected Rigidbody RigidBody;
    protected bool FromTeamA;
    protected MinecraftUnit _minecraftUnitOrigin;

    void Awake()
    {
        RigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Destroy after _lifeSpan, in all case
        Destroy(this.gameObject, _lifeSpan);
    }

    void Update()
    {
        // Align with speed
        if (RigidBody.linearVelocity.magnitude >= 1f) transform.forward = RigidBody.linearVelocity.normalized;
    }

    public void LaunchProjectile(Vector3 baseSpeed, bool fromTeamA, MinecraftUnit minecraftUnit)
    {
        RigidBody.linearVelocity = baseSpeed;
        FromTeamA = fromTeamA;
        _minecraftUnitOrigin = minecraftUnit;
    }
    
    
}
