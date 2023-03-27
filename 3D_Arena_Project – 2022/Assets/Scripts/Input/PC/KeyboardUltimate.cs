using UnityEngine;

namespace Test_Project
{
    public class KeyboardUltimate : UltimateInput
    {
        [SerializeField] private KeyCode useUltimate;
        private void Update()
        {
            if (Input.GetKeyDown(useUltimate))
            {
                Ultimate();
            }
        }
    }
}