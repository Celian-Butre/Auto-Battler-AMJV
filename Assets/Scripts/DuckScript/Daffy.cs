using UnityEngine;
using System.Collections;

public class Daffy : MonoBehaviour
{
    [SerializeField] private GameObject Sword;
    private Rigidbody rib;
    float RotSpeed = 300.0f;

    [SerializeField] float explosionRadius;  
    [SerializeField] float explosionForce;  
    private float upwardModifier = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //à supprimer
        float SPEED = 10.0f;
        float ROT = 100.0f;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("avancer");
            transform.Translate(Vector3.forward * Time.deltaTime * SPEED);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Debug.Log("reculer");
            transform.Translate(-Vector3.forward * Time.deltaTime * SPEED);
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Attaque");
            StartCoroutine(Rotate360());
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("DAFFY SMASH");
            StartCoroutine(Rotate360());
            Explode();

        }
    }

    IEnumerator Rotate360()
    {

        bool IsFinish = true;

        while (IsFinish || Mathf.Abs(Sword.transform.localRotation.x)>0.05f)
        {
            //Debug.Log(Mathf.Abs(Sword.transform.localRotation.x));
            Sword.transform.Rotate(RotSpeed * Time.deltaTime, 0.0f, 0.0f);
            if (Mathf.Abs(Sword.transform.localRotation.x)>0.1f)
            {
                IsFinish = false;
            }
            yield return null;
        }
        Sword.transform.Rotate(RotSpeed * Time.deltaTime, 0.0f, 0.0f);
    }

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
    }
}

    

