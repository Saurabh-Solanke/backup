using Application.Commands;
using Application.Handlers;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IProductService
    {
        Task CreateProductAsync(CreateProductCommand command);
        Task<Product> GetProductByIdAsync(int id);
        Task UpdateProductAsync(UpdateProductCommand command);
        Task DeleteProductAsync(int id);

        Task<PagedResult<Product>> GetAllProductsAsync(int pageNumber, int pageSize);

    }
}
