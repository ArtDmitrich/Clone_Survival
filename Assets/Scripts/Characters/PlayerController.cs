using UnityEngine;
using Zenject;

public class PlayerController : MovableCharacter
{
    public AttackingSystem AttackingSystem { get { return _attackingSystem; } }
    [SerializeField] private AttackingSystem _attackingSystem;

    private InputController _input;

    [Inject]
    private void Construct(InputController inputController)
    {
        _input = inputController;
    }

    private void StartMovement(Vector2 direction)
    {
        Movement?.StartMovement(direction * CharacterStats.MovementSpeed);
    }

    private void StopMovement()
    {
        Movement?.StopMovement();
    }

    private void SetAdditionalDamageToAttackingSystem(float additionalDamage)
    {
        if (_attackingSystem != null)
        {
            _attackingSystem.AdditionalDamage = additionalDamage;
        }
    }

    private void Start()
    {
        SetAdditionalDamageToAttackingSystem(CharacterStats.Damage);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _input.PlayerMovementStarted += StartMovement;
        _input.PlayerMovementStoped += StopMovement;

        CharacterStats.DamageChanged += SetAdditionalDamageToAttackingSystem;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _input.PlayerMovementStarted -= StartMovement;
        _input.PlayerMovementStoped -= StopMovement;

        CharacterStats.DamageChanged -= SetAdditionalDamageToAttackingSystem;
    }
}
