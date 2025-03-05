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
    public class PanelServiceTests : ServiceTestBase
    {
        [Fact]
        public async Task Save_should_add_new_list()
        {
            // Arrange
            var service = new PanelsService(DbContext);
            var panel = new Panel { Name = "Le panel", Manufacturer = "Test", Unit = "1" };

            // Act
            await service.Save(panel);

            // Assert
            var count = DbContext.Panels.Count();
            var result = DbContext.Panels.FirstOrDefault();
            Assert.Equal(1, count);
            Assert.Equal(panel.Name, result.Name);
        }

        [Fact]
        public async Task Save_should_update_existing_panel()
        {
            var service = new PanelsService(DbContext);
            var panel = new Panel { Name = "Le panel", Manufacturer = "Test", Unit = "1" };
            DbContext.Panels.Add(panel);
            await DbContext.SaveChangesAsync();

            panel.Manufacturer = "Test2";
            await service.Save(panel);

            var newPanel = await DbContext.Panels.FindAsync(panel.Id);
            Assert.NotNull(newPanel);
            Assert.Equal("Test2", newPanel.Manufacturer);
        }

        [Fact]
        public async Task Get_should_return_correct_panel()
        {
            var service = new PanelsService(DbContext);
            var panel = new Panel { Name = "Le panel", Manufacturer = "Test", Unit = "1" };
            DbContext.Panels.Add(panel);
            await DbContext.SaveChangesAsync();

            var getPanel = await service.Get(panel.Id);
            Assert.NotNull(getPanel);
            Assert.Equal(panel.Id, getPanel.Id);
        }

        [Fact]
        public async Task Delete_should_remove_given_list()
        {
            // Arrange
            var service = new PanelsService(DbContext);
            var panel = new Panel { Name = "Le panel", Manufacturer = "Test", Unit = "1" };
            DbContext.Panels.Add(panel);
            DbContext.SaveChanges();

            // Act
            await service.Delete(1);

            // Assert
            var count = DbContext.Panels.Count();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task List_should_return_paged_results()
        {
            var service = new PanelsService(DbContext);
            for (int i = 0; i < 10; i++)
            {
                DbContext.Panels.Add(new Panel { Name = "Le panel", Manufacturer = "Test", Unit = "1"  + i });
            }
            await DbContext.SaveChangesAsync();

            var result = await service.List(1, 5);

            Assert.NotNull(result);
            Assert.Equal(10, result.RowCount);
            Assert.Equal("Test", result.First().Manufacturer);
        }

        [Fact]
        public async Task List_should_filter_by_keyword()
        {
            var service = new PanelsService(DbContext);
            DbContext.Panels.AddRange(
                new Panel { Name = "Gooner", Manufacturer = "Test", Unit = "1" },
                new Panel { Name = "Le panel", Manufacturer = "Test", Unit = "1" },
                new Panel { Name = "Le panel", Manufacturer = "Test", Unit = "1" }
            );
            await DbContext.SaveChangesAsync();

            var search = new PanelSearch { Keyword = "Gooner" };
            var result = await service.List(1, 10, search);

            Assert.NotNull(result);
            Assert.All(result, panel => Assert.Contains("Gooner", panel.Manufacturer));
        }
    }
}