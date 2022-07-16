using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Factories
{
    public interface IAccountDataStoreFactory
    {
        IAccountDataStore Get(DataStoreType dataStoreType);
    }
}
