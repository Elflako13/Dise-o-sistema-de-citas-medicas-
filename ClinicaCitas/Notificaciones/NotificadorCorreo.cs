using ClinicaCitas.Interfaces;
using ClinicaCitas.Models;

namespace ClinicaCitas.Notificaciones
{
 
    public class NotificadorCorreo : INotificador
    {
        public string Canal => "Correo electrónico";

        public void Enviar(Cita cita, string mensaje)
        {
            Console.WriteLine($"[{Canal}] Para: {cita.Paciente.Correo}");
            Console.WriteLine($"[{Canal}] {mensaje}");
        }
    }
}
