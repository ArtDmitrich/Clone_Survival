using UnityEngine;
using Zenject;

public class PlayerController : MovableCharacter
{
    public AttackingSystem AttackingSystem { get { return _attackingSystem; } }
    [SerializeField] private AttackingSystem _attackingSystem;

    [SerializeField] private Animator _animator;

    private InputController _input;
    private ResourceManager _resourceManager;

    [Inject]
    private void Construct(InputController inputController, ResourceManager resourceManager)
    {
        _input = inputController;
        _resourceManager = resourceManager;
    }

    private void StartMovement(Vector2 direction)
    {
        Movement?.StartMovement(direction * CharacterStats.MovementSpeed);

        _animator.SetTrigger("StartMovement");
        _animator.SetFloat("DirectionX", direction.x);
        _animator.SetFloat("DirectionY", direction.y);
    }

    private void StopMovement()
    {
        Movement?.StopMovement();

        _animator.SetTrigger("StopMovement");
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

        _resourceManager.PlayerHealed += HealthComponent.Heal;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _input.PlayerMovementStarted -= StartMovement;
        _input.PlayerMovementStoped -= StopMovement;

        CharacterStats.DamageChanged -= SetAdditionalDamageToAttackingSystem;
        _resourceManager.PlayerHealed -= HealthComponent.Heal;
    }
}
