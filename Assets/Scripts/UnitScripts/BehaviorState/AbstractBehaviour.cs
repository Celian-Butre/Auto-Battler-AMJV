using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MinecraftUnit))]
[RequireComponent(typeof(MovementHandler))]
public abstract class AbstractBehaviour : MonoBehaviour
{
    [SerializeField] private float pathFps = 1.0f;
    [SerializeField] private float attackFps = 5.0f;
    
    protected abstract void MoveAction();
    protected abstract void AttackAction();
    
    protected MinecraftUnit Unit;
    

    void Start()
    {
        Unit = GetComponent<MinecraftUnit>();
        StartCoroutine(attackUpdate());
        StartCoroutine(pathUpdate());
    }
    
    // Path update and attack update can be expansive, so we don't do that every frame. We create custom update
    // We create custom update at low fps to handle this without performance issues

    private IEnumerator attackUpdate()
    {
        while (true)
        {
            AttackAction();
            yield return new WaitForSeconds(1.0f/attackFps); 
        }
    }
    
    
    private IEnumerator pathUpdate()
    {
        while (true)
        {
            MoveAction();
            yield return new WaitForSeconds(1.0f/pathFps); 
        }
    }
}
