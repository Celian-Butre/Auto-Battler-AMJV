using UnityEngine;
using System.Collections;

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
        GetComponent<AttackCAC>().changeCACouDistance(false);
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
        //rib.linearVelocity = Vector3.zero;
/*        
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
*/
    }
    //Tir. La variable Shoot est le garde-fou pour �viter de tirer sans prendre en compte le cooldown
    IEnumerator Tir()
    {
        Shoot = false;
        GameObject BULLET = Instantiate(Balle, Bout.transform.position, Bout.transform.rotation);
        BULLET.GetComponent<Lazer>().parent = this.gameObject;
        BULLET.GetComponent<Lazer>().damage = GetComponent<AttackCAC>().GetDamage();
        Rigidbody rb = BULLET.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * ForceTir, ForceMode.Impulse);
        yield return null;
        Shoot = true;
    }
    //Le cooldown est utilisé ici pour son spécial lui permettant de mitrailler. On peut créer une fonction public dans BaseDuckScript getCooldown
    //Et changeCooldown permettant de manipuler le cooldown du duck. Vestige de l'ancien code qui ne mérite pas d'être supprimé actuellement
    //C'est un cut content, donc ça passe
    IEnumerator Special()
    {
        float BackupCooldown = Cooldown;
        Cooldown = 1.0f;
        yield return new WaitForSeconds(5.0f);
        Cooldown = BackupCooldown;
    }
    //On tue le signal pour eviter tout problemes (conseil de Game Jam)
    void OnDestroy()
    {
        AttackCAC.ATTACK -= Attack;
    }
}
