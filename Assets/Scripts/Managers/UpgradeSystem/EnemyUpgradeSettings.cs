using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Upgrades/EnemyUpgradeSettings", order = 1)]
public class EnemyUpgradeSettings : ScriptableObject
{
    [SerializeField] private List<CharacterStatsUpgrade> _upgrades = new List<CharacterStatsUpgrade>();

    public CharacterStatsUpgrade GetUpgrade(int index)
    {
        if (_upgrades.Count == 0)
        {
            return null;
        }

        if (index < 0) 
        {
            index = 0;
        }
        else if (index >= _upgrades.Count)
        {
            index = _upgrades.Count - 1;
        }

        return _upgrades[index];
    }
}
