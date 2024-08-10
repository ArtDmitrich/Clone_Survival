using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    public UnityAction CharacterDied;
    public UnityAction<float> HealthRationChanged;

    private bool _isAutoHealing;

    private CharacterStats _characterStats;

    public void Init(CharacterStats characterStats)
    {
        _characterStats = characterStats;
        _characterStats.CurrentHealth = _characterStats.MaxHealth;
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
        CheckAutoHealing(out var healthPerSec);

        if (_isAutoHealing)
        {
            ChangeCurrentHealth(healthPerSec * Time.deltaTime);
        }
    }
}
