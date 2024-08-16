using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Upgrades/CharacterStats", order = 1)]
public class CharacterStatsUpgrade : Upgrade
{
    public float MaxHealth { get { return _maxHealth; } }
    public float HealthPerSec { get { return _healthPerSec; } }
    public float Damage { get { return _damage; } }
    public float MovementSpeed { get { return _movementSpeed; } }
    public float Defence { get { return _defence; } }

    [SerializeField] private float _maxHealth = 0.0f;
    [SerializeField] private float _healthPerSec = 0.0f;
    [SerializeField] private float _damage = 0.0f;
    [SerializeField] private float _movementSpeed = 0.0f;
    [SerializeField] private float _defence = 0.0f;

    public override void Activate(PlayerController player)
    {
        player.CharacterStats.AddToStatMultipliers(_maxHealth, _healthPerSec, _damage, _movementSpeed, _defence);
    }
}
