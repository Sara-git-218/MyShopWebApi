using Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class TestCateroryRepository
    {
        [Fact]
        public async Task GetCategories_ReturnsAllCategories()
        {
            var catgories = new List<Catgory>
            {
                new Catgory { CatgoryName = "c1" },
                new Catgory { CatgoryName = "c2" }
            };

            var mockContext = new Mock<_326059268_ShopApiContext>();
            mockContext.Setup(c => c.Catgories).ReturnsDbSet(catgories);

            var repo = new CategoryRepository(mockContext.Object);
            var result = await repo.GetCatgories();

            Assert.Equal(2, result.Count);
        }
        [Fact]
        public async Task GetCategories_EmptyDb_ReturnsEmptyList()
        {
            var mockContext = new Mock<_326059268_ShopApiContext>();
            mockContext.Setup(c => c.Catgories).ReturnsDbSet(new List<Catgory>());

            var repo = new CategoryRepository(mockContext.Object);
            var result = await repo.GetCatgories();

            Assert.Empty(result);
        }

    }
}
