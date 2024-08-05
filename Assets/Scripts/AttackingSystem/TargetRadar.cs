using UnityEngine;

public static class TargetRadar
{
    public static Transform FindNearestTarget(Vector3 position, float detectionRadius, LayerMask targetLayer)
    {
        var colliders = Physics2D.OverlapCircleAll(position, detectionRadius, targetLayer);

        float nearestSqrDistance = Mathf.Infinity;
        Collider2D nearestTarget = null;

        foreach (var col in colliders)
        {
            float sqrDistance = (col.transform.position - position).sqrMagnitude;

            if (sqrDistance < nearestSqrDistance)
            {
                nearestSqrDistance = sqrDistance;
                nearestTarget = col;
            }
        }

        return nearestTarget != null ? nearestTarget.transform : null;
    }
}