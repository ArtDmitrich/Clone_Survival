using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected LayerMask _targetLayer;
    [SerializeField] protected float _damageValue;
    [SerializeField] protected float _coldownTime;

    protected bool _canAttack = true;
    
    protected IEnumerator AttackingColdown(float coldownTime)
    {
        _canAttack = false;

        yield return new WaitForSeconds(coldownTime);

        _canAttack = true;
    }
}
