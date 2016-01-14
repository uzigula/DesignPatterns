using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patterns.Api.Contracts
{
    public interface DataProvider<T>
    {
        List<T> GetAll();
        T Get(int id);
        void Save(T item);

        void Update(T employee);
    }
}
