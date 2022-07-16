using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Factories
{
    public interface IDataStoreFactory
    {
        IDataStore Get(DataStoreType dataStoreType);
    }
}
