using Application.Commands;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class UpdateProductCommandHandler
    {
        private readonly IProductRepository _repository;

        public UpdateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateProductCommand command)
        {
            var product = await _repository.GetByIdAsync(command.Id);

            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            product.Name = command.Name;
            product.Description = command.Description;
            product.Price = command.Price;
            product.StockQuantity = command.StockQuantity;

            await _repository.UpdateAsync(product);
        }
    }
}
