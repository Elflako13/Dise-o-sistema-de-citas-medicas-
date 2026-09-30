using ClinicaCitas.Interfaces;
using ClinicaCitas.Models;

namespace ClinicaCitas.Services
{
  
    public class AgendaCitasService : IAgendaCitas
    {
        private readonly IRepositorio<Cita> _citas;
        private readonly IRegistroPacientes _pacientes;
        private readonly IRegistroMedicos _medicos;
        private readonly IDisponibilidadAgenda _disponibilidad;

        public AgendaCitasService(IRepositorio<Cita> citas, IRegistroPacientes pacientes,
            IRegistroMedicos medicos, IDisponibilidadAgenda disponibilidad)
        {
            _citas = citas;
            _pacientes = pacientes;
            _medicos = medicos;
            _disponibilidad = disponibilidad;
        }

        public Cita Agendar(int pacienteId, int medicoId, DateTime fechaHora, string motivo)
        {
            Paciente paciente = _pacientes.ObtenerPorId(pacienteId);
            Medico medico = _medicos.ObtenerPorId(medicoId);

            ValidarHorarioLibre(pacienteId, medicoId, fechaHora);

            var cita = new Cita(paciente, medico, fechaHora, motivo);
            _citas.Agregar(cita);

            return cita;
        }

        public Cita Reprogramar(int citaId, DateTime nuevaFechaHora)
        {
            Cita cita = _citas.ObtenerPorId(citaId)
                ?? throw new InvalidOperationException($"No existe una cita con el código {citaId}.");

            ValidarHorarioLibre(cita.Paciente.Id, cita.Medico.Id, nuevaFechaHora, cita.Id);

            cita.Reprogramar(nuevaFechaHora);
            return cita;
        }

        private void ValidarHorarioLibre(int pacienteId, int medicoId, DateTime fechaHora, int citaIdIgnorada = 0)
        {
            if (!_disponibilidad.MedicoEstaDisponible(medicoId, fechaHora, citaIdIgnorada))
                throw new InvalidOperationException("El médico ya tiene una cita en ese horario.");

            if (!_disponibilidad.PacienteEstaDisponible(pacienteId, fechaHora, citaIdIgnorada))
                throw new InvalidOperationException("El paciente ya tiene una cita en ese horario.");
        }
    }
}
