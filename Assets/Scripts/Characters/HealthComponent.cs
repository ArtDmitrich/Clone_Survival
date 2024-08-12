using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    public UnityAction CharacterDied;
    public UnityAction<float> HealthRationChanged;

    private bool _isAutoHealing;
    private bool _isAlive;

    private CharacterStats _characterStats;

    public void Init(CharacterStats characterStats)
    {
        _characterStats = characterStats;
        _characterStats.CurrentHealth = _characterStats.MaxHealth;
        _isAlive = true;
    }

    public void GetDamage(float value)
    {
        ChangeCurrentHealth(-value);
    }

    public void Heal(float value)
    {
        ChangeCurrentHealth(value);
    }

    private void ChangeCurrentHealth(float value)
    {
        _characterStats.CurrentHealth = value;
        var currentHealth = _characterStats.CurrentHealth;

        if (currentHealth <= 0)
        {
            CharacterDied?.Invoke();
            _isAlive = false;
        }

        HealthRationChanged?.Invoke(currentHealth / _characterStats.MaxHealth);
    }

    private void CheckAutoHealing(out float healthPerSec)
    {
        healthPerSec = _characterStats.HealthPerSec;

        if (healthPerSec <= 0.01f)
        {
            _isAutoHealing = false;
            return;
        }

        _isAutoHealing = _characterStats.CurrentHealth < _characterStats.MaxHealth;
    }

    private void Update()
    {
        if (!_isAlive)
        {
            return;
        }

        CheckAutoHealing(out var healthPerSec);

        if (_isAutoHealing)
        {
            ChangeCurrentHealth(healthPerSec * Time.deltaTime);
        }
    }
}
