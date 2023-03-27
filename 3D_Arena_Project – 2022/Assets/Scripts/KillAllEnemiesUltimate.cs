using Test_Project.Abstractions;

namespace Test_Project
{
    public class KillAllEnemiesUltimate : IUltimate
    {
        private IObjectContainer<IDieable> _enemiesContainer;
        private IUltimateUser _ultimateUser;
        public float NeedEnergy => 100;
        public int KilledEnemies { get; private set; } = 0;
        public KillAllEnemiesUltimate(IObjectContainer<IDieable> enemies, IUltimateUser ultimateUser)
        {
            _enemiesContainer = enemies;
            _ultimateUser = ultimateUser;
        }
        
        public void UseUltimate()
        {
            if (NeedEnergy <= _ultimateUser.Energy)
            {
                
                _ultimateUser.Exhaust();
                foreach (var enemy in _enemiesContainer.GetAllObject())
                {
                    if (enemy.Alive)
                    {
                        enemy.TakeDamage(float.MaxValue);
                        KilledEnemies++;
                    }
                }
            }
        }

       
    }
}