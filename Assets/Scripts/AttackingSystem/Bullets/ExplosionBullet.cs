using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : Bullet
{
    [SerializeField] private float _explosionRadius;

    protected override void TakeDamage(ITakingDamage target)
    {
        base.TakeDamage(target);

        var colliders = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);

        foreach (var collider in colliders)
        {
            if (collider.gameObject.TryGetComponent<ITakingDamage>(out var otherTarget) && target != otherTarget)
            {
                otherTarget.TakeDamage(_damageValue);
            }
        }
    }
}
