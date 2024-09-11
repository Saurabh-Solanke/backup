using Application.Queries;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class GetAllProductsQueryHandler
    {
        private readonly IProductRepository _repository;

        public GetAllProductsQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<Product>> Handle(GetAllProductsQuery query)
        {
            var products = await _repository.GetAllAsync();

            var pagedProducts = products
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToList();

            return new PagedResult<Product>
            {
                Items = pagedProducts,
                TotalItems = products.Count(),
                PageNumber = query.PageNumber,
                PageSize = query.PageSize
            };
        }
    }

    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalItems { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public int TotalPages => (int)System.Math.Ceiling(TotalItems / (double)PageSize);
    }
}
