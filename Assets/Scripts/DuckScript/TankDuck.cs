using UnityEngine;
using System.Collections;

public class TankDuck : MonoBehaviour
{
    [SerializeField] private GameObject Bout;
    [SerializeField] private GameObject Balle;
    [SerializeField] private float ForceTir;
    private bool Shoot = true;
    private Rigidbody rib;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AttackCAC.ATTACK += Attack;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Attack()
    {
        if (Shoot)
        {
            Debug.Log("Attaque");
            StartCoroutine(Tir());
        }
    }

    IEnumerator Tir()
    {
        Shoot = false;
        GameObject BULLET = Instantiate(Balle, Bout.transform.position, Bout.transform.rotation);
        Rigidbody rb = BULLET.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * ForceTir, ForceMode.Impulse);
        yield return null;
        Shoot = true;
    }
    //On tue le signal pour éviter tout problèmes (conseil de Game Jam)
    void OnDestroy()
    {
        AttackCAC.ATTACK -= Attack;
    }
}


