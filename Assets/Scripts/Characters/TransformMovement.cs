using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float _movementSpeed;

    private bool _isMoving;
    private Vector2 _direction;

    public void StartMovement(Vector2 direction)
    {
        _isMoving = true;
        _direction = direction;
    }

    public void StopMovement()
    {
        _isMoving = false;
    }

    private void Move()
    {
        transform.Translate(_direction * _movementSpeed * Time.deltaTime);
    }

    private void Update()
    {
        if (_isMoving)
        {
            Move();
        }
    }

    private void OnDisable()
    {
        StopMovement();
    }
}
