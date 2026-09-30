using ClinicaCitas.Models;

namespace ClinicaCitas.Interfaces
{
    public interface IRegistroPacientes
    {
        Paciente Registrar(string nombre, string apellido, string cedula, string telefono, string correo);
        Paciente ObtenerPorId(int id);
        IReadOnlyList<Paciente> ObtenerTodos();
    }
}
