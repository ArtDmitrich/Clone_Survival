using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingWeapon : MonoBehaviour
{
    [SerializeField] private float _rotatingSpeed;

    private void Update()
    {
        transform.Rotate(0.0f, 0.0f, _rotatingSpeed * Time.deltaTime);
    }
}
