using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableEnemy : MovableCharacter
{
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
}
