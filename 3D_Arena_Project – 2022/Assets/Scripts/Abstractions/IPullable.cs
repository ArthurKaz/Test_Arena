namespace Test_Project.Abstractions
{
    public interface IPullable
    {
        void Activate();
        void Deactivate();
        bool IsAvailable();
    }
}