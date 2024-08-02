public class BulletManager : ItemManagerSingleton<BulletManager>
{
    public Bullet GetBullet(string bulletName)
    {
        return _poolManager.GetPooledItem<Bullet>(bulletName);
    }
}
