using ClinicaCitas.Models;

namespace ClinicaCitas.Interfaces
{
    public interface IConsultaCitas
    {
        Cita ObtenerPorId(int citaId);
        IReadOnlyList<Cita> ObtenerPorPaciente(int pacienteId);
        IReadOnlyList<Cita> ObtenerPorMedico(int medicoId);
        IReadOnlyList<Cita> ObtenerTodas();
    }
}
