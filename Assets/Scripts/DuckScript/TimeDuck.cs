using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TimeDuck: MonoBehaviour
{

    private Rigidbody rib;
    float Speed = 5.0f;
    float Cooldown = 100.0f;
    [SerializeField] float explosionRadius;
    float explosionForce = 0.0f;
    private float upwardModifier = 0.0f;
    [SerializeField] float RangeExplosion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void Attack()
    {
        Debug.Log("Attaque");
        Explode();
    }

    // Update is called once per frame
    void Update()
    {
        //Prend sa cible et regarde sa distance pour voir quelle arme prendre contre lui
        GameObject Target = GetComponent<AttackCAC>().GetTarget();
        if (Target != null)
        {
            Vector3 RangeWeapon = Target.transform.position - transform.position;
            if (RangeWeapon.magnitude < RangeExplosion)
            {
                Attack();
            }
        }

        //à supprimer

        if (Input.GetKey(KeyCode.B))
        {
            StartCoroutine(Boost());
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Explosion");
            StartCoroutine(Explode());

        }
    }

    IEnumerator Boost()
    {
        Speed = 12.0f;
        yield return new WaitForSeconds(Cooldown);
        Speed = 5.0f;

    }

    IEnumerator Explode()
    {
        rib = GetComponent<Rigidbody>();
        Vector3 explosionPosition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
        foreach (Collider collider in colliders)
        {
            Debug.Log(collider);
            
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null & rb != rib & rb.isKinematic==false)
            {
                rb.isKinematic=true;
                collider.GetComponent<AttackCAC>().enabled = false;
                rb.linearVelocity=Vector3.zero;
                yield return new WaitForSeconds(Cooldown);
                rb.isKinematic = false;
                collider.GetComponent<AttackCAC>().enabled = true;
            }
            

        }
        Destroy(gameObject);

    }
}
