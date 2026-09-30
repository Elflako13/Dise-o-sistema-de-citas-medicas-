using ClinicaCitas.Models;

namespace ClinicaCitas.Interfaces
{
    public interface IRegistroEspecialidades
    {
        Especialidad Registrar(string nombre, string descripcion);
        Especialidad ObtenerPorId(int id);
        IReadOnlyList<Especialidad> ObtenerTodas();
    }
}
