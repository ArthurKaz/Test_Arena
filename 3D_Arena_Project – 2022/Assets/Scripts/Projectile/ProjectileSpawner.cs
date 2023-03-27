using Test_Project;
using UnityEngine;
using Test_Project.Abstractions;

public class ProjectileSpawner : MonoBehaviour, ISpawnable<SimpleBullet>, ISpawnable<HomingBullet>
{
    private ITargetable _target;
    private SimpleObjectPull<SimpleBullet> _simpleBulletPull;
    private SimpleObjectPull<HomingBullet> _homingBulletPull;
    private Vector3 _spawnPoint;

    public void Init(SimpleObjectPull<SimpleBullet> simpleObjectPull, SimpleObjectPull<HomingBullet> homingBulletPull,
        ITargetable targetable)
    {

        _simpleBulletPull = simpleObjectPull;
        _homingBulletPull = homingBulletPull;
        _target = targetable;
        _simpleBulletPull.NewObjectJustCreated += SimpleBulletCreatedHandle;
        _homingBulletPull.NewObjectJustCreated += HomingBulletCreatedHandle;
        _simpleBulletPull.SpawnBeginObjects();
        _homingBulletPull.SpawnBeginObjects();
    }

    

    SimpleBullet ISpawnable<SimpleBullet>.Spawn(Vector3 position)
    {
        var bullet = _simpleBulletPull.GetObject();

        bullet.transform.position = position;
        return bullet;
    }

    HomingBullet ISpawnable<HomingBullet>.Spawn(Vector3 position)
    {
        var bullet = _homingBulletPull.GetObject();
        bullet.Target = _target;
        bullet.transform.position = position;

        return bullet;
    }

    private void SimpleBulletCreatedHandle(SimpleBullet simpleBullet)
    {
        simpleBullet.Disappear += BackSimpleBulletToPull;
    }

    private void HomingBulletCreatedHandle(HomingBullet homingBullet)
    {
        homingBullet.Disappear += BackHomingBulletToPull;
        _target.Replaced += homingBullet.SetDirectionProvider;
    }

    private void BackSimpleBulletToPull(Projectile obj)
    {
        _simpleBulletPull.AddObjectToPull((SimpleBullet)obj);
    }

    private void BackHomingBulletToPull(Projectile obj)
    {
        _homingBulletPull.AddObjectToPull((HomingBullet)obj);
    }
}
