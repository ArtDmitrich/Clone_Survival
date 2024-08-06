using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    public UnityAction CharacterDied;
    public UnityAction<float> HealthRationChanged;

    public int MaxHealth
    {
        get { return _maxHealth; }
    }
    public int CurrentHealth
    {
        get { return (int)_currentHealth; }
    }
    public int HealthPerSec
    {
        get { return _autoHealPerSec; }
    }

    [SerializeField] private int _maxHealth;
    [SerializeField] private int _autoHealPerSec;

    private bool _isAutoHealing;
    private float _currentHealth;

    public void Init()
    {
        _currentHealth = _maxHealth;
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
        else if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }

        HealthRationChanged?.Invoke(_currentHealth/_maxHealth);
    }

    private void CheckAutoHealing()
    {
        if (_autoHealPerSec == 0)
        {
            _isAutoHealing = false;
        }

        _isAutoHealing = _currentHealth < _maxHealth;
    }

    private void Update()
    {
        CheckAutoHealing();

        if (_isAutoHealing)
        {
            ChangeCurrentHealth(_autoHealPerSec * Time.deltaTime);
        }
    }
}
