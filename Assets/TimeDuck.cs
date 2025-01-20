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
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null & rb != rib & rb.isKinematic==false)
            {
                rb.isKinematic=true;
                yield return new WaitForSeconds(Cooldown);
                rb.isKinematic = false;
            }
            
        }

    }
}
