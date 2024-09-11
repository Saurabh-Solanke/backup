using Application.Commands;
using Application.Handlers;
using Application.Queries;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly CreateProductCommandHandler _createHandler;
        private readonly GetProductByIdQueryHandler _getByIdHandler;
        private readonly UpdateProductCommandHandler _updateHandler;
        private readonly DeleteProductCommandHandler _deleteHandler;
        private readonly GetAllProductsQueryHandler _getAllHandler;

        public ProductService(
            CreateProductCommandHandler createHandler,
            GetProductByIdQueryHandler getByIdHandler,
            UpdateProductCommandHandler updateHandler,
            DeleteProductCommandHandler deleteHandler,
            GetAllProductsQueryHandler getAllHandler)
        {
            _createHandler = createHandler;
            _getByIdHandler = getByIdHandler;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
            _getAllHandler = getAllHandler;
        }

        public async Task CreateProductAsync(CreateProductCommand command)
        {
            await _createHandler.Handle(command);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _getByIdHandler.Handle(new GetProductByIdQuery { Id = id });
        }

        public async Task UpdateProductAsync(UpdateProductCommand command)
        {
            await _updateHandler.Handle(command);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _deleteHandler.Handle(new DeleteProductCommand { Id = id });
        }

        public async Task<PagedResult<Product>> GetAllProductsAsync(int pageNumber, int pageSize)
        {
            return await _getAllHandler.Handle(new GetAllProductsQuery { PageNumber = pageNumber, PageSize = pageSize });
        }
    }
}
