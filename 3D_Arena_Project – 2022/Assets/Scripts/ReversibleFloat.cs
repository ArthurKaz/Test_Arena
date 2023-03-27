using System;
using Test_Project.Abstractions;
using UnityEngine;

namespace Test_Project
{
    [Serializable]
    public class ReversibleFloat : IProgressBarValue
    {
        [SerializeField] private float maxValue;

        private float _value;
        private float _beginValue;

        public float Value
        {
            get => _value;
            set
            {
                _value = value >= maxValue ? maxValue: value;
                OnProgressChanged();
            }
        }

        public event Action ProgressChanged;
        public float Progress => _value / maxValue;
        public float Max => maxValue;

        public void Init(float beginValue)
        {
            _beginValue = beginValue;
            Reset();
        }
        protected virtual void OnProgressChanged()
        {
            ProgressChanged?.Invoke();
        }

        public void Reset()
        {
            Value = _beginValue;
        }
    }
}