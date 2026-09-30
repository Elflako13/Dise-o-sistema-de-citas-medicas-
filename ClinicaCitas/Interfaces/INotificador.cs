using ClinicaCitas.Models;

namespace ClinicaCitas.Interfaces
{
    public interface INotificador
    {
        string Canal { get; }
        void Enviar(Cita cita, string mensaje);
    }
}
