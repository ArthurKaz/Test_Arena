using System;

namespace Test_Project.Abstractions
{
    public interface IProgressBarValue
    {
        public event Action ProgressChanged;
        public float Progress { get; }
    }
}