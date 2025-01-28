using System.Collections;
using UnityEngine;

public class CharDuck : MonoBehaviour
{
    private Rigidbody rib;
    [SerializeField] GameObject Lazer;
    [SerializeField] GameObject LazerBoom;
    [SerializeField] GameObject Gauche;
    [SerializeField] GameObject Droit;
    [SerializeField] GameObject Tete;
    [SerializeField] float ForceTir;
    private bool Shoot=true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<AttackCAC>().changeCACouDistance(false);
        AttackCAC.ATTACK += Attack;
    }

    void Attack()
    {
        if (Shoot)
        {
            Debug.Log("Attaque");
            StartCoroutine(Lasers());
        }
    }

    // Update is called once per frame
    void Update()
    {
/*
        //à supprimer

        
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (Shoot)
            {
                Debug.Log("Attack !!");
                StartCoroutine(Lasers());
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Explosion");
            StartCoroutine(Special());

        }
*/
    }
    //Tire des Lasers des yeux du pion vers la cible (Target). Donc rbg=RigidBodyGauche par exemple.
    IEnumerator Lasers()
    {
        Shoot = false;
        GameObject Target = GetComponent<AttackCAC>().GetTarget();
        Vector3 Destination = Vector3.Normalize(Target.transform.position-transform.position);
        GameObject LazGauche = Instantiate(Lazer, Gauche.transform.position, Gauche.transform.rotation);
        Rigidbody rbg = LazGauche.GetComponent<Rigidbody>();
        rbg.AddForce(Destination * ForceTir, ForceMode.Impulse);
        GameObject LazDroite = Instantiate(Lazer, Droit.transform.position, Droit.transform.rotation);
        Rigidbody rbd = LazDroite.GetComponent<Rigidbody>();
        rbd.AddForce(Destination * ForceTir, ForceMode.Impulse);
        yield return null;
        Shoot = true;
    }
    //Génere une sphère qui se dirige vers sa cible (Target) pour faire une explosion
    IEnumerator Special()
    {
        Shoot = false;
        GameObject Target = GetComponent<AttackCAC>().GetTarget();
        Vector3 Destination = Vector3.Normalize(Target.transform.position - transform.position);
        //Vector3 Salse = new Vector3(0, -1, 0);
        GameObject Laz = Instantiate(LazerBoom, Tete.transform.position, Tete.transform.rotation);
        Rigidbody rb = Laz.GetComponent<Rigidbody>();
        rb.AddForce(Destination * ForceTir, ForceMode.Impulse);
        //rb.AddForce(Salse * ForceTir, ForceMode.Impulse);
        yield return null;
        Shoot = true;
    }
    //On tue le signal pour éviter tout problèmes (conseil de Game Jam)
    void OnDestroy()
    {
        AttackCAC.ATTACK -= Attack;
    }
}
