using UnityEngine;

public class AttackProjectile : AttackHandler
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float arrowBaseSpeed;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private bool directShot;

    
    public override bool Attack()
    {
        // If no target (target is dead an destroyed)
        if (!_minecraftUnit.MovementHandler.TargetUnit) _minecraftUnit.MovementHandler.UpdateNearest();
        float launchAngle = findLaunchAngle();
        //print(launchAngle);
        // If target not reachable
        if (launchAngle < 0) return false;
        
        GameObject arrow = Instantiate(arrowPrefab, spawnPos.position, spawnPos.rotation);
        ProjectileHandler projectileHandler = arrow.GetComponent<ProjectileHandler>();
        // In target <-> launcher + transform.up basis
        Vector2 localLaunchVector = arrowBaseSpeed * new Vector2(Mathf.Cos(launchAngle), Mathf.Sin(launchAngle));
        // Transform it in global basis
        AbstractUnit targetUnit = _minecraftUnit.MovementHandler.TargetUnit;
        Vector3 diffVector = Vector3.ProjectOnPlane(targetUnit.transform.position - spawnPos.position, Vector3.up);
        
        Vector3 launchVectorNormalized = (localLaunchVector.x * diffVector.normalized + localLaunchVector.y * Vector3.up).normalized;
        projectileHandler.LaunchProjectile(launchVectorNormalized * arrowBaseSpeed, _minecraftUnit.IsTeamA, _minecraftUnit);
        
        return true;
    }

    private float findLaunchAngle()
    {
        // Source : https://en.wikipedia.org/wiki/Projectile_motion#Angle_%CE%B8_required_to_hit_coordinate_(x,_y)
        
        AbstractUnit targetUnit = _minecraftUnit.MovementHandler.TargetUnit;
        Vector3 diffVector = targetUnit.transform.position - spawnPos.position;
        Vector3 projectOnPlane = Vector3.ProjectOnPlane(diffVector, Vector3.up);
        
        float x = Vector3.ProjectOnPlane(projectOnPlane, Vector3.up).magnitude;
        float y = diffVector.y;
        float g = Physics.gravity.magnitude;
        float v = arrowBaseSpeed;
        float v_sqr = v * v;
        //print("x : " + x);
        
        float inside_sqrt_root = v_sqr*v_sqr - g*(g*x*x + 2*y*v_sqr);
        if (inside_sqrt_root < 0.0f) return -1f; // Can't reach target
        
        // directShot is the smallest angle, undirectShot shot is the biggest angle
        float numerator = directShot ? v_sqr - Mathf.Sqrt(inside_sqrt_root) : v_sqr + Mathf.Sqrt(inside_sqrt_root);
        float inside_arctan = numerator / (g * x);
        return Mathf.Atan(inside_arctan);
    }

    
}
