using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    private void Attack(ITakingDamage target)
    {
        StartCoroutine(AttackingColdown(_coldownTime));

        target.TakeDamage(_damageValue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_targetLayer & (1 << collision.gameObject.layer)) != 0 && collision.gameObject.TryGetComponent<ITakingDamage>(out var target))
        {
            Attack(target);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_canAttack && (_targetLayer & (1 << collision.gameObject.layer)) != 0 && collision.gameObject.TryGetComponent<ITakingDamage>(out var target))
        {
            Attack(target);
        }
    }
}
