using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThreeYellowDucks.AccesoDatos;
using ThreeYellowDucks.Models;
using ThreeYellowDucks.Repositories;
using ThreeYellowDucks.Services;

namespace ThreeYellowDucks.Pages.Productos
{
    public class CreateProductoModel : PageModel
    {
        [BindProperty]
        public Producto Producto { get; set; }
        public List<Categoria> ListaCategorias;
		private readonly ProductService _productService;
        private readonly CategoryService _categoriaService;
		public CreateProductoModel()
		{
			IAccesoDatos<Producto> accesoDatosProducto = new AccesoDatos<Producto>("Productos");
            IRepository<Producto> productRepository = new JsonRepository<Producto>(accesoDatosProducto);
            _productService = new ProductService(productRepository);
            IAccesoDatos<Categoria> accesoDatosCategoria = new AccesoDatos<Categoria>("Categorias");
            IRepository<Categoria> categoryRepository = new JsonRepository<Categoria>(accesoDatosCategoria);
            _categoriaService = new CategoryService(categoryRepository, productRepository);
		}
		public IActionResult OnGet()
        {
            ListaCategorias = _categoriaService.GetAllCategories();

            if(ListaCategorias.Count == 0)
            {
                return RedirectToPage("/Categorias/IndexCategorias");
			}

            return Page();
		}

        public IActionResult OnPost()
        {

            ListaCategorias = _categoriaService.GetAllCategories();

			if (!ModelState.IsValid)
            {
                return Page();
			}
            bool result = _productService.CreateProduct(Producto);

            if (!result)
            { 
                ModelState.AddModelError("Producto.Tipo", "El producto ya existe.");
                return Page();
			}
            return RedirectToPage("IndexProductos");
		}
    }
}
