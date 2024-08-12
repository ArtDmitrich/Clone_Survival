using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableEnemy : MovableCharacter
{
    [SerializeField] private MeleeWeapon weapon;

    public Transform Target
    {
        get { return _target; }
        set
        {
            _target = value;
            _directionToMove = GetDirectionToMove();
            Movement.StartMovement(_directionToMove);
        }
    }

    protected Vector2 _directionToMove;
    protected PooledItem PooledItem { get { return _pooledItem = _pooledItem ?? GetComponent<PooledItem>(); } }
    private PooledItem _pooledItem;
    private Transform _target;

    protected Vector2 GetDirectionToMove()
    {
        var vectorToTarget = Target.position - transform.position;
        return vectorToTarget.normalized;
    }

    protected override void Death()
    {
        base.Death();
        PooledItem.Release();
    }
    private void SetAdditionalDamageToMeleeWeapon(float additionalDamage)
    {
        if (weapon != null)
        {
            weapon.SetAdditionalDamage(additionalDamage);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        CharacterStats.DamageChanged += SetAdditionalDamageToMeleeWeapon;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        CharacterStats.DamageChanged -= SetAdditionalDamageToMeleeWeapon;
    }
}
