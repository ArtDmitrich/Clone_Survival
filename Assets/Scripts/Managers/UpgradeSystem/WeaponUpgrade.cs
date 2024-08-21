using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Upgrades/Weapon", order = 1)]

public class WeaponUpgrade : Upgrade
{
    public int Level;
    public Weapon Prefab;

    public override void Activate(PlayerController player)
    {
        var weapon = Object.Instantiate(Prefab);
        player.AttackingSystem.AddNewWeapon(weapon);
    }
}
