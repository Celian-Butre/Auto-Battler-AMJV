using System.Collections;
using UnityEngine;

public class CreeperBomb : AttackHandler
{
    [SerializeField] private GameObject explodeMesh;
    [SerializeField] private float exploseMeshTime = 0.5f;
    
    
    public override bool Attack()
    {
        bool hasExploded = base.Attack();
        if (hasExploded)
        {
            _minecraftUnit.HealthHandler.Death();
            CoroutineManager.Instance.StartCoroutine(ExplodeVisual());
            Destroy(gameObject);
        }
        return hasExploded;
    }

    private IEnumerator ExplodeVisual()
    {
        GameObject explosion = Instantiate(explodeMesh, transform.position, Quaternion.identity);
        explosion.transform.parent = null;
        yield return new WaitForSeconds(exploseMeshTime);
        Destroy(explosion);
    }
}
