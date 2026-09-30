using ClinicaCitas.Models;

namespace ClinicaCitas.Interfaces
{
    public interface IRegistroMedicos
    {
        Medico Registrar(string nombre, string apellido, string exequatur, string telefono, string correo);
        Medico ObtenerPorId(int id);
        IReadOnlyList<Medico> ObtenerTodos();
    }
}
