using UnityEngine;
using System.Collections;
public class AttackCAC : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private float cooldown;
    private BaseDuckScript baseDuckScript;
    [SerializeField] private LayerMask duckLayer; // Layer for ducks
    [SerializeField] private LayerMask groundLayer;
    private bool canAttack = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        baseDuckScript = gameObject.GetComponent<BaseDuckScript>();
    }
    
    IEnumerator coolDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, range, duckLayer);
        
    }
}
