using ClinicaCitas.Interfaces;
using ClinicaCitas.Models;

namespace ClinicaCitas.Repositories
{
  
    public class RepositorioEnMemoria<T> : IRepositorio<T> where T : EntidadBase
    {
        private readonly List<T> _elementos = new();
        private int _ultimoId;

        public void Agregar(T entidad)
        {
            if (entidad == null)
                throw new ArgumentException("No se puede guardar un elemento vacío.");

            entidad.Id = ++_ultimoId;
            _elementos.Add(entidad);
        }

        public T? ObtenerPorId(int id) => _elementos.FirstOrDefault(e => e.Id == id);

        public IReadOnlyList<T> ObtenerTodos() => _elementos.AsReadOnly();
    }
}
