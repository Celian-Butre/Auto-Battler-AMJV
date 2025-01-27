using GLTFast.Schema;
using System.Collections;
using UnityEngine;

public class CharDuck : MonoBehaviour
{
    private Rigidbody rib;
    float Speed = 5.0f;
    float Cooldown = 0.0f;
    [SerializeField] GameObject Lazer;
    [SerializeField] GameObject LazerBoom;
    [SerializeField] GameObject Gauche;
    [SerializeField] GameObject Droit;
    [SerializeField] GameObject Tete;
    [SerializeField] float ForceTir;
    bool Shoot = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //à supprimer

        float ROT = 100.0f;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("avancer");
            transform.Translate(Vector3.forward * Time.deltaTime * Speed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Debug.Log("reculer");
            transform.Translate(-Vector3.forward * Time.deltaTime * Speed);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("tourner la tête à gauche");
            transform.Rotate(0.0f, -ROT * Time.deltaTime, 0.0f, Space.Self);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("tourner la tête à droite");
            transform.Rotate(0.0f, ROT * Time.deltaTime, 0.0f, Space.Self);
        }
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
    }

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
        yield return new WaitForSeconds(Cooldown);
        Shoot = true;
    }

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
        yield return new WaitForSeconds(Cooldown);
        Shoot = true;
    }
}
