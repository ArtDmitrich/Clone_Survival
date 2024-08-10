using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] private List<Upgrade> _upgrades = new List<Upgrade>();

    public Upgrade GetUpgrade(int index)
    {
        return _upgrades[index];
    }

}
