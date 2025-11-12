using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThreeYellowDucks.AccesoDatos;
using ThreeYellowDucks.Models;
using ThreeYellowDucks.Repositories;
using ThreeYellowDucks.Services;

namespace ThreeYellowDucks.Pages.Productos
{
    public class IndexProductosModel : PageModel
    {
        public List<Producto> ListaProductos;
        public List<Categoria> ListaCategorias;
		public Dictionary<int, string> CategoriasNombres;

		public decimal TotalGeneral { get; set; }

		private readonly ProductService _productService;
        private readonly CategoryService _categoriaService;
		public IndexProductosModel()
		{
			IAccesoDatos<Producto> accesoDatosProducto = new AccesoDatos<Producto>("Productos");
            IRepository<Producto> productRepository = new JsonRepository<Producto>(accesoDatosProducto);
            _productService = new ProductService(productRepository);
            IAccesoDatos<Categoria> accesoDatosCategoria = new AccesoDatos<Categoria>("Categorias");
            IRepository<Categoria> categoryRepository = new JsonRepository<Categoria>(accesoDatosCategoria);
            _categoriaService = new CategoryService(categoryRepository, productRepository);
		}
		public void OnGet()
        {
            ListaProductos = _productService.GetAllProducts();
            ListaCategorias = _categoriaService.GetAllCategories();
            CategoriasNombres = ListaCategorias.ToDictionary(c => c.Id, c => c.NombreCategoria);

            TotalGeneral = ListaProductos.Sum(p => p.Precio * p.Cantidad);
		}
    }
}
