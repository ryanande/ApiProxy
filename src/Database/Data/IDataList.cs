using System.Collections.Generic;

namespace Database.Data
{
    public interface IDataList<out T>
    {
        IEnumerable<T> GetList();
    }
}
