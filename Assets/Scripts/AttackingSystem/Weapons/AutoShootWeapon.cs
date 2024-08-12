using System.Collections;
using UnityEngine;
using UnityEngine.Windows;
using Zenject;

public class AutoShootWeapon : Weapon
{
    public Transform Target;
    public float MaxDistanceToShoot;

    [SerializeField] private BulletType _bulletType;
    [SerializeField] private bool _isPercing;
    [SerializeField] private float _bulletSpeedMultiplier = 1.0f;

    private float _sqrMaxDistanceToShoot;
    private BulletManager _bulletManager;

    private void Start()
    {
        _canAttack = true;
        _sqrMaxDistanceToShoot = MaxDistanceToShoot * MaxDistanceToShoot;
        _bulletManager = BulletManager.Instance;
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

        var bullet = _bulletManager.GetBullet(_bulletType.ToString());

        if (bullet.gameObject.TryGetComponent<IMovable>(out var movement))
        {
            bullet.Init(_targetLayer, DamageValue, _isPercing);
            bullet.transform.position = transform.position;

            movement.StartMovement(direction);
        }
    }

    private bool CheckDistanceToTarget(Vector3 vectorToTarget)
    {
        return vectorToTarget.sqrMagnitude <= _sqrMaxDistanceToShoot;
    }
}
