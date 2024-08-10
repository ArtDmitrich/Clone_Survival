using UnityEngine;

public class CharacterStatsUpgrade : Upgrade
{
    [SerializeField] private float _maxHealth = 0.0f;
    [SerializeField] private float _healthPerSec = 0.0f;
    [SerializeField] private float _damage = 0.0f;
    [SerializeField] private float _movementSpeed = 0.0f;
    [SerializeField] private float _defence = 0.0f;

    public void UpgradeStats(CharacterStats stats)
    {
        stats.AddToStatMultipliers(_maxHealth, _healthPerSec, _damage, _movementSpeed, _defence);
    }
}
