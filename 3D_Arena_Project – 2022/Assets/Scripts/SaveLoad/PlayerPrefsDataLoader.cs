using Test_Project.Abstractions;
using UnityEngine;

namespace Test_Project
{
    public class PlayerPrefsDataLoader: IDataLoader<int>
    {
        private readonly string _key;

        public PlayerPrefsDataLoader(string key)
        {
            _key = key;
        }

        public int Load()
        {
            if (PlayerPrefs.HasKey(_key) == false)
            {
                return 0;
            }
           return PlayerPrefs.GetInt(_key);
        }
    }
}