using ClinicaCitas.Helpers;
using ClinicaCitas.Interfaces;
using ClinicaCitas.Models;

namespace ClinicaCitas.UI
{
    public class MenuMedicos
    {
        private readonly IRegistroMedicos _medicos;
        private readonly IAsignacionEspecialidades _asignacion;

        public MenuMedicos(IRegistroMedicos medicos, IAsignacionEspecialidades asignacion)
        {
            _medicos = medicos;
            _asignacion = asignacion;
        }

        public void Mostrar()
        {
            bool volver = false;

            while (!volver)
            {
                ConsoleHelper.MostrarTitulo("Médicos");
                Console.WriteLine("1. Registrar médico");
                Console.WriteLine("2. Asignar especialidad a un médico");
                Console.WriteLine("3. Listar médicos");
                Console.WriteLine("4. Listar médicos por especialidad");
                Console.WriteLine("0. Volver");
                Console.WriteLine();

                switch (ConsoleHelper.LeerOpcion("Seleccione una opción", 0, 4))
                {
                    case 1: Registrar(); break;
                    case 2: AsignarEspecialidad(); break;
                    case 3: Listar(_medicos.ObtenerTodos(), "Listado de médicos"); break;
                    case 4: ListarPorEspecialidad(); break;
                    case 0: volver = true; break;
                }
            }
        }

        private void Registrar()
        {
            ConsoleHelper.MostrarTitulo("Registrar médico");

            ConsoleHelper.Ejecutar(() =>
            {
                Medico medico = _medicos.Registrar(
                    ConsoleHelper.LeerTexto("Nombre"),
                    ConsoleHelper.LeerTexto("Apellido"),
                    ConsoleHelper.LeerTexto("Exequátur"),
                    ConsoleHelper.LeerTexto("Teléfono"),
                    ConsoleHelper.LeerTexto("Correo"));

                ConsoleHelper.MostrarExito($"\nMédico registrado con el código {medico.Id}.");
            });
        }

        private void AsignarEspecialidad()
        {
            ConsoleHelper.MostrarTitulo("Asignar especialidad");

            ConsoleHelper.Ejecutar(() =>
            {
                int medicoId = ConsoleHelper.LeerEntero("Código del médico");
                int especialidadId = ConsoleHelper.LeerEntero("Código de la especialidad");

                _asignacion.AsignarEspecialidad(medicoId, especialidadId);

                ConsoleHelper.MostrarExito($"\nEspecialidad asignada: {_medicos.ObtenerPorId(medicoId)}");
            });
        }

        private void ListarPorEspecialidad()
        {
            ConsoleHelper.MostrarTitulo("Médicos por especialidad");

            ConsoleHelper.Ejecutar(() =>
            {
                int especialidadId = ConsoleHelper.LeerEntero("Código de la especialidad");
                IReadOnlyList<Medico> medicos = _asignacion.ObtenerMedicosPorEspecialidad(especialidadId);

                Console.WriteLine();

                if (medicos.Count == 0)
                {
                    ConsoleHelper.MostrarAviso("No hay médicos asignados a esa especialidad.");
                    return;
                }

                foreach (Medico m in medicos)
                    Console.WriteLine($"{m.Id,-8} {m}");
            });
        }

        private static void Listar(IReadOnlyList<Medico> medicos, string titulo)
        {
            ConsoleHelper.MostrarTitulo(titulo);

            if (medicos.Count == 0)
            {
                ConsoleHelper.MostrarAviso("No hay médicos registrados.");
                ConsoleHelper.Pausar();
                return;
            }

            Console.WriteLine($"{"Código",-8} {"Nombre",-28} {"Exequátur",-14} Especialidad");
            Console.WriteLine(new string('-', 80));

            foreach (Medico m in medicos)
                Console.WriteLine($"{m.Id,-8} {m.NombreCompleto,-28} {m.Exequatur,-14} {m.NombreEspecialidad}");

            ConsoleHelper.Pausar();
        }
    }
}
