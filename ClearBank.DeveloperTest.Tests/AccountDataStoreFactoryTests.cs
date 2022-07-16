using System;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Factories;
using ClearBank.DeveloperTest.Types;
using FluentAssertions;
using Xunit;

namespace ClearBank.DeveloperTest.Tests
{
    public class AccountDataStoreFactoryTests
    {
        [Fact]
        public void AccountDataStoreFactoryGetsBackupAccountDataStore()
        {
            // Arrange
            var accountDataStoreFactory = new AccountDataStoreFactory();

            // Act
            var accountDataStore = accountDataStoreFactory.Get(DataStoreType.BackupAccount);

            // Assert
            accountDataStore.Should().BeOfType<BackupAccountDataStore>();
        }

        [Fact]
        public void AccountDataStoreFactoryGetsAccountDataStore()
        {
            // Arrange
            var accountDataStoreFactory = new AccountDataStoreFactory();

            // Act
            var accountDataStore = accountDataStoreFactory.Get(DataStoreType.Account);

            // Assert
            accountDataStore.Should().BeOfType<AccountDataStore>();
        }

        [Fact]
        public void AccountDataStoreFactoryThrowsOnNotSetDataStoreType()
        {
            // Arrange
            var accountDataStoreFactory = new AccountDataStoreFactory();

            // Act
            Action action = () => accountDataStoreFactory.Get(DataStoreType.NotSet);

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage("Unrecognised DataStoreType NotSet.");
        }
    }
}
