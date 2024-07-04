using Xunit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using AutoMapper;

using Clean.Core.Entities;
using Clean.WebApi.Controllers;
using Clean.WebApi.DTOs;
using Clean.WebApi.Mappings;
using System.Collections.Generic;
using System.Threading.Tasks;
using Clean.Core.Interface;

namespace Clean.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly ProductsController _controller;
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly IMapper _mapper;

        public ProductsControllerTests()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            _mapper = mappingConfig.CreateMapper();

            _mockRepo = new Mock<IProductRepository>();
            _controller = new ProductsController(_mockRepo.Object, _mapper);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkResult_WithProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product1", Price = 100 },
                new Product { Id = 2, Name = "Product2", Price = 200 }
            };
            _mockRepo.Setup(repo => repo.GetAllProducts()).ReturnsAsync(products);
            var result = await _controller.GetAll();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<ProductDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenProductNotExists()
        {
            // Arrange
            var productId = 1;
            _mockRepo.Setup(repo => repo.GetProductById(productId)).ReturnsAsync((Product)null);

            // Act
            var result = await _controller.GetById(productId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        // More integration tests for other actions
    }
}
