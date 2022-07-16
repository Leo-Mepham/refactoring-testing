using System.Configuration;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public DataStoreType GetDataStoreType()
        {
            var dataStoreType = ConfigurationManager.AppSettings["DataStoreType"];

            switch (dataStoreType)
            {
                case "Backup": return DataStoreType.BackupAccount;
                default : return DataStoreType.Account;
            }
        }
    }
}
