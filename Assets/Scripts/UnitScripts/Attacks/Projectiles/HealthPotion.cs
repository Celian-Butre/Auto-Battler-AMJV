using UnityEngine;
using System.Collections;


public class HealthPotion : ProjectileHandler
{
    [SerializeField] private float healthAdd;
    [SerializeField] private SphereCollider healthPotionEffectArea;
    [SerializeField] private GameObject explodeMesh;
    [SerializeField] private float exploseMeshTime = 0.5f;
    
    void OnCollisionEnter(Collision collision)
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, healthPotionEffectArea.radius, healthPotionEffectArea.includeLayers);
        foreach (Collider target in targets)
        {
            if (!target.CompareTag("Unit")) continue;
            // GetComponent is expensive in performance, optimize here if it's slow
            AbstractUnit targetUnit = target.GetComponent<AbstractUnit>();
            
            // No EnemyHealing 
            if (targetUnit.IsTeamA != FromTeamA) continue;
            
            targetUnit.Heal(healthAdd);
            _minecraftUnitOrigin.Capacity.AddMana(healthAdd*0.1f);
        }
        CoroutineManager.Instance.StartCoroutine(ExplodeVisual());
        Destroy(gameObject);
    }
    
    private IEnumerator ExplodeVisual()
    {
        GameObject explosion = Instantiate(explodeMesh, transform.position, Quaternion.identity);
        explosion.transform.parent = null;
        yield return new WaitForSeconds(exploseMeshTime);
        Destroy(explosion);
    }
}
