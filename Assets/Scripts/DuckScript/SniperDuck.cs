using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class SniperDuck : MonoBehaviour
{
    [SerializeField] private GameObject Bout;
    [SerializeField] private GameObject Balle;
    private float Cooldown;
    [SerializeField] private float ForceTir;
    private Rigidbody rib;
    private bool Shoot = true;
    Vector3 STAY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AttackCAC.ATTACK += Attack;
        STAY=transform.position;
    }

    // Update is called once per frame

    void Attack()
    {
        if(Shoot)
        {
            Debug.Log("Attaque");
            StartCoroutine(Tir());
        }
    }
    void Update()
    {
        transform.position = STAY;
        rib = GetComponent<Rigidbody>();
        rib.linearVelocity = Vector3.zero;
        
        if (Input.GetKeyDown(KeyCode.R))
        {

            if (Shoot)
            {
                Debug.Log("Attaque");
                StartCoroutine(Tir());
            }

        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Special");
            StartCoroutine(Special());
        }
    }
    //Tir. La variable Shoot est le garde-fou pour éviter de tirer sans prendre en compte le cooldown
    IEnumerator Tir()
    {
        Shoot = false;
        GameObject BULLET = Instantiate(Balle, Bout.transform.position, Bout.transform.rotation);
        Rigidbody rb = BULLET.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * ForceTir, ForceMode.Impulse);
        yield return new WaitForSeconds(Cooldown);
        Shoot = true;
    }

    IEnumerator Special()
    {
        float BackupCooldown = Cooldown;
        Cooldown = 1.0f;
        yield return new WaitForSeconds(5.0f);
        Cooldown = BackupCooldown;
    }
    //On tue le signal pour éviter tout problèmes (conseil de Game Jam)
    void OnDestroy()
    {
        AttackCAC.ATTACK -= Attack;
    }
}
