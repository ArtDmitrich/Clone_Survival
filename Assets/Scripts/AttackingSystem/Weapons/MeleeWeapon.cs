using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    private void Attack(ITakingDamage target, Vector3 attackPoint)
    {
        StartCoroutine(AttackingColdown(_coldownTime));

        target.TakeDamage(DamageValue);

        var impactEffect = EffectsManager.Instance.GetEffect(WeaponType.ToString());
        impactEffect.transform.position = attackPoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_targetLayer & (1 << collision.gameObject.layer)) != 0 && collision.gameObject.TryGetComponent<ITakingDamage>(out var target))
        {
            Attack(target, collision.gameObject.transform.position);

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_canAttack && (_targetLayer & (1 << collision.gameObject.layer)) != 0 && collision.gameObject.TryGetComponent<ITakingDamage>(out var target))
        {
            Attack(target, collision.gameObject.transform.position);
        }
    }
}
