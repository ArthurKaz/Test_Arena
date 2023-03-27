namespace Test_Project.Abstractions
{
    public interface IDataLoader<T>
    {
        public T Load();
    }
}