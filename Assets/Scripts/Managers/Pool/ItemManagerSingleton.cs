using UnityEngine;

public class ItemManagerSingleton<T> : Singleton<T> where T : MonoBehaviour
{
    [SerializeField] protected SpawnerSettings _spawnerSettings;

    [SerializeField] protected bool _poolPrewarming = true;
    [SerializeField] protected int _startPoolSize = 5;
    protected PoolManager _poolManager;

    protected void Awake()
    {
        _poolManager = gameObject.AddComponent<PoolManager>();
        _poolManager.Init(_spawnerSettings, _poolPrewarming, _startPoolSize);
    }
}
