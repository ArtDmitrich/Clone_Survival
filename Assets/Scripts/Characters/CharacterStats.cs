using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterStats : MonoBehaviour
{
    public UnityAction<float> DamageChanged;

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
        if (maxHealth != 0.0f)
        {
            _maxHealthMultiplier += maxHealth;
        }

        if (healthPerSec != 0.0f)
        {
            _healthPerSecMultiplier += healthPerSec;
        }

        if (damage != 0.0f)
        {
            _damageMultiplier += damage;
            DamageChanged?.Invoke(Damage);
        }

        if (movementSpeed != 0.0f)
        {
            _movementSpeedMultiplier += movementSpeed;
        }

        if (defence != 0.0f)
        {
            _defenceMultiplier += defence;
        }
    }

    public void ResetStatsMultipliers()
    {
        _maxHealthMultiplier = 1.0f;
        _healthPerSecMultiplier = 1.0f;
        _damageMultiplier = 1.0f;
        _movementSpeedMultiplier = 1.0f;
        _defenceMultiplier = 1.0f;
    }
}
