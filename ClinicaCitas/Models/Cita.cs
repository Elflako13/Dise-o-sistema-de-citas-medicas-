using ClinicaCitas.Validaciones;

namespace ClinicaCitas.Models
{
    public class Cita : EntidadBase
    {
        public static readonly TimeSpan Duracion = TimeSpan.FromMinutes(30);

        public Paciente Paciente { get; }
        public Medico Medico { get; }
        public DateTime FechaHora { get; private set; }
        public string Motivo { get; }
        public EstadoCita Estado { get; private set; }
        public string MotivoCancelacion { get; private set; } = string.Empty;

        public Cita(Paciente paciente, Medico medico, DateTime fechaHora, string motivo)
        {
            Paciente = paciente ?? throw new ArgumentException("El paciente es obligatorio.");
            Medico = medico ?? throw new ArgumentException("El médico es obligatorio.");
            FechaHora = Validador.FechaDeCitaValida(fechaHora);
            Motivo = Validador.TextoRequerido(motivo, "Motivo de la cita");
            Estado = EstadoCita.Programada;
        }

        public DateTime FechaHoraFin => FechaHora + Duracion;

        public bool EstaActiva => Estado != EstadoCita.Cancelada;

        public void Reprogramar(DateTime nuevaFechaHora)
        {
            if (!EstaActiva)
                throw new InvalidOperationException("No se puede reprogramar una cita cancelada.");

            FechaHora = Validador.FechaDeCitaValida(nuevaFechaHora);
            Estado = EstadoCita.Reprogramada;
        }

        public void Cancelar(string motivo)
        {
            if (!EstaActiva)
                throw new InvalidOperationException("La cita ya está cancelada.");

            MotivoCancelacion = Validador.TextoRequerido(motivo, "Motivo de la cancelación");
            Estado = EstadoCita.Cancelada;
        }
        public bool SeSolapaCon(DateTime fechaHora) =>
            fechaHora < FechaHoraFin && FechaHora < fechaHora + Duracion;

        public override string ToString() =>
            $"#{Id} | {FechaHora:dd/MM/yyyy hh:mm tt} | {Paciente.NombreCompleto} con {Medico} | {Estado}";
    }
}
