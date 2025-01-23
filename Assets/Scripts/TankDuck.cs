using UnityEngine;
using System.Collections;

public class TankDuck : MonoBehaviour
{
    [SerializeField] private GameObject Bout;
    [SerializeField] private GameObject Balle;
    [SerializeField] private float Cooldown;
    [SerializeField] private float ForceTir;
    private bool Shoot = true;
    private Rigidbody rib;
    float RotSpeed = 300.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //à supprimer
        float SPEED = 3.0f;
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
            if(Shoot)
            {
                Debug.Log("Attaque");
                StartCoroutine(Tir());
            }
        }

    }

    IEnumerator Tir()
    {
        Shoot = false;
        GameObject BULLET = Instantiate(Balle, Bout.transform.position, Bout.transform.rotation);
        Rigidbody rb = BULLET.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * ForceTir, ForceMode.Impulse);
        yield return new WaitForSeconds(Cooldown);
        Shoot = true;
    }
}


