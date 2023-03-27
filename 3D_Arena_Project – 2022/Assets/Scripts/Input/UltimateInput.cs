using Test_Project.Abstractions;
using UnityEngine;

namespace Test_Project
{
    public abstract class UltimateInput : MonoBehaviour
    {
        private IUltimate _ultimate;

        public void Init(IUltimate ultimate)
        {
            _ultimate = ultimate;
        }

        protected void Ultimate()
        {
            _ultimate.UseUltimate();
        }
    }
}