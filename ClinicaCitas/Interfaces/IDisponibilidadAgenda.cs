namespace ClinicaCitas.Interfaces
{
    public interface IDisponibilidadAgenda
    {
        bool MedicoEstaDisponible(int medicoId, DateTime fechaHora, int citaIdIgnorada = 0);
        bool PacienteEstaDisponible(int pacienteId, DateTime fechaHora, int citaIdIgnorada = 0);
    }
}
