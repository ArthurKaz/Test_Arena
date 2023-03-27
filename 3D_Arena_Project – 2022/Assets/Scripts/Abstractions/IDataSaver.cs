namespace Test_Project.Abstractions
{
    public interface IDataSaver<T>
    {
        public void Save(T killedAmount);
    }
}