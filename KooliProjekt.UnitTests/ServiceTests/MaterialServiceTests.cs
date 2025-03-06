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
    public class MaterialServiceTests : ServiceTestBase
    {
        [Fact]
        public async Task Save_should_add_new_list()
        {
            // Arrange
            var service = new MaterialsService(DbContext);
            var material = new Material { Name = "Nails", Manufacturer = "Test", Unit = "1" };

            // Act
            await service.Save(material);

            // Assert
            var count = DbContext.Materials.Count();
            var result = DbContext.Materials.FirstOrDefault();
            Assert.Equal(1, count);
            Assert.Equal(material.Name, result.Name);
        }

        [Fact]
        public async Task Save_should_update_existing_material()
        {
            var service = new MaterialsService(DbContext);
            var material = new Material { Name = "Nails", Manufacturer = "Test", Unit = "1" };
            DbContext.Materials.Add(material);
            await DbContext.SaveChangesAsync();

            material.Manufacturer = "Test2";
            await service.Save(material);

            var newMaterial = await DbContext.Materials.FindAsync(material.Id);
            Assert.NotNull(newMaterial);
            Assert.Equal("Test2", newMaterial.Manufacturer);
        }

        [Fact]
        public async Task Get_should_return_correct_material()
        {
            var service = new MaterialsService(DbContext);
            var material = new Material { Name = "Nails", Manufacturer = "Test", Unit = "1" };
            DbContext.Materials.Add(material);
            await DbContext.SaveChangesAsync();

            var getMaterial = await service.Get(material.Id);
            Assert.NotNull(getMaterial);
            Assert.Equal(material.Id, getMaterial.Id);
        }

        [Fact]
        public async Task Delete_should_remove_given_list()
        {
            // Arrange
            var service = new MaterialsService(DbContext);
            var material = new Material { Name = "Nails", Manufacturer = "Test", Unit = "1" };
            DbContext.Materials.Add(material);
            DbContext.SaveChanges();

            // Act
            await service.Delete(1);

            // Assert
            var count = DbContext.Materials.Count();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task List_should_return_paged_results()
        {
            var service = new MaterialsService(DbContext);
            for (int i = 0; i < 10; i++)
            {
                DbContext.Materials.Add(new Material { Name = "Nails", Manufacturer = "Test", Unit = "1" + i });
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
            var service = new MaterialsService(DbContext);
            DbContext.Materials.AddRange(
                new Material { Name = "Nails", Manufacturer = "Test1", Unit = "1" },
                new Material { Name = "BOON", Manufacturer = "Test23", Unit = "1" },
                new Material { Name = "LOON", Manufacturer = "Test231", Unit = "1" }
            );
            await DbContext.SaveChangesAsync();

            var search = new MaterialSearch { Keyword = "Test1" };
            var result = await service.List(1, 10, search);

            Assert.NotNull(result);
            Assert.All(result, material => Assert.Contains("Test1", material.Manufacturer));
        }
    }
}