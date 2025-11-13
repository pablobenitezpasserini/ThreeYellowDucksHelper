using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThreeYellowDucks.AccesoDatos;
using ThreeYellowDucks.Models;
using ThreeYellowDucks.Repositories;
using ThreeYellowDucks.Services;

namespace ThreeYellowDucks.Pages.Productos
{
    public class EditProductoModel : PageModel
    {
        [BindProperty]
        public Producto Producto { get; set; }
		public List<Categoria> ListaCategorias;
        public Dictionary<int, string> CategoriaDict;
		private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
		public EditProductoModel()
		{
			IAccesoDatos<Producto> accesoDatosProducto = new AccesoDatos<Producto>("Productos");
            IRepository<Producto> productoRepository = new JsonRepository<Producto>(accesoDatosProducto);
            _productService = new ProductService(productoRepository);
            IAccesoDatos<Categoria> accesoDatosCategoria = new AccesoDatos<Categoria>("Categorias");
            IRepository<Categoria> categoriaRepository = new JsonRepository<Categoria>(accesoDatosCategoria);
            _categoryService = new CategoryService(categoriaRepository, productoRepository);
		}

		public IActionResult OnGet(int id)
        {
			Producto? producto = _productService.GetProductById(id);

            if (producto is not null)
            {
				ListaCategorias = _categoryService.GetAllCategories();
				CategoriaDict = ListaCategorias.ToDictionary(c => c.Id, c => c.NombreCategoria);
				Producto = producto;
                return Page();
			}
            else
            {
                return RedirectToPage("IndexProductos");
			}
		}
        public IActionResult OnPost() 
        {
            ListaCategorias = _categoryService.GetAllCategories();
			CategoriaDict = ListaCategorias.ToDictionary(c => c.Id, c => c.NombreCategoria);

			if (!ModelState.IsValid)
            {
                return Page();
			}

            var result = _productService.EditProduct(Producto);

            if (!result)
            {
                ModelState.AddModelError("Producto.Tipo", "Ya existe un producto con ese nombre.");
                return Page();
            }

            return RedirectToPage("IndexProductos");
		}
    }
}
