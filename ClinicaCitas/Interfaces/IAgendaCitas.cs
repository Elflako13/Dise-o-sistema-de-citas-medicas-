using ClinicaCitas.Models;

namespace ClinicaCitas.Interfaces
{
    public interface IAgendaCitas
    {
        Cita Agendar(int pacienteId, int medicoId, DateTime fechaHora, string motivo);
        Cita Reprogramar(int citaId, DateTime nuevaFechaHora);
    }
}
