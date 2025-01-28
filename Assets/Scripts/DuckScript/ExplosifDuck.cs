using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosifDuck : MonoBehaviour
{

    private Rigidbody rib;
    float Speed = 6.0f;
    float Cooldown=10.0f;
    [SerializeField] float explosionRadius;
    [SerializeField] float explosionForce;
    [SerializeField] float RangeExplosion;
    private float upwardModifier = 0.0f;

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
        Destroy(gameObject);
    }


}
