using ClinicaCitas.Helpers;
using ClinicaCitas.Interfaces;
using ClinicaCitas.Models;

namespace ClinicaCitas.UI
{
   
    public class MenuPacientes
    {
        private readonly IRegistroPacientes _pacientes;

        public MenuPacientes(IRegistroPacientes pacientes)
        {
            _pacientes = pacientes;
        }

        public void Mostrar()
        {
            bool volver = false;

            while (!volver)
            {
                ConsoleHelper.MostrarTitulo("Pacientes");
                Console.WriteLine("1. Registrar paciente");
                Console.WriteLine("2. Listar pacientes");
                Console.WriteLine("0. Volver");
                Console.WriteLine();

                switch (ConsoleHelper.LeerOpcion("Seleccione una opción", 0, 2))
                {
                    case 1: Registrar(); break;
                    case 2: Listar(); break;
                    case 0: volver = true; break;
                }
            }
        }

        private void Registrar()
        {
            ConsoleHelper.MostrarTitulo("Registrar paciente");

            ConsoleHelper.Ejecutar(() =>
            {
                Paciente paciente = _pacientes.Registrar(
                    ConsoleHelper.LeerTexto("Nombre"),
                    ConsoleHelper.LeerTexto("Apellido"),
                    ConsoleHelper.LeerTexto("Cédula"),
                    ConsoleHelper.LeerTexto("Teléfono"),
                    ConsoleHelper.LeerTexto("Correo"));

                ConsoleHelper.MostrarExito($"\nPaciente registrado con el código {paciente.Id}.");
            });
        }

        private void Listar()
        {
            ConsoleHelper.MostrarTitulo("Listado de pacientes");
            IReadOnlyList<Paciente> pacientes = _pacientes.ObtenerTodos();

            if (pacientes.Count == 0)
            {
                ConsoleHelper.MostrarAviso("No hay pacientes registrados.");
                ConsoleHelper.Pausar();
                return;
            }

            Console.WriteLine($"{"Código",-8} {"Nombre",-28} {"Cédula",-16} {"Teléfono",-14} Correo");
            Console.WriteLine(new string('-', 90));

            foreach (Paciente p in pacientes)
                Console.WriteLine($"{p.Id,-8} {p.NombreCompleto,-28} {p.Cedula,-16} {p.Telefono,-14} {p.Correo}");

            ConsoleHelper.Pausar();
        }
    }
}
