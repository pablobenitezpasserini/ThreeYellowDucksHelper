using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThreeYellowDucks.AccesoDatos;
using ThreeYellowDucks.Models;
using ThreeYellowDucks.Repositories;
using ThreeYellowDucks.Services;

namespace ThreeYellowDucks.Pages.Productos
{
    public class DeleteProductoModel : PageModel
    {
        [BindProperty]
        public Producto Producto { get; set; }
        public List<Categoria> ListaCategorias;
        public Dictionary<int, string> CategoriaDict;
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        public DeleteProductoModel()
        {
            IAccesoDatos<Producto> accesoDatos = new AccesoDatos<Producto>("Productos");
            IRepository<Producto> productoRepository = new JsonRepository<Producto>(accesoDatos);
            _productService = new ProductService(productoRepository);
            IAccesoDatos<Categoria> accesoDatosCategoria = new AccesoDatos<Categoria>("Categorias");
            IRepository<Categoria> categoriaRepository = new JsonRepository<Categoria>(accesoDatosCategoria);
            _categoryService = new CategoryService(categoriaRepository, productoRepository);
        }
        public IActionResult OnGet(int id)
        {
            ListaCategorias = _categoryService.GetAllCategories();
            CategoriaDict = ListaCategorias.ToDictionary(c => c.Id, c => c.NombreCategoria);

            var producto = _productService.GetProductById(id);
            if (producto is null)
            {
                return RedirectToPage("IndexProductos");
            }

            Producto = producto;
            return Page();
        }

        public IActionResult OnPost()
        {
            _productService.DeleteProduct(Producto.Id);
            return RedirectToPage("IndexProductos");
		}
    }
}
