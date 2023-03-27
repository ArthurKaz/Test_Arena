using Test_Project;
using Test_Project.Abstractions;
using Test_Project.Entities;

namespace Test_Project
{
    public class RuntimeController
    {
        public static bool LockedInput;
  
        
        private readonly EnemySpawner _enemySpawner;
        private readonly IObjectContainer<IDieable> _enemies;
        public RuntimeController(EnemySpawner enemySpawner)
        {
            _enemySpawner = enemySpawner;
            _enemies = _enemySpawner;
            LockedInput = false;
        }

        public void LockInput()
        {
            LockedInput = true;
        }

        public void LockSpawner()
        {
            _enemySpawner.locked = true;
        }

        public void HideAllEnemies()
        {
            foreach (var enemy in _enemies.GetAllObject())
            {
                if (enemy.Alive)
                {
                    enemy.TakeDamage(enemy.MaxHealth);
                }
            }
        }
    }
}