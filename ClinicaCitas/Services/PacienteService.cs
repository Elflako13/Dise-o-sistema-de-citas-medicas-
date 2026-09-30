using ClinicaCitas.Interfaces;
using ClinicaCitas.Models;

namespace ClinicaCitas.Services
{
    public class PacienteService : IRegistroPacientes
    {
        private readonly IRepositorio<Paciente> _repositorio;

        public PacienteService(IRepositorio<Paciente> repositorio)
        {
            _repositorio = repositorio;
        }

        public Paciente Registrar(string nombre, string apellido, string cedula, string telefono, string correo)
        {
            var paciente = new Paciente(nombre, apellido, cedula, telefono, correo);

            bool cedulaRepetida = _repositorio.ObtenerTodos()
                .Any(p => p.Cedula.Equals(paciente.Cedula, StringComparison.OrdinalIgnoreCase));

            if (cedulaRepetida)
                throw new InvalidOperationException($"Ya existe un paciente con la cédula {paciente.Cedula}.");

            _repositorio.Agregar(paciente);
            return paciente;
        }

        public Paciente ObtenerPorId(int id) =>
            _repositorio.ObtenerPorId(id)
            ?? throw new InvalidOperationException($"No existe un paciente con el código {id}.");

        public IReadOnlyList<Paciente> ObtenerTodos() => _repositorio.ObtenerTodos();
    }
}
