using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;

public class Cost : MonoBehaviour
{
    private int cost;
    [SerializeField] TextMeshProUGUI texteCout;
    [SerializeField] TextMeshProUGUI unit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cost = GlobalsVariable.prices[unit.text];
        texteCout.text = cost.ToString();
    }
}
