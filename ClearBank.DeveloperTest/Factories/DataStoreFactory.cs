using System;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Factories
{
    public class DataStoreFactory : IDataStoreFactory
    {
        public IDataStore Get(DataStoreType dataStoreType)
        {
            switch (dataStoreType)
            {
                case DataStoreType.Account: return new AccountDataStore();
                case DataStoreType.BackupAccount: return new BackupAccountDataStore();
                default: throw new ArgumentException($"Unrecognised DataStoreType {dataStoreType}.");
            }
        }
    }
}
