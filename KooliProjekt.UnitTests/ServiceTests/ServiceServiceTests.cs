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
    public class ServiceServiceTests : ServiceTestBase
    {
        [Fact]
        public async Task Save_should_add_new_list()
        {
            // Arrange
            var service = new ServicesService(DbContext);
            var services = new Service { Name = "Digging", Provider = "TEST", Unit = "1" };

            // Act
            await service.Save(services);

            // Assert
            var count = DbContext.Services.Count();
            var result = DbContext.Services.FirstOrDefault();
            Assert.Equal(1, count);
            Assert.Equal(services.Name, result.Name);
        }

        [Fact]
        public async Task Save_should_update_existing_service()
        {
            var service = new ServicesService(DbContext);
            var services = new Service { Name = "Test1", Provider = "TEST", Unit = "1" };
            DbContext.Services.Add(services);
            await DbContext.SaveChangesAsync();

            services.Name = "Test2";
            await service.Save(services);

            var newServices = await DbContext.Services.FindAsync(services.Id);
            Assert.NotNull(newServices);
            Assert.Equal("Test2", newServices.Name);
        }

        [Fact]
        public async Task Get_should_return_correct_service()
        {
            var service = new ServicesService(DbContext);
            var services = new Service { Name = "Digging", Provider = "TEST", Unit = "1" };
            DbContext.Services.Add(services);
            await DbContext.SaveChangesAsync();

            var getServices = await service.Get(services.Id);
            Assert.NotNull(getServices);
            Assert.Equal(services.Id, getServices.Id);
        }

        [Fact]
        public async Task Delete_should_remove_given_list()
        {
            // Arrange
            var service = new ServicesService(DbContext);
            var services = new Service { Name = "Digging", Provider = "TEST", Unit = "1" };
            DbContext.Services.Add(services);
            DbContext.SaveChanges();

            // Act
            await service.Delete(1);

            // Assert
            var count = DbContext.Services.Count();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task List_should_return_paged_results()
        {
            var service = new ServicesService(DbContext);
            for (int i = 0; i < 10; i++)
            {
                DbContext.Services.Add(new Service { Name = "Digging", Provider = "TEST", Unit = "1" + i });
            }
            await DbContext.SaveChangesAsync();

            var result = await service.List(1, 5);

            Assert.NotNull(result);
            Assert.Equal(10, result.RowCount);
            Assert.Equal("Digging", result.First().Name);
        }

        [Fact]
        public async Task List_should_filter_by_keyword()
        {
            var service = new ServicesService(DbContext);
            DbContext.Services.AddRange(
                new Service { Name = "Digging", Provider = "TEST", Unit = "1" },
                new Service { Name = "HUGGIN", Provider = "TEST", Unit = "1" },
                new Service { Name = "CHUGGIN", Provider = "TEST", Unit = "1" }
            );
            await DbContext.SaveChangesAsync();

            var search = new ServiceSearch { Keyword = "Digging" };
            var result = await service.List(1, 10, search);

            Assert.NotNull(result);
            Assert.All(result, services => Assert.Contains("Digging", services.Name));
        }
    }
}