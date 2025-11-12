using ThreeYellowDucks.Models;
using ThreeYellowDucks.Repositories;

namespace ThreeYellowDucks.Services
{
	public class ProductService
	{
		IRepository<Producto> _productRepository;
		public ProductService(IRepository<Producto> productRepository)
		{
			_productRepository = productRepository;
		}
		public List<Producto> GetAllProducts()
		{
			return _productRepository.GetAll();
		}
		public Producto? GetProductById(int id)
		{
			return _productRepository.GetById(id);
		}
		public bool CreateProduct(Producto product)
		{
			var products = _productRepository.GetAll();

			if (products.Any(p => p.Tipo.ToLower().Trim() == product.Tipo.ToLower().Trim()))
			{
				return false;
			}

			_productRepository.Create(product);
			return true;
		}

		public bool EditProduct(Producto product)
		{
			var products = _productRepository.GetAll();
			if (products.Any(p => p.Id != product.Id && p.Tipo.ToLower().Trim() == product.Tipo.ToLower().Trim()))
			{
				return false;
			}
			_productRepository.Edit(product);
			return true;
		}
		public void DeleteProduct(int id)
		{
			_productRepository.Delete(id);
		}
	}
}
