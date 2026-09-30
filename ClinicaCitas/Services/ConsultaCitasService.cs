using ClinicaCitas.Interfaces;
using ClinicaCitas.Models;

namespace ClinicaCitas.Services
{
    public class ConsultaCitasService : IConsultaCitas
    {
        private readonly IRepositorio<Cita> _citas;

        public ConsultaCitasService(IRepositorio<Cita> citas)
        {
            _citas = citas;
        }

        public Cita ObtenerPorId(int citaId) =>
            _citas.ObtenerPorId(citaId)
            ?? throw new InvalidOperationException($"No existe una cita con el código {citaId}.");

        public IReadOnlyList<Cita> ObtenerPorPaciente(int pacienteId) =>
            Ordenadas(_citas.ObtenerTodos().Where(c => c.Paciente.Id == pacienteId));

        public IReadOnlyList<Cita> ObtenerPorMedico(int medicoId) =>
            Ordenadas(_citas.ObtenerTodos().Where(c => c.Medico.Id == medicoId));

        public IReadOnlyList<Cita> ObtenerTodas() => Ordenadas(_citas.ObtenerTodos());

        private static IReadOnlyList<Cita> Ordenadas(IEnumerable<Cita> citas) =>
            citas.OrderBy(c => c.FechaHora).ToList();
    }
}
