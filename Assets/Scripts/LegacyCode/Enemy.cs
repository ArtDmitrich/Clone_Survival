using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour, ITakingDamage
{
    public UnityAction<Enemy> EnemyDead;

    private HealthComponent HealthComponent { get {  return _healthComponent = _healthComponent ?? GetComponent<HealthComponent>(); } }
    private HealthComponent _healthComponent;
    public CharacterStats CharacterStats { get { return _characterStats = _characterStats ?? GetComponent<CharacterStats>(); } }
    private CharacterStats _characterStats;
    private PooledItem PooledItem { get { return _pooledItem = _pooledItem ?? GetComponent<PooledItem>(); } }
    private PooledItem _pooledItem;

    public void TakeDamage(float damage)
    {
        HealthComponent.GetDamage(damage);
    }

    protected void Death()
    {
        EnemyDead?.Invoke(this);
        PooledItem.Release();
    }

    protected void OnEnable()
    {
        HealthComponent.CharacterDied += Death;
        HealthComponent.Init(CharacterStats);
    }

    protected void OnDisable()
    {
        HealthComponent.CharacterDied -= Death;
    }
}
