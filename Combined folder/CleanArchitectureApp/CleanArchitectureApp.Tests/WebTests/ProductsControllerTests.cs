using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureApp.Tests.WebTests
{
	public class ProductsControllerTests : IClassFixture<WebApplicationFactory<CleanArchitectureApp.Web.Program>>
	{
		private readonly HttpClient _client;

		public ProductsControllerTests(WebApplicationFactory<CleanArchitectureApp.Web.Program> factory)
		{
			_client = factory.CreateClient();
		}

		[Fact]
		public async Task Post_CreateProduct_Returns_Ok()
		{
			// Arrange
			var product = new
			{
				Name = "Test Product",
				Description = "Test Description",
				Price = 10.99M,
				StockQuantity = 100
			};
			var content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");

			// Act
			var response = await _client.PostAsync("/api/products", content);

			// Assert
			response.EnsureSuccessStatusCode();
			Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
		}

		[Fact]
		public async Task Get_GetProductById_Returns_Product()
		{
			// Arrange
			var productId = 1; // Assume this product ID exists in the database.

			// Act
			var response = await _client.GetAsync($"/api/products/{productId}");

			// Assert
			response.EnsureSuccessStatusCode();
			var product = JsonSerializer.Deserialize<Product>(await response.Content.ReadAsStringAsync());
			Assert.Equal(productId, product.Id);
		}

		[Fact]
		public async Task Put_UpdateProduct_Returns_NoContent()
		{
			// Arrange
			var productId = 1; // Assume this product ID exists in the database.
			var updatedProduct = new
			{
				Id = productId,
				Name = "Updated Product",
				Description = "Updated Description",
				Price = 15.99M,
				StockQuantity = 50
			};
			var content = new StringContent(JsonSerializer.Serialize(updatedProduct), Encoding.UTF8, "application/json");

			// Act
			var response = await _client.PutAsync($"/api/products/{productId}", content);

			// Assert
			response.EnsureSuccessStatusCode();
			Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
		}

		[Fact]
		public async Task Delete_DeleteProduct_Returns_NoContent()
		{
			// Arrange
			var productId = 1; // Assume this product ID exists in the database.

			// Act
			var response = await _client.DeleteAsync($"/api/products/{productId}");

			// Assert
			response.EnsureSuccessStatusCode();
			Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
		}


	}
}
