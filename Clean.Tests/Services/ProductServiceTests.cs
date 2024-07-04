using Moq;
using AutoMapper;
using Clean.Core.Entities;
using Clean.WebApi.Mappings;
using Clean.Core.Interface;

namespace Clean.Tests.Services
{
    public class ProductServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly List<Product> _products;

        public ProductServiceTests()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            _mapper = mappingConfig.CreateMapper();

            _mockRepo = new Mock<IProductRepository>();
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Product1", Price = 100 },
                new Product { Id = 2, Name = "Product2", Price = 200 }
            };
        }

        [Fact]
        public async Task GetAllProducts_ShouldReturnProducts()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetAllProducts()).ReturnsAsync(_products);

            // Act
            var result = await _mockRepo.Object.GetAllProducts();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetProductById_ShouldReturnProduct()
        {
            var productId = 1;
            _mockRepo.Setup(repo => repo.GetProductById(productId)).ReturnsAsync(_products[0]);
            var result = await _mockRepo.Object.GetProductById(productId);
            Assert.NotNull(result);
            Assert.Equal(productId, result.Id);
        }
    }
}
