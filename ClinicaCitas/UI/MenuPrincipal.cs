using ClinicaCitas.Helpers;

namespace ClinicaCitas.UI
{
    public class MenuPrincipal
    {
        private readonly MenuPacientes _menuPacientes;
        private readonly MenuMedicos _menuMedicos;
        private readonly MenuEspecialidades _menuEspecialidades;
        private readonly MenuCitas _menuCitas;

        public MenuPrincipal(MenuPacientes menuPacientes, MenuMedicos menuMedicos,
            MenuEspecialidades menuEspecialidades, MenuCitas menuCitas)
        {
            _menuPacientes = menuPacientes;
            _menuMedicos = menuMedicos;
            _menuEspecialidades = menuEspecialidades;
            _menuCitas = menuCitas;
        }

        public void Iniciar()
        {
            bool salir = false;

            while (!salir)
            {
                ConsoleHelper.MostrarTitulo("Clínica - Sistema de Gestión de Citas Médicas");
                Console.WriteLine("1. Pacientes");
                Console.WriteLine("2. Médicos");
                Console.WriteLine("3. Especialidades");
                Console.WriteLine("4. Citas médicas");
                Console.WriteLine("0. Salir");
                Console.WriteLine();

                switch (ConsoleHelper.LeerOpcion("Seleccione una opción", 0, 4))
                {
                    case 1: _menuPacientes.Mostrar(); break;
                    case 2: _menuMedicos.Mostrar(); break;
                    case 3: _menuEspecialidades.Mostrar(); break;
                    case 4: _menuCitas.Mostrar(); break;
                    case 0: salir = true; break;
                }
            }

            Console.WriteLine("¡Gracias por usar el sistema!");
        }
    }
}
