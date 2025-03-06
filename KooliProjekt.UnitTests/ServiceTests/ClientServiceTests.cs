using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.Search;
using KooliProjekt.Services;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class ClientServiceTests : ServiceTestBase
    {
        [Fact]
        public async Task Save_should_add_new_list()
        {
            // Arrange
            var service = new ClientService(DbContext);
            var client = new Client { Name = "Test", PhoneNumber = "69696969"};

            // Act
            await service.Save(client);

            // Assert
            var count = DbContext.Clients.Count();
            var result = DbContext.Clients.FirstOrDefault();
            Assert.Equal(1, count);
            Assert.Equal(client.Name, result.Name);
        }

        [Fact]
        public async Task Save_should_update_existing_client()
        {
            var service = new ClientService(DbContext);
            var client = new Client { Name = "Test", PhoneNumber = "69696969" };
            DbContext.Clients.Add(client);
            await DbContext.SaveChangesAsync();

            client.Name = "Test2";
            await service.Save(client);

            var newClient = await DbContext.Clients.FindAsync(client.Id);
            Assert.NotNull(newClient);
            Assert.Equal("Test2", newClient.Name);
        }

        [Fact]
        public async Task Get_should_return_correct_client()
        {
            var service = new ClientService(DbContext);
            var client = new Client { Name = "Test", PhoneNumber = "69696969" };
            DbContext.Clients.Add(client);
            await DbContext.SaveChangesAsync();

            var getClient = await service.Get(client.Id);
            Assert.NotNull(getClient);
            Assert.Equal(client.Id, getClient.Id);
        }

        [Fact]
        public async Task Delete_should_remove_given_list()
        {
            // Arrange
            var service = new ClientService(DbContext);
            var client = new Client {Name = "Test", PhoneNumber = "69696969" };
            DbContext.Clients.Add(client);
            DbContext.SaveChanges();

            // Act
            await service.Delete(1);

            // Assert
            var count = DbContext.Clients.Count();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task List_should_return_paged_results()
        {
            var service = new ClientService(DbContext);
            for (int i = 0; i < 10; i++)
            {
                DbContext.Clients.Add(new Client { Name = "Test", PhoneNumber = "69696969" });
            }
            await DbContext.SaveChangesAsync();

            var result = await service.List(1, 5);

            Assert.NotNull(result);
            Assert.Equal(10, result.RowCount);
            Assert.Equal("Test", result.First().Name);
        }

        [Fact]
        public async Task List_should_filter_by_keyword()
        {
            var service = new ClientService(DbContext);
            DbContext.Clients.AddRange(
                new Client { Name = "Test", PhoneNumber = "69696969" },
                new Client { Name = "Test23", PhoneNumber = "69696969" },
                new Client { Name = "Test2323", PhoneNumber = "69696969" }
            );
            await DbContext.SaveChangesAsync();

            var search = new ClientSearch { Keyword = "Test" };
            var result = await service.List(1, 10, search);

            Assert.NotNull(result);
            Assert.All(result, client => Assert.Contains("Test", client.Name));
        }
    }
}