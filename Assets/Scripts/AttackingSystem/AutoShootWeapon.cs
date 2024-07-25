using System.Collections;
using UnityEngine;

public class AutoShootWeapon : Weapon
{
    public Transform Target;

    [SerializeField] private BulletType _bulletType;
    [SerializeField] private bool _isPercing;
    [SerializeField] private float _maxDistanceToShoot;
    [SerializeField] private float _bulletSpeedMultiplier = 1.0f;

    private float _sqrMaxDistanceToShoot;

    private void Start()
    {
        _sqrMaxDistanceToShoot = _maxDistanceToShoot * _maxDistanceToShoot;
    }

    private void Update()
    {
        if (!_canAttack || Target == null)
        {
            return;
        }

        if (Target.gameObject.activeInHierarchy)
        {
            var vectorToTarget = Target.position - transform.position;
        
            if (CheckDistanceToTarget(vectorToTarget))
            {
                Shoot(vectorToTarget.normalized * _bulletSpeedMultiplier);
                //return;
            }
            else
            {
                Target = null;
            }
        }
        else
        {
            Target = null;
        }
    }
    
    private void Shoot(Vector3 direction)
    {
        StartCoroutine(AttackingColdown(_coldownTime));

        var bullet = BulletManager.Instance.GetBullet(_bulletType.ToString());

        if (bullet.gameObject.TryGetComponent<IMovable>(out var movement))
        {
            bullet.Init(_targetLayer, _damageValue, _isPercing);
            bullet.transform.position = transform.position;

            movement.StartMovement(direction);
        }
    }

    private bool CheckDistanceToTarget(Vector3 vectorToTarget)
    {
        return vectorToTarget.sqrMagnitude <= _sqrMaxDistanceToShoot;
    }
}
