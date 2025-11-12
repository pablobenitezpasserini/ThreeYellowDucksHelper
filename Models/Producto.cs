using System.ComponentModel.DataAnnotations;

namespace ThreeYellowDucks.Models
{
	public class Producto : IIdentificable
	{
		public int Id { get; set; }
		[Display(Name = "Marca")]
		[Required(ErrorMessage = "La marca del producto no puede estar vacio")]
		[StringLength(30, MinimumLength = 2, ErrorMessage = "La marca del producto debe tener entre 2 y 30 caracteres")]
		public string MarcaNombre { get; set; }
		[Display(Name = "Tipo de Producto")]
		[Required(ErrorMessage = "El tipo de producto no puede estar vacio")]
		[StringLength(30, MinimumLength = 2, ErrorMessage = "El tipo de producto debe tener entre 2 y 30 caracteres")]
		public string Tipo { get; set; }
		[Required(ErrorMessage = "El precio del producto no puede estar vacio")]
		[Range(0.01, double.MaxValue, ErrorMessage = $"El precio debe ser mayor a 0.1")]
		public decimal Precio { get; set; }
		[Required(ErrorMessage = "La cantidad del producto no puede estar vacio")]
		[Range(0, int.MaxValue, ErrorMessage = "La cantidad no puede ser negativa")]
		public int Cantidad { get; set; }
		[Required(ErrorMessage = "La categoria del producto no puede estar vacio")]
		public int IdCategoria { get; set; }
	}
}
