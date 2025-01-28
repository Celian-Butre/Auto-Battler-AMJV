using System.Collections;
using UnityEngine;

public class ExplosifDuck : MonoBehaviour
{

    private Rigidbody rib;
    float Speed;
    float Cooldown=10.0f;
    [SerializeField] float explosionRadius;
    [SerializeField] float explosionForce;
    [SerializeField] float RangeExplosion;
    private float upwardModifier = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<AttackCAC>().changeCACouDistance(true);
        Speed = GetComponent<BaseDuckScript>().getSpeed();
        AttackCAC.ATTACK += Attack;
    }

    void Attack()
    {
        Debug.Log("Attaque");
        Explode();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject Target = GetComponent<AttackCAC>().GetTarget();
        if (Target != null)
        {
            Vector3 RangeWeapon = Target.transform.position - transform.position;
            if (RangeWeapon.magnitude < RangeExplosion)
            {
                Attack();
            }
        }


        if (Input.GetKey(KeyCode.B))
        {
            StartCoroutine(Boost());
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Explosion");
            Explode();

        }
    }
    //Le speed est utilisé ici pour son spécial lui permettant de boost. On peut créer une fonction public dans BaseDuckScript getSpeed
    //Et changeSpeed permettant de manipuler la Speed du duck. Vestige de l'ancien code qui ne mérite pas d'être supprimé actuellement
    //C'est un cut content, donc ça passe
    IEnumerator Boost()
    {
        Speed=12.0f;
        yield return new WaitForSeconds(Cooldown);
        Speed = 6.0f;

    }
    //Prend tout les rigidbody sauf le sien et leurs applique une force pour les expulser
    void Explode()
    {
        rib = GetComponent<Rigidbody>();
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
        Destroy(this.gameObject);
    }

    //On tue le signal pour eviter tout problemes (conseil de Game Jam)
    void OnDestroy()
    {
        AttackCAC.ATTACK -= Attack;
    }

}
