using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Test_Project.Abstractions;

namespace Test_Project.Entities
{
    public class EnemySpawner : MonoBehaviour, IObjectContainer<IDieable>
    {
        private const float BlueY = 0.3f;
        private const float RedY = 0.2f;
        [SerializeField] private float spawnDelay = 5;
        [SerializeField] private float minSpawnDelay = 2;
        [SerializeField] private float stepAfterWave = 1;
        [SerializeField] private float maxBlueEnemy;
        
        private List<IDieable> _enemies = new();
        private ISpawnable<HomingBullet> _bulletSpawn;
        private IRandomPosition _position;
        private SimpleObjectPull<Enemy> _blueEnemy;
        private SimpleObjectPull<Enemy> _redEnemy;
        private ITargetable _targetable;

        private int _blueEnemyAmount = 1;
        private int RedEnemyAmount => _blueEnemyAmount* 4 ;
        
        public bool locked;
        public void Init(SimpleObjectPull<Enemy> blueEnemy, SimpleObjectPull<Enemy> redEnemy)
        {
            _blueEnemy = blueEnemy;
            _redEnemy = redEnemy;
            _redEnemy.NewObjectJustCreated += AddSpawnedRedEnemy;
            _blueEnemy.NewObjectJustCreated += AddSpawnedBlueEnemy;
            
        }
        public void Init(ISpawnable<HomingBullet> bulletSpawn, IRandomPosition position,ITargetable targetable)
        {
            _bulletSpawn = bulletSpawn;
            _position = position;
            _targetable = targetable;
        }

        private void AddSpawnedBlueEnemy(Enemy enemy)=> AddSpawnedEnemy(enemy, _blueEnemy);

        private void AddSpawnedRedEnemy(Enemy enemy) => AddSpawnedEnemy(enemy, _redEnemy);
        private void AddSpawnedEnemy(Enemy enemy,SimpleObjectPull<Enemy> enemyPull)
        {
            enemy.Init(_targetable);

            enemy.Died += () =>
            {
                enemyPull.AddObjectToPull(enemy);
            };
            _enemies.Add(enemy);
        }

        public void StarSpawnEnemies()
        {
            _blueEnemy.SpawnBeginObjects();
            _redEnemy.SpawnBeginObjects();
            StartCoroutine(Spawning());
        }

        private IEnumerator Spawning()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnDelay);
                if (locked == false)
                {
                    SpawnEnemyWave();
                    UpdateWaveOptions();
                    
                }
            }
        }

        private void SpawnEnemyWave()
        {
            for (int i = 0; i < _blueEnemyAmount; i++)
            {
               SpawnBlueEnemy();
            }

            for (int i = 0; i < RedEnemyAmount; i++)
            {
                SpawnRedEnemy();
            }
           
        }

        private void SpawnBlueEnemy() => SpawnEnemy(_blueEnemy,BlueY);

        private void SpawnRedEnemy() => SpawnEnemy(_redEnemy,RedY);

        private void SpawnEnemy(SimpleObjectPull<Enemy> enemyPull, float y)
        {
            var enemy = enemyPull.GetObject();
            var position = _position.GetPosition;
            position.y = y;
            enemy.transform.position = position;
            enemy.Init(_bulletSpawn);
            
            enemy.Activate();
        }

        private void UpdateWaveOptions()
        {
            if (spawnDelay <= minSpawnDelay)
            {
                _blueEnemyAmount = _blueEnemyAmount + 1 > maxBlueEnemy ? _blueEnemyAmount : _blueEnemyAmount + 1;
            }
            else
            {
                spawnDelay = spawnDelay - stepAfterWave < minSpawnDelay ? minSpawnDelay : spawnDelay - stepAfterWave;
            }
        }
        public IEnumerable<IDieable> GetAllObject()
        {
            return _enemies;
        }
    }
}