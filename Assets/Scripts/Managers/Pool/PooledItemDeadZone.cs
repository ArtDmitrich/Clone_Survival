using UnityEngine;
using Zenject;

public class PooledItemDeadZone : MonoBehaviour
{
    [Inject] private EnemiesManager _enemiesManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ReleasePooledItem(collision.gameObject);
    }

    private void ReleasePooledItem(GameObject item)
    {
        if (item.TryGetComponent<Character>(out var character))
        {
            _enemiesManager.RemoveEnemy(character);
        }

        if (item.TryGetComponent<PooledItem>(out var pooldeItem))
        {
            pooldeItem.Release();
        }
    }
}
