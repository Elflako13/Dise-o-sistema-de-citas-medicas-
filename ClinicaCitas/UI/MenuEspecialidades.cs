using ClinicaCitas.Helpers;
using ClinicaCitas.Interfaces;
using ClinicaCitas.Models;

namespace ClinicaCitas.UI
{
    public class MenuEspecialidades
    {
        private readonly IRegistroEspecialidades _especialidades;

        public MenuEspecialidades(IRegistroEspecialidades especialidades)
        {
            _especialidades = especialidades;
        }

        public void Mostrar()
        {
            bool volver = false;

            while (!volver)
            {
                ConsoleHelper.MostrarTitulo("Especialidades");
                Console.WriteLine("1. Registrar especialidad");
                Console.WriteLine("2. Listar especialidades");
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
            ConsoleHelper.MostrarTitulo("Registrar especialidad");

            ConsoleHelper.Ejecutar(() =>
            {
                Especialidad especialidad = _especialidades.Registrar(
                    ConsoleHelper.LeerTexto("Nombre de la especialidad"),
                    ConsoleHelper.LeerTexto("Descripción"));

                ConsoleHelper.MostrarExito($"\nEspecialidad registrada con el código {especialidad.Id}.");
            });
        }

        private void Listar()
        {
            ConsoleHelper.MostrarTitulo("Listado de especialidades");
            IReadOnlyList<Especialidad> especialidades = _especialidades.ObtenerTodas();

            if (especialidades.Count == 0)
            {
                ConsoleHelper.MostrarAviso("No hay especialidades registradas.");
                ConsoleHelper.Pausar();
                return;
            }

            Console.WriteLine($"{"Código",-8} {"Nombre",-25} Descripción");
            Console.WriteLine(new string('-', 80));

            foreach (Especialidad e in especialidades)
                Console.WriteLine($"{e.Id,-8} {e.Nombre,-25} {e.Descripcion}");

            ConsoleHelper.Pausar();
        }
    }
}
