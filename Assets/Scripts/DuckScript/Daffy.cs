using UnityEngine;
using System.Collections;

public class Daffy : MonoBehaviour
{
    [SerializeField] private GameObject Sword;
    private Rigidbody rib;
    private float RotSpeed = 300.0f;
    [SerializeField] float explosionRadius;  
    [SerializeField] float explosionForce;  
    private float upwardModifier = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<AttackCAC>().changeCACouDistance(true);
        AttackCAC.ATTACK += Attack;
    }

    void Attack()
    {
        Debug.Log("Attaque");
        StartCoroutine(Rotate360());
    }

    // Update is called once per frame
    void Update()
    {
/*    
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
*/
    }

    IEnumerator Rotate360()
    {

        bool IsFinish = true;
        //Clairement pas ouf, mais je sais faire autrement. Tourne jusqu'à ce que les conditions match
        //Ie que les 2 bool soit faux, l'un s'active quand il est proche de 0 degré, l'autre s'active après une demie rotation;
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
    }
    //On tue le signal pour eviter tout problemes (conseil de Game Jam)
    void OnDestroy()
    {
        AttackCAC.ATTACK -= Attack;
    }
}

    

