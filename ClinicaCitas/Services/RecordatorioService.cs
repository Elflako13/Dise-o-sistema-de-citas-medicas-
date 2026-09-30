using ClinicaCitas.Interfaces;
using ClinicaCitas.Models;

namespace ClinicaCitas.Services
{
   
    public class RecordatorioService : IRecordatorioCitas
    {
        private readonly IConsultaCitas _consulta;
        private readonly IReadOnlyList<INotificador> _notificadores;

        public RecordatorioService(IConsultaCitas consulta, IReadOnlyList<INotificador> notificadores)
        {
            _consulta = consulta;
            _notificadores = notificadores;
        }

        public void EnviarRecordatorio(int citaId)
        {
            Cita cita = _consulta.ObtenerPorId(citaId);

            if (!cita.EstaActiva)
                throw new InvalidOperationException("No se envían recordatorios de citas canceladas.");

            if (_notificadores.Count == 0)
                throw new InvalidOperationException("No hay ningún canal de notificación configurado.");

            string mensaje = ArmarMensaje(cita);

            foreach (INotificador notificador in _notificadores)
                notificador.Enviar(cita, mensaje);
        }

        private static string ArmarMensaje(Cita cita) =>
            $"Hola {cita.Paciente.Nombre}, le recordamos su cita del " +
            $"{cita.FechaHora:dd/MM/yyyy} a las {cita.FechaHora:hh:mm tt} " +
            $"con {cita.Medico}. Motivo: {cita.Motivo}.";
    }
}
