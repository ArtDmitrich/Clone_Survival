using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicalMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float _movementSpeed;

    private Rigidbody2D RB { get { return _rb = _rb ?? GetComponent<Rigidbody2D>(); } }
    private Rigidbody2D _rb;

    public void StartMovement(Vector2 direction)
    {
        RB.velocity = direction * _movementSpeed;
    }

    public void StopMovement()
    {
        RB.velocity = Vector2.zero;
    }
}
