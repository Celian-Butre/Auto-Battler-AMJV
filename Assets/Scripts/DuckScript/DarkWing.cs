using UnityEngine;
using System.Collections;

public class DarkWing : MonoBehaviour
{
    [SerializeField] private GameObject Bout;
    [SerializeField] private GameObject Balle;
    [SerializeField] private GameObject Sword;
    [SerializeField] private GameObject Gun;
    private float Cooldown;
    [SerializeField] private float ForceTir;
    [SerializeField] private float Range;
    private Rigidbody rib;
    private bool Shoot = true;
    float RotSpeed = 300.0f;
    private bool BladeGun; //True = Blade, False = Gun
    private GameObject Target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AttackCAC.ATTACK += Attack;
        BladeGun = true;
    }

    // Update is called once per frame

    void Attack()
    {
        Debug.Log("Attaque");
        if (!BladeGun) 
        {
            if(Shoot)
            {
                StartCoroutine(Tir());
            }
        }
        else
        {
            StartCoroutine(Rotate360());
        }
        

    }
    void Update()
    {
        //Choose which weapon to use against enemy
        Target=GetComponent<AttackCAC>().GetTarget();
        if (Target != null)
        {
            Vector3 RangeWeapon = Target.transform.position - transform.position;
            if (RangeWeapon.magnitude > Range)
            {
                BladeGun = false;
            }
            else
            {
                BladeGun = true;
            }
        }

        if (!BladeGun) //Gun
        {
            Sword.SetActive(false);
            Gun.SetActive(true);

            if (Input.GetKeyDown(KeyCode.R))
            {

                if (Shoot)
                {
                    Debug.Log("Attaque Shoot");
                    StartCoroutine(Tir());
                }

            }
        }
        else //Sword
        {
            Sword.SetActive(true);
            Gun.SetActive(false);

            if (Input.GetKeyDown(KeyCode.R))
            {

                if (Shoot)
                {
                    Debug.Log("Attaque Sword");
                    StartCoroutine(Rotate360());
                }

            }
        }


        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("DeRender");
            //SetRenderState(Sword,false);
            Sword.SetActive(false);
        }
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Special");
            StartCoroutine(Special());
        }
    }
    //Tir. La variable Shoot est le garde-fou pour �viter de tirer sans prendre en compte le cooldown
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

        yield return new WaitForSeconds(5.0f);

    }

    IEnumerator Rotate360()
    {

        bool IsFinish = true;
        //Clairement pas ouf, mais je sais faire autrement. Tourne jusqu'� ce que les conditions match
        //Ie que les 2 bool soit faux, l'un s'active quand il est proche de 0 degr�, l'autre s'active apr�s une demie rotation;
        while (IsFinish || Mathf.Abs(Sword.transform.localRotation.x) > 0.05f)
        {
            //Debug.Log(Mathf.Abs(Sword.transform.localRotation.x));
            Sword.transform.Rotate(RotSpeed * Time.deltaTime, 0.0f, 0.0f);
            if (Mathf.Abs(Sword.transform.localRotation.x) > 0.1f)
            {
                IsFinish = false;
            }
            yield return null;
        }
        Sword.transform.Rotate(RotSpeed * Time.deltaTime, 0.0f, 0.0f);
    }
    //On tue le signal pour �viter tout probl�mes (conseil de Game Jam)
    void OnDestroy()
    {
        AttackCAC.ATTACK -= Attack;
    }
}
