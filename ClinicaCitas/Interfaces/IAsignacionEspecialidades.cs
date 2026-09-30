using ClinicaCitas.Models;

namespace ClinicaCitas.Interfaces
{
    public interface IAsignacionEspecialidades
    {
        void AsignarEspecialidad(int medicoId, int especialidadId);
        IReadOnlyList<Medico> ObtenerMedicosPorEspecialidad(int especialidadId);
    }
}
