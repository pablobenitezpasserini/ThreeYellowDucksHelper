using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThreeYellowDucks.AccesoDatos;
using ThreeYellowDucks.Models;
using ThreeYellowDucks.Repositories;
using ThreeYellowDucks.Services;

namespace ThreeYellowDucks.Pages.Categorias
{
	public class EditCategoriaModel : PageModel
	{
		[BindProperty]
		public Categoria Categoria { get; set; }
		private readonly CategoryService _categoryService;
		public EditCategoriaModel()
		{
			IAccesoDatos<Categoria> accesoDatos = new AccesoDatos<Categoria>("Categorias");
			IAccesoDatos<Producto> accesoDatosProductos = new AccesoDatos<Producto>("Productos");
			IRepository<Producto> productoRepository = new JsonRepository<Producto>(accesoDatosProductos);
			IRepository<Categoria> categoriaRepository = new JsonRepository<Categoria>(accesoDatos);
			_categoryService = new CategoryService(categoriaRepository, productoRepository);
		}
		public void OnGet(int id)
		{
			Categoria? categoria = _categoryService.GetCategoryById(id);

			if (categoria is not null)
			{
				Categoria = categoria;
			}
		}

		public IActionResult OnPost()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var result = _categoryService.EditCategory(Categoria);

			if(!result)
			{
				ModelState.AddModelError("Categoria.NombreCategoria", "Ya existe una categoría con ese nombre.");
				return Page();
			}

			return RedirectToPage("IndexCategorias");
		}
	}
}
