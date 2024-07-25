using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    public UnityAction CharacterDied;
    public UnityAction CharacterResurrected;

    public int MaxHealth;
    public int AutoHealPerSec;

    private bool _issAutoHealing;
    private float _currentHealth;

    public void Init()
    {
        _currentHealth = MaxHealth;
        CharacterResurrected?.Invoke();
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
        _currentHealth += value;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            CharacterDied?.Invoke();
        }
        else if (_currentHealth > MaxHealth)
        {
            _currentHealth = MaxHealth;
        }
    }

    private void CheckAutoHealing()
    {
        _issAutoHealing = _currentHealth < MaxHealth;
    }

    private void Update()
    {
        CheckAutoHealing();

        if (_issAutoHealing)
        {
            ChangeCurrentHealth(AutoHealPerSec * Time.deltaTime);
        }
    }
}
