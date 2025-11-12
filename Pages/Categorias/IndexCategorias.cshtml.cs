using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThreeYellowDucks.AccesoDatos;
using ThreeYellowDucks.Models;
using ThreeYellowDucks.Repositories;
using ThreeYellowDucks.Services;

namespace ThreeYellowDucks.Pages.Categorias
{
    public class IndexCategoriasModel : PageModel
    {
        public List<Categoria> ListaCategorias;
        private readonly CategoryService _categoryService;
        public IndexCategoriasModel()
        {
			IAccesoDatos<Categoria> accesoDatos = new AccesoDatos<Categoria>("Categorias");
			IAccesoDatos<Producto> accesoDatosProductos = new AccesoDatos<Producto>("Productos");
			IRepository<Producto> productoRepository = new JsonRepository<Producto>(accesoDatosProductos);
			IRepository<Categoria> categoriaRepository = new JsonRepository<Categoria>(accesoDatos);
			_categoryService = new CategoryService(categoriaRepository, productoRepository);
		}
		public void OnGet()
        {
            ListaCategorias = _categoryService.GetAllCategories();
		}
    }
}
