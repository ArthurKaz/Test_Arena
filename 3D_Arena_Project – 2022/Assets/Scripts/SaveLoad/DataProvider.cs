using Test_Project.Abstractions;
using Test_Project.Entities;

namespace Test_Project
{
    public class DataProvider
    {
        private const string Key = "EnemyKilledRecord";
        private readonly IDataLoader<int> _enemiesLoader;
        private readonly IDataSaver<int> _enemiesSaver;
        private Player _player;
        private IUltimate _ultimate;
        private int _killedEnemies;
        public DataProvider(Player player, IUltimate ultimate)
        {
            _player = player;
            _ultimate = ultimate;
            _enemiesLoader = new PlayerPrefsDataLoader(Key);
            _enemiesSaver = new PlayerPrefsDataSaver(Key);
            _killedEnemies = _enemiesLoader.Load();
        }

        public int GetKilledEnemiesRecord()
        {
            return _killedEnemies;
        }

        public void SaveKilledEnemiesRecord()
        {
            int newKilled = _player.KilledEnemies + _ultimate.KilledEnemies;

            if (newKilled > _killedEnemies)
            {
                _enemiesSaver.Save( newKilled);
            }
        }
        
    }
}