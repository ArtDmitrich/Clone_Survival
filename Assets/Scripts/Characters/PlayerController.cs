using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using Zenject;

public class PlayerController : MovableCharacter
{
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

    protected override void OnEnable()
    {
        base.OnEnable();
        _input.PlayerMovementStarted += StartMovement;
        _input.PlayerMovementStoped += StopMovement;        
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _input.PlayerMovementStarted -= StartMovement;
        _input.PlayerMovementStoped -= StopMovement;
    }
}
