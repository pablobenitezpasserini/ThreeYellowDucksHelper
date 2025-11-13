using ThreeYellowDucks.Models;
using ThreeYellowDucks.Repositories;

namespace ThreeYellowDucks.Services
{
	public class CategoryService
	{
		private readonly IRepository<Categoria> _categoryRepository;
		private readonly IRepository<Producto> _productRepository;
		public CategoryService(IRepository<Categoria> categoryRepository, IRepository<Producto> productRepository)
		{
			_categoryRepository = categoryRepository;
			_productRepository = productRepository;
		}
		public List<Categoria> GetAllCategories()
		{
			return _categoryRepository.GetAll();
		}
		public Categoria? GetCategoryById(int id)
		{
			return _categoryRepository.GetById(id);
		}
		public bool CreateCategory(Categoria category)
		{
			var categories = _categoryRepository.GetAll();

			if (categories.Any(c => c.NombreCategoria.ToLower().Trim() == category.NombreCategoria.ToLower().Trim())) 
			{
				return false;
			}

			_categoryRepository.Create(category);
			return true;
		}
		public bool EditCategory(Categoria category)
		{
			var categories = _categoryRepository.GetAll();

			if (categories.Any(c => c.Id != category.Id && c.NombreCategoria.ToLower().Trim() == category.NombreCategoria.ToLower().Trim()))
			{
				return false;
			}

			_categoryRepository.Edit(category);
			return true;
		}
		public bool DeleteCategory(int id)
		{
			var products = _productRepository.GetAll();
			if (products.Any(p => p.IdCategoria == id))
			{
				return false;
			}

			_categoryRepository.Delete(id);
			return true;
		}
	}
}
