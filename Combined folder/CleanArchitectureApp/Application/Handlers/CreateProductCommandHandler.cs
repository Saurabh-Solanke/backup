using Application.Commands;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class CreateProductCommandHandler
    {
        private readonly IProductRepository _repository;

        public CreateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateProductCommand command)
        {
            if (string.IsNullOrEmpty(command.Name))
            {
                throw new Exception("Product name is required.");
            }

            if (command.Price <= 0)
            {
                throw new Exception("Price must be greater than zero.");
            }

            var product = new Product
            {
                Name = command.Name,
                Description = command.Description,
                Price = command.Price,
                StockQuantity = command.StockQuantity
            };

            await _repository.AddAsync(product);
        }
    }
}

