using UnityEngine;
using UnityEngine.Serialization;

public class BaseCapacity : MonoBehaviour
{
    [field: SerializeField] public float MaxMana { get; private set; }
    [field: SerializeField] public float Mana { get; private set; }
    [SerializeField] private float manaCost;
    protected AbstractUnit _unit;

    
    
    // Called every frame
    protected virtual bool CapacityCall()
    {
        return true;
    }

    public void AddMana(float manaAdd)
    {
        Mana = Mathf.Min(Mana + manaAdd, MaxMana);
    }
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _unit = GetComponent<AbstractUnit>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mana >= manaCost)
        {
            bool capacityLaunched = CapacityCall();
            if (capacityLaunched) Mana -= manaCost;
        }
    }
}
