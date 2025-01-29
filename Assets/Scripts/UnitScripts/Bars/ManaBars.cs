using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ManaBars : MonoBehaviour
{
    [SerializeField] private BaseCapacity capacity;
    [SerializeField] private Slider manaSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manaSlider.maxValue = capacity.MaxMana;
    }
    void Update()
    {
        manaSlider.value = capacity.Mana;
    }
    // Child of healthbar, rotation is handled by healthbar

    
}
