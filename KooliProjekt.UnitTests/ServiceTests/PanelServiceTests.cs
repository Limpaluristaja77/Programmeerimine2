using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KooliProjekt.Data;
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
            var panel = new Panel { Name = "Test", Manufacturer = "ChingPing", Unit = "10", UnitCost = 1};

            // Act
            await service.Save(panel);

            // Assert
            var count = DbContext.Panels.Count();
            var result = DbContext.Panels.FirstOrDefault();
            Assert.Equal(1, count);
            Assert.Equal(panel.Name, result.Name);
        }

        [Fact]
        public async Task Delete_should_remove_given_list()
        {
            // Arrange
            var service = new PanelsService(DbContext);
            var panel = new Panel { Name = "Test", Manufacturer = "ChingPing", Unit = "10", UnitCost = 1 };
            DbContext.Panels.Add(panel);
            DbContext.SaveChanges();

            // Act
            await service.Delete(1);

            // Assert
            var count = DbContext.Panels.Count();
            Assert.Equal(0, count);
        }


        [Fact]
        public async Task Save_Should_update_list()
        {
            //Arrange
            var service = new PanelsService(DbContext);
            var panel = new Panel { Name = "Test", Manufacturer = "ChingPing", Unit = "10", UnitCost = 1 };
            DbContext.Panels.Update(panel);
            DbContext.SaveChanges();


            //Act
            await service.Save(panel);

            //Assert
            var count = DbContext.Panels.Count();
            var result = DbContext.Panels.FirstOrDefault();
            Assert.Equal(1, count);
            Assert.Equal(panel.Name, result.Name);
        }


        [Fact]
        public async Task Get()
        {

            //Arrange
            var service = new PanelsService(DbContext);
            var panel = new Panel { Name = "Test", Manufacturer = "ChingPing", Unit = "10", UnitCost = 1 };
            DbContext.Panels.Add(panel);
            DbContext.SaveChanges();

            //Act

            await service.Get(1);

            //Assert

            var count = DbContext.Panels.Count();
            Assert.Equal(1, count);
        }


    }
}
