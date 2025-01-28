using UnityEngine;

public class Duck : MonoBehaviour
{

    [SerializeField] protected float baseMovementSpeed;
    [SerializeField] protected int baseHP;
    [SerializeField] protected int baseArmor;
    
    
    [SerializeField] protected float baseAttackSpeed;
    [SerializeField] protected int manaToSpecialAttack;
    [SerializeField] protected Weapon weapon;
    
    protected float currentMovementSpeed;
    protected int currentHP;
    protected int currentArmor;
    protected float currentAttackSpeed;


    //TODO WEAPON
    //TODO Special Ability

    public virtual void activateSpecialAbility(){

    }

    public virtual void takeDamage(int damage){
        currentHP -= (damage > currentArmor) ? (damage - currentArmor) : 0;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentMovementSpeed = baseMovementSpeed;
        currentHP = baseHP;
        currentArmor = baseArmor;
        currentAttackSpeed = baseAttackSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
