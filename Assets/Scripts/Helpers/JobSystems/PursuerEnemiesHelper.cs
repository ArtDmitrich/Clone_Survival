using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;
using Zenject;

public class PursuerEnemiesHelper : MonoBehaviour
{
    public List<PursuerEnemy> Enemies;

    [SerializeField] private int _callsPerSec;

    private Transform _player;
    private float _timer;

    [Inject]
    private void Construct(PlayerController playerController)
    {
        _player = playerController.transform;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            _timer += (1 / _callsPerSec);
            EnemyMovement();
        }
    }
    private void EnemyMovement()
    {
        if (_player == null || Enemies.Count == 0)
        {
            return;
        }

        var sqrMinDistancesToTarget = new NativeArray<float>(Enemies.Count, Allocator.TempJob);
        var enemiesTransformAccess = new TransformAccessArray(Enemies.Count);

        for (int i = 0; i < Enemies.Count; i++)
        {
            sqrMinDistancesToTarget[i] = Enemies[i].SqrMinDistanceToTarget;
            enemiesTransformAccess.Add(Enemies[i].transform);
        }

        var directionsToTarget = new NativeArray<Vector3>(Enemies.Count, Allocator.TempJob);

        var job = new DirectionToMoveJob
        {
            Target = _player.position,
            SqrMinDistances = sqrMinDistancesToTarget,
            DirectionToTarget = directionsToTarget
        };


        var handle = job.Schedule(enemiesTransformAccess);
        handle.Complete();

        for (int i = 0; i < Enemies.Count; i++)
        {
            var directionToTarget = directionsToTarget[i];

            if (directionToTarget == Vector3.zero)
            {
                Enemies[i].Movement.StopMovement();
            }
            else
            {
                Enemies[i].Movement.StartMovement(directionToTarget * Enemies[i].CharacterStats.MovementSpeed);
            }
        }

        directionsToTarget.Dispose();
        enemiesTransformAccess.Dispose();
        sqrMinDistancesToTarget.Dispose();
    }
}

[BurstCompile]
public struct DirectionToMoveJob : IJobParallelForTransform
{
    public Vector3 Target;
    public NativeArray<float> SqrMinDistances;
    public NativeArray<Vector3> DirectionToTarget;

    public void Execute(int index, TransformAccess transform)
    {
        var vectorToTarget = Target - transform.position;
        var sqrDistanceToTarget = vectorToTarget.sqrMagnitude;

        if (sqrDistanceToTarget <= SqrMinDistances[index])
        {
            DirectionToTarget[index] = Vector3.zero;
        }
        else
        {
            DirectionToTarget[index] = vectorToTarget.normalized;
        }
    }
}
