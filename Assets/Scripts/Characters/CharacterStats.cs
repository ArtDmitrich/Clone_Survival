using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float CurrentHealth
    {
        set
        {
            if (value < 0)
            {
                value += Defence;
            }

            _currentHealth += value;

            if (_currentHealth > MaxHealth)
            {
                _currentHealth = MaxHealth;
            }
        }
        get { return _currentHealth; }
    }
    public float MaxHealth
    {
        get { return _maxHealthBase * _maxHealthMultiplier; }
    }
    public float HealthPerSec
    {
        get { return _healthPerSecBase * _healthPerSecMultiplier; }
    }
    public float Damage
    {
        get { return _damageBase * _damageMultiplier; }
    }
    public float MovementSpeed
    {
        get { return _movementSpeedBase * _movementSpeedMultiplier; }
    }
    public float Defence
    {
        get { return _defenceBase * _defenceMultiplier; }
    }

    [SerializeField] private float _currentHealth;
    [SerializeField] private int _maxHealthBase;
    [SerializeField] private float _healthPerSecBase;

    [SerializeField] private int _damageBase;

    [SerializeField] private float _movementSpeedBase;
    [SerializeField] private int _defenceBase;

    private float _maxHealthMultiplier = 1.0f;
    private float _healthPerSecMultiplier = 1.0f;
    private float _damageMultiplier = 1.0f;
    private float _movementSpeedMultiplier = 1.0f;
    private float _defenceMultiplier = 1.0f;

    public void AddToStatMultipliers(float maxHealth, float healthPerSec, float damage, float movementSpeed, float defence)
    {
        _maxHealthMultiplier += maxHealth;
        _healthPerSecMultiplier += healthPerSec;
        _damageMultiplier += damage;
        _movementSpeedMultiplier += movementSpeed;
        _defenceMultiplier += defence;
    }
}
