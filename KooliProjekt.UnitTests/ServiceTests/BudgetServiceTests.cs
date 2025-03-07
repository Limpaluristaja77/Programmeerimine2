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
    public class BudgetServiceTests : ServiceTestBase
    {
        [Fact]
        public async Task Save_should_add_new_list()
        {
            // Arrange
            var service = new BudgetService(DbContext);
            var budget = new Budget { ClientId = 1,  BuildingsId = 1, ServicesId = 1 };

            // Act
            await service.Save(budget);

            // Assert
            var count = DbContext.Budgets.Count();
            var result = DbContext.Budgets.FirstOrDefault();
            Assert.Equal(1, count);
            Assert.Equal(budget.ClientId, result.ClientId);
        }

        [Fact]
        public async Task Save_should_update_existing_budget()
        {
            var service = new BudgetService(DbContext);
            var budget = new Budget { ClientId = 1, BuildingsId = 1, ServicesId = 1 };
            DbContext.Budgets.Add(budget);
            await DbContext.SaveChangesAsync();

            budget.ClientId = 1;
            await service.Save(budget);

            var newBudget = await DbContext.Budgets.FindAsync(budget.Id);
            Assert.NotNull(newBudget);
            Assert.Equal(1, newBudget.ClientId);
        }

        [Fact]
        public async Task Get_should_return_correct_budget()
        {
            var service = new BudgetService(DbContext);
            var budget = new Budget { ClientId = 1, BuildingsId = 1, ServicesId = 1 };
            DbContext.Budgets.Add(budget);
            await DbContext.SaveChangesAsync();

            var getBudget = await service.Get(budget.Id);
            Assert.NotNull(getBudget);
            Assert.Equal(budget.Id, getBudget.Id);
        }

        [Fact]
        public async Task Delete_should_remove_given_list()
        {
            // Arrange
            var service = new BudgetService(DbContext);
            var budget = new Budget { ClientId = 1, BuildingsId = 1, ServicesId = 1 };
            DbContext.Budgets.Add(budget);
            DbContext.SaveChanges();

            // Act
            await service.Delete(1);

            // Assert
            var count = DbContext.Budgets.Count();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task List_should_return_paged_results()
        {
            // Arrange
            var service = new BudgetService(DbContext);

            // Seed data
            var client1 = new Client { Id = 1, Name = "Client 1", PhoneNumber = "111" };
            var client2 = new Client { Id = 2, Name = "Client 2", PhoneNumber = "111" };

            var building1 = new Buildings { Id = 1, Name = "Building 1" };
            var building2 = new Buildings { Id = 2, Name = "Building 2" };

            var service1 = new Service { Id = 1, Name = "Service 1", Provider = "Ching", Unit ="1" };
            var service2 = new Service { Id = 2, Name = "Service 2", Provider = "Ching", Unit = "1" };

            DbContext.Clients.Add(client1);
            DbContext.Clients.Add(client2);

            DbContext.Buildings.Add(building1);
            DbContext.Buildings.Add(building2);

            DbContext.Services.Add(service1);
            DbContext.Services.Add(service2);

            DbContext.Budgets.Add(new Budget { ClientId = 1, Client = client1, BuildingsId = 1, Buildings = building1, ServicesId = 1, Services = service1 });
            DbContext.Budgets.Add(new Budget { ClientId = 1, Client = client1, BuildingsId = 2, Buildings = building2, ServicesId = 2, Services = service2 });
            DbContext.Budgets.Add(new Budget { ClientId = 2, Client = client2, BuildingsId = 1, Buildings = building1, ServicesId = 1, Services = service1 });

            await DbContext.SaveChangesAsync();

            // Act
            var result = await service.List(1, 5, null); 

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.RowCount); 
            Assert.Equal(1, result.First().ClientId);  
        }

        [Fact]
        public async Task List_should_filter_by_keyword()
        {
            var service = new BudgetService(DbContext);
            DbContext.Budgets.AddRange(
                new Budget { ClientId = 1, BuildingsId = 1, ServicesId = 1 },
                new Budget { ClientId = 2, BuildingsId = 1, ServicesId = 1 },
                new Budget { ClientId = 3, BuildingsId = 1, ServicesId = 1 }
            );
            await DbContext.SaveChangesAsync();

            var search = new BudgetSearch { Keyword = "1" };
            var result = await service.List(1, 10, search);

            Assert.NotNull(result);
            Assert.All(result, budget => Assert.Contains("1", budget.ClientId.ToString()));
        }
    }
}