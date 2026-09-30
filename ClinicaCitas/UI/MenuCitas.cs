using ClinicaCitas.Helpers;
using ClinicaCitas.Interfaces;
using ClinicaCitas.Models;

namespace ClinicaCitas.UI
{
    public class MenuCitas
    {
        private readonly IAgendaCitas _agenda;
        private readonly IConsultaCitas _consulta;
        private readonly ICancelacionCitas _cancelacion;
        private readonly IRecordatorioCitas _recordatorio;

        public MenuCitas(IAgendaCitas agenda, IConsultaCitas consulta,
            ICancelacionCitas cancelacion, IRecordatorioCitas recordatorio)
        {
            _agenda = agenda;
            _consulta = consulta;
            _cancelacion = cancelacion;
            _recordatorio = recordatorio;
        }

        public void Mostrar()
        {
            bool volver = false;

            while (!volver)
            {
                ConsoleHelper.MostrarTitulo("Citas médicas");
                Console.WriteLine("1. Agendar cita");
                Console.WriteLine("2. Consultar citas por paciente");
                Console.WriteLine("3. Consultar citas por médico");
                Console.WriteLine("4. Reprogramar cita");
                Console.WriteLine("5. Cancelar cita");
                Console.WriteLine("6. Enviar recordatorio de cita");
                Console.WriteLine("7. Listar todas las citas");
                Console.WriteLine("0. Volver");
                Console.WriteLine();

                switch (ConsoleHelper.LeerOpcion("Seleccione una opción", 0, 7))
                {
                    case 1: Agendar(); break;
                    case 2: ConsultarPorPaciente(); break;
                    case 3: ConsultarPorMedico(); break;
                    case 4: Reprogramar(); break;
                    case 5: Cancelar(); break;
                    case 6: EnviarRecordatorio(); break;
                    case 7: ListarTodas(); break;
                    case 0: volver = true; break;
                }
            }
        }

        private void Agendar()
        {
            ConsoleHelper.MostrarTitulo("Agendar cita");

            ConsoleHelper.Ejecutar(() =>
            {
                Cita cita = _agenda.Agendar(
                    ConsoleHelper.LeerEntero("Código del paciente"),
                    ConsoleHelper.LeerEntero("Código del médico"),
                    ConsoleHelper.LeerFechaHora("Fecha y hora"),
                    ConsoleHelper.LeerTexto("Motivo"));

                ConsoleHelper.MostrarExito($"\nCita agendada:\n{cita}");
            });
        }

        private void ConsultarPorPaciente()
        {
            ConsoleHelper.MostrarTitulo("Citas por paciente");

            ConsoleHelper.Ejecutar(() =>
            {
                int pacienteId = ConsoleHelper.LeerEntero("Código del paciente");
                MostrarCitas(_consulta.ObtenerPorPaciente(pacienteId));
            });
        }

        private void ConsultarPorMedico()
        {
            ConsoleHelper.MostrarTitulo("Citas por médico");

            ConsoleHelper.Ejecutar(() =>
            {
                int medicoId = ConsoleHelper.LeerEntero("Código del médico");
                MostrarCitas(_consulta.ObtenerPorMedico(medicoId));
            });
        }

        private void Reprogramar()
        {
            ConsoleHelper.MostrarTitulo("Reprogramar cita");

            ConsoleHelper.Ejecutar(() =>
            {
                int citaId = ConsoleHelper.LeerEntero("Código de la cita");
                Console.WriteLine($"Cita actual: {_consulta.ObtenerPorId(citaId)}");

                Cita cita = _agenda.Reprogramar(citaId, ConsoleHelper.LeerFechaHora("Nueva fecha y hora"));

                ConsoleHelper.MostrarExito($"\nCita reprogramada:\n{cita}");
            });
        }

        private void Cancelar()
        {
            ConsoleHelper.MostrarTitulo("Cancelar cita");

            ConsoleHelper.Ejecutar(() =>
            {
                int citaId = ConsoleHelper.LeerEntero("Código de la cita");
                Console.WriteLine($"Cita: {_consulta.ObtenerPorId(citaId)}");

                if (!ConsoleHelper.Confirmar("¿Confirma la cancelación?"))
                {
                    ConsoleHelper.MostrarAviso("Operación cancelada.");
                    return;
                }

                Cita cita = _cancelacion.Cancelar(citaId, ConsoleHelper.LeerTexto("Motivo de la cancelación"));

                ConsoleHelper.MostrarExito($"\nCita cancelada:\n{cita}");
            });
        }

        private void EnviarRecordatorio()
        {
            ConsoleHelper.MostrarTitulo("Enviar recordatorio");

            ConsoleHelper.Ejecutar(() =>
            {
                int citaId = ConsoleHelper.LeerEntero("Código de la cita");
                Console.WriteLine();

                _recordatorio.EnviarRecordatorio(citaId);

                ConsoleHelper.MostrarExito("\nRecordatorio enviado.");
            });
        }

        private void ListarTodas()
        {
            ConsoleHelper.MostrarTitulo("Todas las citas");
            MostrarCitas(_consulta.ObtenerTodas());
            ConsoleHelper.Pausar();
        }

        private static void MostrarCitas(IReadOnlyList<Cita> citas)
        {
            Console.WriteLine();

            if (citas.Count == 0)
            {
                ConsoleHelper.MostrarAviso("No hay citas para mostrar.");
                return;
            }

            Console.WriteLine($"{"Código",-8} {"Fecha y hora",-22} {"Paciente",-24} {"Médico",-28} Estado");
            Console.WriteLine(new string('-', 100));

            foreach (Cita c in citas)
                Console.WriteLine($"{c.Id,-8} {c.FechaHora,-22:dd/MM/yyyy hh:mm tt} {c.Paciente.NombreCompleto,-24} " +
                                  $"{c.Medico.NombreCompleto,-28} {c.Estado}");
        }
    }
}
