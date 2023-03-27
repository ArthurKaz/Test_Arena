using System;
using Test_Project;
using Test_Project.Abstractions;
using Test_Project.Entities;
using TMPro;
using UnityEngine;
namespace Test_Project
{
    public class EndGameHandle : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI killedEnemy;
        [SerializeField] private TextMeshProUGUI recordKilledEnemy;
        [SerializeField] private GameObject panel;

        private Player _player;
        private IUltimate _ultimate;
        private DataProvider _dataProvider;
        private RuntimeController _runtimeController;
       

        public void Init(Player player,IUltimate ultimate, RuntimeController runtimeController)
        {
            _player = player;
            _dataProvider = new DataProvider(_player, ultimate);
            _runtimeController = runtimeController;
            _ultimate = ultimate;
            panel.SetActive(false);
            
        }

        public void EndGame()
        {
            Cursor.lockState = CursorLockMode.None;
            _runtimeController.LockInput();
            _runtimeController.LockSpawner();
            _runtimeController.HideAllEnemies();
            ShowEndMenu();
            SaveData();
        }

        

        private void ShowEndMenu()
        {
            panel.SetActive(true);
            int killed = _player.KilledEnemies + _ultimate.KilledEnemies;
            recordKilledEnemy.text = _dataProvider.GetKilledEnemiesRecord().ToString();
            killedEnemy.text = killed.ToString();
        }

        private void SaveData()
        {
            _dataProvider.SaveKilledEnemiesRecord();
        }
        
    }
}