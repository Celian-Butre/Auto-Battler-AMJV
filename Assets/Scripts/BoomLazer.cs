using UnityEngine;

public class BoomLazer : MonoBehaviour
{
    private float i = 0.0f;
    Rigidbody rib;
    [SerializeField] float explosionRadius;
    [SerializeField] float explosionForce;
    private float upwardModifier = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        rib=GetComponent<Rigidbody>();
    }


    //Prend tout les rigidbody sauf le sien et leurs applique une force pour les expulser
    private void OnTriggerEnter(Collider other)
    {
        //Uniformisation des codes de balles, pour la détruire au 2e impact avec un collider dû au fait qu'elle spawn DANS un collider
        //Reste clairement Junky et est sujet à amélioration
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
