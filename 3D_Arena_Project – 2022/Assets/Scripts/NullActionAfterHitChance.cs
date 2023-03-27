using Test_Project.Abstractions;

namespace Test_Project
{
    public class NullActionAfterHitChance : IActionAfterHitChance
    {
        public float Chance => 0;
        public bool DidAfterHit()
        {
            return false;
        }
    }
}