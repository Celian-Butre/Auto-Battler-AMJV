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
        if (baseDuckScript.getGameManagerScript().combatPhase)
        {
            if (canAttack)
            {
                Collider[] hits = Physics.OverlapSphere(transform.position, range, duckLayer);
                GameObject targetToAttack = null;
                float distanceToChosenTarget = range;
                bool targetFound = false;
                foreach (Collider hit in hits)
                {
                    Vector3 directionToTarget = hit.transform.position - transform.position;
                    float distanceToTarget = directionToTarget.magnitude;
                    if (baseDuckScript.getAttackMode() == 3)
                    {
                        distanceToTarget = Vector3.Distance(baseDuckScript.getArmyManagerScript().getCrownDuck(baseDuckScript.getTeam()).transform.position, hit.transform.position);
                    }

                    if (!Physics.Raycast(transform.position, directionToTarget.normalized, distanceToTarget,
                            groundLayer))
                    {
                        if (baseDuckScript.getArmyManagerScript().getArmy(!baseDuckScript.getTeam()).Contains(hit.gameObject))
                        {
                            if (baseDuckScript.getAttackMode() == 1)
                            {
                                if (baseDuckScript.getArmyManagerScript().getCrownDuck(!baseDuckScript.getTeam()) ==
                                    hit.gameObject)
                                {
                                    targetToAttack = hit.gameObject;
                                    targetFound = true;
                                }
                            }

                            if (baseDuckScript.getAttackMode() == 2 || baseDuckScript.getAttackMode() == 3)
                            {
                                if (distanceToTarget < distanceToChosenTarget)
                                {
                                    distanceToChosenTarget = distanceToTarget;
                                    targetToAttack = hit.gameObject;
                                    targetFound = true;
                                }
                            }
                        }
                    }

                }

                if (targetFound)
                {
                    Attack(targetToAttack);
                }
            }
        }
    }

    private void Attack(GameObject attackTarget)
    {
        StartCoroutine(coolDown());
        attackTarget.GetComponent<BaseDuckScript>().TakeDamage(damage);
    } 
}
