namespace Test_Project.Abstractions
{
    public interface IActionAfterHitChance
    {
        public float Chance { get; }
       
        public bool DidAfterHit();

    }
}