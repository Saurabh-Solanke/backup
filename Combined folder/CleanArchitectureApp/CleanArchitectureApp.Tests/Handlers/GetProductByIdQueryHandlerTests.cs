using Application.Handlers;
using Application.Queries;
using Core.Entities;
using Core.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureApp.Tests.Handlers
{
    public class GetProductByIdQueryHandlerTests
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly GetProductByIdQueryHandler _handler;

        public GetProductByIdQueryHandlerTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            _handler = new GetProductByIdQueryHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_Should_Return_Product()
        {
            // Arrange
            var query = new GetProductByIdQuery { Id = 1 };
            var product = new Product { Id = 1, Name = "Test Product" };

            _mockRepository.Setup(r => r.GetByIdAsync(query.Id)).ReturnsAsync(product);

            // Act
            var result = await _handler.Handle(query);

            // Assert
            Assert.Equal(product, result);
        }
    }
}
