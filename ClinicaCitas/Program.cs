using System.Text;
using ClinicaCitas.Interfaces;
using ClinicaCitas.Models;
using ClinicaCitas.Notificaciones;
using ClinicaCitas.Repositories;
using ClinicaCitas.Services;
using ClinicaCitas.UI;

namespace ClinicaCitas
{
    internal class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            
            IRepositorio<Paciente> repositorioPacientes = new RepositorioEnMemoria<Paciente>();
            IRepositorio<Medico> repositorioMedicos = new RepositorioEnMemoria<Medico>();
            IRepositorio<Especialidad> repositorioEspecialidades = new RepositorioEnMemoria<Especialidad>();
            IRepositorio<Cita> repositorioCitas = new RepositorioEnMemoria<Cita>();

            IRegistroPacientes pacientes = new PacienteService(repositorioPacientes);
            IRegistroEspecialidades especialidades = new EspecialidadService(repositorioEspecialidades);
            var medicoService = new MedicoService(repositorioMedicos, especialidades);
            IDisponibilidadAgenda disponibilidad = new DisponibilidadAgendaService(repositorioCitas);
            IAgendaCitas agenda = new AgendaCitasService(repositorioCitas, pacientes, medicoService, disponibilidad);
            IConsultaCitas consulta = new ConsultaCitasService(repositorioCitas);
            ICancelacionCitas cancelacion = new CancelacionCitasService(consulta);

            
            var notificadores = new List<INotificador> { new NotificadorCorreo() };
            IRecordatorioCitas recordatorio = new RecordatorioService(consulta, notificadores);

           
            var menu = new MenuPrincipal(
                new MenuPacientes(pacientes),
                new MenuMedicos(medicoService, medicoService),
                new MenuEspecialidades(especialidades),
                new MenuCitas(agenda, consulta, cancelacion, recordatorio));

            menu.Iniciar();
        }
    }
}
