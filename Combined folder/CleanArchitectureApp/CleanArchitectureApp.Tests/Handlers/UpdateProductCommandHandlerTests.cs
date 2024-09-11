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
    public class UpdateProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly UpdateProductCommandHandler _handler;

        public UpdateProductCommandHandlerTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            _handler = new UpdateProductCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_Should_Update_Product()
        {
            // Arrange
            var command = new UpdateProductCommand
            {
                Id = 1,
                Name = "Updated Product",
                Description = "Updated Description",
                Price = 15.99M,
                StockQuantity = 50
            };
            var product = new Product { Id = 1, Name = "Old Product" };

            _mockRepository.Setup(r => r.GetByIdAsync(command.Id)).ReturnsAsync(product);

            // Act
            await _handler.Handle(command);

            // Assert
            _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Product>()), Times.Once);
        }
    }
}
