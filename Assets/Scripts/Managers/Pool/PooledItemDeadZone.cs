using UnityEngine;

public class PooledItemDeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ReleasePooledItem(collision.gameObject);
    }

    private void ReleasePooledItem(GameObject item)
    {
        if (item.TryGetComponent<Character>(out var character))
        {
            EnemiesManager.Instance.RemoveEnemy(character);
        }

        if (item.TryGetComponent<PooledItem>(out var pooldeItem))
        {
            pooldeItem.Release();
        }
    }
}
