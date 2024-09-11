using Application.Commands;
using Application.Handlers;
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
    public class CreateProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly CreateProductCommandHandler _handler;

        public CreateProductCommandHandlerTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            _handler = new CreateProductCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_Should_Add_Product()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = 10.99M,
                StockQuantity = 100
            };

            // Act
            await _handler.Handle(command);

            // Assert
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
        }
    }
}
