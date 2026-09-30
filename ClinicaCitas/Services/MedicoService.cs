using ClinicaCitas.Interfaces;
using ClinicaCitas.Models;

namespace ClinicaCitas.Services
{
  
    public class MedicoService : IRegistroMedicos, IAsignacionEspecialidades
    {
        private readonly IRepositorio<Medico> _repositorio;
        private readonly IRegistroEspecialidades _especialidades;

        public MedicoService(IRepositorio<Medico> repositorio, IRegistroEspecialidades especialidades)
        {
            _repositorio = repositorio;
            _especialidades = especialidades;
        }

        public Medico Registrar(string nombre, string apellido, string exequatur, string telefono, string correo)
        {
            var medico = new Medico(nombre, apellido, exequatur, telefono, correo);

            bool exequaturRepetido = _repositorio.ObtenerTodos()
                .Any(m => m.Exequatur.Equals(medico.Exequatur, StringComparison.OrdinalIgnoreCase));

            if (exequaturRepetido)
                throw new InvalidOperationException($"Ya existe un médico con el exequátur {medico.Exequatur}.");

            _repositorio.Agregar(medico);
            return medico;
        }

        public Medico ObtenerPorId(int id) =>
            _repositorio.ObtenerPorId(id)
            ?? throw new InvalidOperationException($"No existe un médico con el código {id}.");

        public IReadOnlyList<Medico> ObtenerTodos() => _repositorio.ObtenerTodos();

        public void AsignarEspecialidad(int medicoId, int especialidadId)
        {
            Medico medico = ObtenerPorId(medicoId);
            Especialidad especialidad = _especialidades.ObtenerPorId(especialidadId);

            medico.AsignarEspecialidad(especialidad);
        }

        public IReadOnlyList<Medico> ObtenerMedicosPorEspecialidad(int especialidadId) =>
            _repositorio.ObtenerTodos()
                .Where(m => m.Especialidad != null && m.Especialidad.Id == especialidadId)
                .ToList();
    }
}
