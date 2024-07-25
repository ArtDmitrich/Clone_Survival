using UnityEngine;

public class EnemyDebuger : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private MovableEnemy _enemy;

    private void Awake()
    {
        _enemy.Target = _target;
    }
}
