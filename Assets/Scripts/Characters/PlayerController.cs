using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : MovableCharacter
{
    [SerializeField] private int _damageReduction;
    [Inject] private InputController _input;

    public override void TakeDamage(float damage)
    {
        damage -= _damageReduction;

        base.TakeDamage(damage);
    }

    private void StartMovement(Vector2 direction)
    {
        Movement?.StartMovement(direction);
    }

    private void StopMovement()
    {
        Movement?.StopMovement();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _input.PlayerMovementStarted += StartMovement;
        _input.PlayerMovementStoped += StopMovement;

        if (ResourceManager.Instance != null)
        {
            ResourceManager.Instance.PlayerHealed += HealthComponent.Heal;
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _input.PlayerMovementStarted -= StartMovement;
        _input.PlayerMovementStoped -= StopMovement;

        if (ResourceManager.Instance != null)
        {
            ResourceManager.Instance.PlayerHealed -= HealthComponent.Heal;
        }
    }
}
