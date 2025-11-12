using ThreeYellowDucks.AccesoDatos;
using ThreeYellowDucks.Models;

namespace ThreeYellowDucks.Repositories
{
	public class JsonRepository <T> : IRepository<T> where T : class, IIdentificable
	{
		private readonly IAccesoDatos<T> _accesoDatos;
		public JsonRepository(IAccesoDatos<T> acceso)
		{
			_accesoDatos = acceso;
		}

		public List<T> GetAll()
		{
			return _accesoDatos.Leer();
		}

		public void Guardar(List<T> lista)
		{
			_accesoDatos.Guardar(lista);
		}

		public void Create(T entity)
		{
			var lista = _accesoDatos.Leer();

			int nuevoId = lista.Count == 0 ? 1 : lista.Max(c => c.Id) + 1;

			entity.Id = nuevoId;

			lista.Add(entity);
			_accesoDatos.Guardar(lista);
		}

		public T? GetById(int id)
		{
			var lista = _accesoDatos.Leer();
			
			foreach (var item in lista)
			{
				if (item.Id == id)
				{
					return item;
				}
			}
			return null;
		}

		public void Edit(T entity)
		{
			var currentEntity = GetById(entity.Id);

			if (currentEntity is not null)
			{
				var lista = _accesoDatos.Leer();
				int index = lista.FindIndex(e => e.Id == entity.Id);
				lista[index] = entity;
				_accesoDatos.Guardar(lista);
			}
		}

		public void Delete(int id)
		{
			var lista = _accesoDatos.Leer();
			var entityToDelete = GetById(id);

			if (entityToDelete is not null)
			{
				int index = lista.FindIndex(e => e.Id == entityToDelete.Id);
				lista.RemoveAt(index);
				_accesoDatos.Guardar(lista);
			}
			else
			{ 
				return;
			}
		}
	}
}
