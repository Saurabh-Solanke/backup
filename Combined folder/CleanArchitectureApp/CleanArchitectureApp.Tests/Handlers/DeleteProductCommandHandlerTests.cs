using Application.Commands;
using Application.Handlers;
using Core.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureApp.Tests.Handlers
{
    public class DeleteProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly DeleteProductCommandHandler _handler;

        public DeleteProductCommandHandlerTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            _handler = new DeleteProductCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_Should_Delete_Product()
        {
            // Arrange
            var command = new DeleteProductCommand { Id = 1 };

            // Act
            await _handler.Handle(command);

            // Assert
            _mockRepository.Verify(r => r.DeleteAsync(command.Id), Times.Once);
        }
    }
}
