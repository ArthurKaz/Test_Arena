using System.Collections.Generic;

namespace Test_Project.Abstractions
{
    public interface IObjectContainer<T>
    {
        public IEnumerable<T> GetAllObject();
    }
}