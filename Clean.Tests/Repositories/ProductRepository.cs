using Xunit;
using Microsoft.EntityFrameworkCore;
using Clean.Infrastructure.Data;
using Clean.Core.Entities;
using Clean.Infrastructure.Repository;

namespace Clean.Tests.IntegrationTests
{
    public class ProductRepositoryIntegrationTests
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductRepository _productRepository;

        public ProductRepositoryIntegrationTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(options);
            _productRepository = new ProductRepository(_context);
            _context.Products.AddRange(new List<Product>
            {
                new() { Id = 1, Name = "Product1", Price = 100 },
                new() { Id = 2, Name = "Product2", Price = 200 },
                new() { Id = 3, Name = "Product3", Price = 300 }
            });
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllProducts_ShouldReturnAllButOneProduct()
        {
            var result = await _productRepository.GetAllProducts();
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.DoesNotContain(result, p => p.Id == 3);
        }
    }
}
