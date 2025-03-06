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
    public class BuildingServiceTests : ServiceTestBase
    {
        [Fact]
        public async Task Save_should_add_new_list()
        {
            // Arrange
            var service = new BuildingsService(DbContext);
            var building = new Buildings { Name = "SweatyHut", PanelId = 1, MaterialId = 1};

            // Act
            await service.Save(building);

            // Assert
            var count = DbContext.Buildings.Count();
            var result = DbContext.Buildings.FirstOrDefault();
            Assert.Equal(1, count);
            Assert.Equal(building.Name, result.Name);
        }

        [Fact]
        public async Task Save_should_update_existing_building()
        {
            var service = new BuildingsService(DbContext);
            var building = new Buildings { Name = "SweatyHut", PanelId = 1, MaterialId = 1 };
            DbContext.Buildings.Add(building);
            await DbContext.SaveChangesAsync();

            building.Name = "SweatyHut2";
            await service.Save(building);

            var newBuilding = await DbContext.Buildings.FindAsync(building.Id);
            Assert.NotNull(newBuilding);
            Assert.Equal("SweatyHut2", newBuilding.Name);
        }

        [Fact]
        public async Task Get_should_return_correct_building()
        {
            var service = new BuildingsService(DbContext);
            var building = new Buildings { Name = "Hut", PanelId = 1, MaterialId = 1};
            DbContext.Buildings.Add(building);
            await DbContext.SaveChangesAsync();

            var getBuilding = await service.Get(building.Id);
            Assert.NotNull(getBuilding);
            Assert.Equal(building.Id, getBuilding.Id);
        }

        [Fact]
        public async Task Delete_should_remove_given_list()
        {
            // Arrange
            var service = new BuildingsService(DbContext);
            var building = new Buildings { Name = "Hut", PanelId = 1, MaterialId = 1 };
            DbContext.Buildings.Add(building);
            DbContext.SaveChanges();

            // Act
            await service.Delete(1);

            // Assert
            var count = DbContext.Buildings.Count();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task List_should_return_paged_results()
        {
            var service = new BuildingsService(DbContext);
            for (int i = 0; i < 10; i++)
            {
                DbContext.Buildings.Add(new Buildings { Name = "Hut", PanelId = 1, MaterialId = 1 });
            }
            await DbContext.SaveChangesAsync();

            var result = await service.List(1, 5);

            Assert.NotNull(result);
            Assert.Equal(10, result.RowCount);
            Assert.Equal("Hut", result.First().Name);
        }

        [Fact]
        public async Task List_should_filter_by_keyword()
        {
            var service = new BuildingsService(DbContext);
            DbContext.Buildings.AddRange(
                new Buildings { Name = "Hut", PanelId = 1, MaterialId = 1 },
                new Buildings { Name = "Hut2", PanelId = 1, MaterialId = 1 },
                new Buildings { Name = "Hut3", PanelId = 1, MaterialId = 1 }
            );
            await DbContext.SaveChangesAsync();

            var search = new BuildingSearch { Keyword = "Hut" };
            var result = await service.List(1, 10, search);

            Assert.NotNull(result);
            Assert.All(result, building => Assert.Contains("Hut", building.Name));
        }
    }
}