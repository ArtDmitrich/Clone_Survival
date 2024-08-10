using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Updates/Weapon", order = 1)]

public class WeaponUpgrade : Upgrade
{
    [SerializeField] private Weapon _prefab;
    public override void Activate(PlayerController player)
    {
        var weapon = Object.Instantiate(_prefab);
        //TODO: add to PlayerController AttackingSystem and add logic to change weapon
    }
}
