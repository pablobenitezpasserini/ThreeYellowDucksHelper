using System.ComponentModel.DataAnnotations;

namespace ThreeYellowDucks.Models
{
	public class Categoria : IIdentificable
	{
		public int Id { get; set; }
		[Display(Name = "Nombre de la Categoria")]
		[Required(ErrorMessage = "El nombre de la categoria no puede estar vacio")]
		[StringLength(20, MinimumLength = 2, ErrorMessage = "El nombre de la categoria debe tener entre 2 y 20 caracteres")]
		public string NombreCategoria { get; set; }
	}
}
