using System;
using UnityEngine;
using UnityEngine.UI;


namespace Test_Project.ButtonInput
{
    public class ButtonRestart : RestartInput
    {
        [SerializeField] private Button _button;

        private void Start()
        {
            _button.onClick.AddListener(RestartGame);
        }
    }
}