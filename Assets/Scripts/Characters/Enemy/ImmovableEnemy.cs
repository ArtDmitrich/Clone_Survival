using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Zenject;

public class ImmovableEnemy : Character
{
    [SerializeField] private AttackingSystem _attackingSystem;

    private PooledItem PooledItem { get { return _pooledItem = _pooledItem ?? GetComponent<PooledItem>(); } }
    private PooledItem _pooledItem;

    protected override void Death()
    {
        base.Death();
        PooledItem.Release();
    }
    private void SetAdditionalDamageToAttackingSystem(float additionalDamage)
    {
        if (_attackingSystem != null)
        {
            _attackingSystem.AdditionalDamage = additionalDamage;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        CharacterStats.DamageChanged += SetAdditionalDamageToAttackingSystem;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        CharacterStats.DamageChanged -= SetAdditionalDamageToAttackingSystem;
    }
}
