using ClinicaCitas.Models;

namespace ClinicaCitas.Interfaces
{
    public interface IRepositorio<T> where T : EntidadBase
    {
        void Agregar(T entidad);
        T? ObtenerPorId(int id);
        IReadOnlyList<T> ObtenerTodos();
    }
}
