using System.Collections;
using UnityEngine;

public class TimeDuck: MonoBehaviour
{

    private Rigidbody rib;
    float Speed;
    [SerializeField] float explosionRadius;
    [SerializeField] float RangeExplosion;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Speed = GetComponent<BaseDuckScript>().getSpeed();
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
/*
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
*/
    }
    //Le speed est utilisé ici pour son spécial lui permettant de boost. On peut créer une fonction public dans BaseDuckScript getSpeed
    //Et changeSpeed permettant de manipuler la Speed du duck. Vestige de l'ancien code qui ne mérite pas d'être supprimé actuellement
    //C'est un cut content, donc ça passe
    IEnumerator Boost()
    {
        Speed = 12.0f;
        yield return null;
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
                yield return null;
                rb.isKinematic = false;
                collider.GetComponent<AttackCAC>().enabled = true;
            }
            

        }
        Destroy(gameObject);

    }
}
