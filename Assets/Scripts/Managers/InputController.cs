using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour
{
    public UnityAction<Vector2> PlayerMovementStarted;
    public UnityAction PlayerMovementStoped;

    private InputActions _input;

    private void Awake()
    {
        _input = new InputActions();
    }

    private void Movement_started(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        var direction = ctx.ReadValue<Vector2>();
        PlayerMovementStarted?.Invoke(direction);
    }

    private void Movement_canceled(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        PlayerMovementStoped?.Invoke();
    }

    private void OnEnable()
    {
        _input.Enable();

        _input.Player.Movement.performed += Movement_started;
        _input.Player.Movement.canceled += Movement_canceled;
    }

    private void OnDisable()
    {
        _input.Disable();

        _input.Player.Movement.started -= Movement_started;
        _input.Player.Movement.canceled -= Movement_canceled;
    }
}
