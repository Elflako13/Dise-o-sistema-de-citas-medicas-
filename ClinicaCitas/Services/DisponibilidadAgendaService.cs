using ClinicaCitas.Interfaces;
using ClinicaCitas.Models;

namespace ClinicaCitas.Services
{
  
    public class DisponibilidadAgendaService : IDisponibilidadAgenda
    {
        private readonly IRepositorio<Cita> _citas;

        public DisponibilidadAgendaService(IRepositorio<Cita> citas)
        {
            _citas = citas;
        }

        public bool MedicoEstaDisponible(int medicoId, DateTime fechaHora, int citaIdIgnorada = 0) =>
            !CitasActivas(citaIdIgnorada).Any(c => c.Medico.Id == medicoId && c.SeSolapaCon(fechaHora));

        public bool PacienteEstaDisponible(int pacienteId, DateTime fechaHora, int citaIdIgnorada = 0) =>
            !CitasActivas(citaIdIgnorada).Any(c => c.Paciente.Id == pacienteId && c.SeSolapaCon(fechaHora));

        private IEnumerable<Cita> CitasActivas(int citaIdIgnorada) =>
            _citas.ObtenerTodos().Where(c => c.EstaActiva && c.Id != citaIdIgnorada);
    }
}
