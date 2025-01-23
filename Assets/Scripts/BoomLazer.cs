using UnityEngine;

public class BoomLazer : MonoBehaviour
{
    private float i = 0.0f;
    Rigidbody rib;
    [SerializeField] float explosionRadius;
    [SerializeField] float explosionForce;
    private float upwardModifier = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        i += 1;
        if (i > 0)
        {
            Vector3 explosionPosition = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);

            foreach (Collider collider in colliders)
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (rb != null & rb != rib)
                {
                    rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardModifier, ForceMode.Impulse);
                }
            }
            Destroy(gameObject);
        }
    }
}
