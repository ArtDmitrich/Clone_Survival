using System.Collections.Generic;
using UnityEngine;

public class AttackingSystem : MonoBehaviour
{
    public float AdditionalDamage {
        get
        {
            return _additionalDamage;
        } 
        set
        {
            _additionalDamage = value;
            SetAdditionalDamageToWeapons();
        }
    }

    [SerializeField] private List<AutoShootWeapon> _shootingWeapons = new List<AutoShootWeapon>();
    [SerializeField] private List<RotatingWeapon> _rotatingWeapons = new List<RotatingWeapon>();
    [SerializeField] private float _detectionRadius;
    [SerializeField] private LayerMask _targetLayer;

    private float _additionalDamage;
    
    public void AddNewWeapon(Weapon weapon)
    {
        var obsoleteWeapon = FindWeapon(weapon.WeaponType);
        if (obsoleteWeapon != null)
        {
            RemoveWeapon(obsoleteWeapon);
        }

        weapon.transform.parent = transform;
        weapon.transform.position = transform.position;
        weapon.SetAdditionalDamage(AdditionalDamage);

        switch (weapon.WeaponType)
        {
            case WeaponType.StandartShooter:
            case WeaponType.ExplosionShooter:
                _shootingWeapons.Add(weapon as AutoShootWeapon); 
                break;
            case WeaponType.RotatingMeleeWeapon:
                _rotatingWeapons.Add(weapon as RotatingWeapon);
                break;
        }
    }

    public int GetWeaponLevel(WeaponType type)
    {
        var weapon = FindWeapon(type);

        if (weapon == null)
        {
            return 0;
        }

        return weapon.WeaponLevel;
    }

    private void RemoveWeapon(Weapon weapon)
    {
        switch (weapon.WeaponType)
        {
            case WeaponType.StandartShooter:
            case WeaponType.ExplosionShooter:
                _shootingWeapons.Remove(weapon as AutoShootWeapon);
                break;
            case WeaponType.RotatingMeleeWeapon:
                _rotatingWeapons.Remove(weapon as RotatingWeapon);
                break;
        }

        Destroy(weapon.gameObject);
    }

    private Weapon FindWeapon(WeaponType type)
    {
        for (int i = 0; i < _shootingWeapons.Count; i++)
        {
            if (type == _shootingWeapons[i].WeaponType)
            {
                return _shootingWeapons[i];
            }
        }

        for (int i = 0; i < _rotatingWeapons.Count; i++)
        {
            if (type == _rotatingWeapons[i].WeaponType)
            {
                return _rotatingWeapons[i];
            }
        }

        return null;
    }

    private void SetAdditionalDamageToWeapons()
    {
        for (int i = 0; i < _shootingWeapons.Count; i++)
        {
            _shootingWeapons[i].SetAdditionalDamage(AdditionalDamage);
        }

        for (int i = 0; i < _rotatingWeapons.Count; i++)
        {
            _rotatingWeapons[i].SetAdditionalDamage(AdditionalDamage);
        }
    }

    private void SetTargetToWeapons()
    {
        if (_shootingWeapons.Count ==  0)
        {
            return;
        }

        for (int i = 0; i < _shootingWeapons.Count; i++)
        {
            if (_shootingWeapons[i]?.Target == null)
            {
                _shootingWeapons[i].Target = TargetRadar.FindNearestTarget(transform.position, _shootingWeapons[i].MaxDistanceToShoot, _targetLayer);
            }
        }
    }
    private void Update()
    {
        SetTargetToWeapons();
    }
}
