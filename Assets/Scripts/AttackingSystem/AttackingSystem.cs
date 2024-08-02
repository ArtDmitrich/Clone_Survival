using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AttackingSystem : MonoBehaviour
{
    [SerializeField] private List<AutoShootWeapon> _shootingWeaponList;
    [SerializeField] private float _detectionRadius;
    [SerializeField] private LayerMask _targetLayer;

    private TargetRadar _targetRadar;

    private void Start()
    {
        _targetRadar = new TargetRadar();
    }

    private void Update()
    {
        SetTargetToWeapons();
    }

    private void SetTargetToWeapons()
    {
        for (int i = 0; i < _shootingWeaponList.Count; i++)
        {
            if (_shootingWeaponList[i].Target == null)
            {
                _shootingWeaponList[i].Target = _targetRadar.FindNearestTarget(transform.position, _detectionRadius, _targetLayer);
            }
        }
    }

}
