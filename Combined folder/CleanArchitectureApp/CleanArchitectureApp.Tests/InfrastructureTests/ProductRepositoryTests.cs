using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureApp.Tests.InfrastructureTests
{
	public class ProductRepositoryTests
	{
		private readonly ProductDbContext _context;
		private readonly ProductRepository _repository;

		public ProductRepositoryTests()
		{
			// Set up the in-memory database options
			var options = new DbContextOptionsBuilder<ProductDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDatabase")
				.Options;

			// Initialize the context and repository
			_context = new ProductDbContext(options);
			_repository = new ProductRepository(_context);
		}

		[Fact]
		public async Task AddAsync_Should_Add_Product_To_Database()
		{
			// Arrange
			var product = new Product
			{
				Name = "Test Product",
				Description = "Test Description",
				Price = 10.99M,
				StockQuantity = 100
			};

			// Act
			await _repository.AddAsync(product);
			var result = await _context.Products.FirstOrDefaultAsync(p => p.Name == "Test Product");

			// Assert
			Assert.NotNull(result);
			Assert.Equal("Test Product", result.Name);
		}

		[Fact]
		public async Task GetByIdAsync_Should_Return_Product_By_Id()
		{
			// Arrange
			var product = new Product
			{
				Name = "Test Product",
				Description = "Test Description",
				Price = 10.99M,
				StockQuantity = 100
			};
			await _context.Products.AddAsync(product);
			await _context.SaveChangesAsync();

			// Act
			var result = await _repository.GetByIdAsync(product.Id);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(product.Id, result.Id);
		}

		[Fact]
		public async Task UpdateAsync_Should_Update_Product_In_Database()
		{
			// Arrange
			var product = new Product
			{
				Name = "Test Product",
				Description = "Test Description",
				Price = 10.99M,
				StockQuantity = 100
			};
			await _context.Products.AddAsync(product);
			await _context.SaveChangesAsync();

			product.Name = "Updated Product";

			// Act
			await _repository.UpdateAsync(product);
			var result = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);

			// Assert
			Assert.NotNull(result);
			Assert.Equal("Updated Product", result.Name);
		}

		[Fact]
		public async Task DeleteAsync_Should_Remove_Product_From_Database()
		{
			// Arrange
			var product = new Product
			{
				Name = "Test Product",
				Description = "Test Description",
				Price = 10.99M,
				StockQuantity = 100
			};
			await _context.Products.AddAsync(product);
			await _context.SaveChangesAsync();

			// Act
			await _repository.DeleteAsync(product.Id);
			var result = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);

			// Assert
			Assert.Null(result);
		}


	}

}
