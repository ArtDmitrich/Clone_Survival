using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour, ITakingDamage
{
    public UnityAction<Character> CharacterDead;

    public HealthComponent HealthComponent { get { return _healthComponent = _healthComponent ?? GetComponent<HealthComponent>(); } }
    private HealthComponent _healthComponent;

    public CharacterStats CharacterStats { get { return _characterStats = _characterStats ?? GetComponent<CharacterStats>(); } }
    private CharacterStats _characterStats;

    protected void Awake()
    {
        HealthComponent.Init(CharacterStats);
    }

    public virtual void TakeDamage(float damage)
    {
        HealthComponent.GetDamage(damage);
    }

    protected virtual void Death()
    {
        CharacterDead?.Invoke(this);
    }

    protected virtual void OnEnable()
    {
        HealthComponent.CharacterDied += Death;
        HealthComponent.Init(CharacterStats);
    }

    protected virtual void OnDisable()
    {
        HealthComponent.CharacterDied -= Death;
    }
}
