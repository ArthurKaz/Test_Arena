using Test_Project.Abstractions;
using UnityEngine;

namespace Test_Project
{
    public class PlayerPrefsDataSaver : IDataSaver<int>
    {
        private readonly string _key;

        public PlayerPrefsDataSaver(string key)
        {
            _key = key;
        }
        public void Save(int killedAmount)
        {
            PlayerPrefs.SetInt(_key,killedAmount);
        }
    }
}