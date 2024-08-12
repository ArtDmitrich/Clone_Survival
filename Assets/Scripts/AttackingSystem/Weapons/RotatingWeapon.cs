using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingWeapon : Weapon
{
    [SerializeField] private List<MeleeWeapon> _weapons = new List<MeleeWeapon>();
    [SerializeField] private float _rotatingSpeed;

    public override void SetAdditionalDamage(float damage)
    {
        for (int i = 0; i < _weapons.Count; i++)
        {
            _weapons[i].SetAdditionalDamage(damage);
        }
    }

    private void Start()
    {
        for (int i = 0; i < _weapons.Count; i++)
        {
            _weapons[i].SetBaseDamage(_damageBase);
        }
    }

    private void Update()
    {
        transform.Rotate(0.0f, 0.0f, _rotatingSpeed * Time.deltaTime);
    }
}
