using ClinicaCitas.Models;

namespace ClinicaCitas.Interfaces
{
    public interface ICancelacionCitas
    {
        Cita Cancelar(int citaId, string motivo);
    }
}
