using System.Globalization;

namespace ClinicaCitas.Helpers
{
    public static class ConsoleHelper
    {
        private const string FormatoFecha = "dd/MM/yyyy HH:mm";

        public static void MostrarTitulo(string titulo)
        {
            if (!Console.IsOutputRedirected)
                Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(new string('=', 60));
            Console.WriteLine($"  {titulo.ToUpper()}");
            Console.WriteLine(new string('=', 60));
            Console.ResetColor();
        }

        public static void MostrarExito(string mensaje) => MostrarMensaje(mensaje, ConsoleColor.Green);

        public static void MostrarError(string mensaje) => MostrarMensaje(mensaje, ConsoleColor.Red);

        public static void MostrarAviso(string mensaje) => MostrarMensaje(mensaje, ConsoleColor.Yellow);

        public static void Pausar()
        {
            Console.WriteLine();
            Console.Write("Presione cualquier tecla para continuar...");

            if (Console.IsInputRedirected)
                Console.ReadLine();
            else
                Console.ReadKey(true);
        }

        public static string LeerTexto(string etiqueta)
        {
            while (true)
            {
                Console.Write($"{etiqueta}: ");
                string entrada = Console.ReadLine()?.Trim() ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(entrada))
                    return entrada;

                MostrarError("Este campo es obligatorio.");
            }
        }

        public static int LeerEntero(string etiqueta, int minimo = 1)
        {
            while (true)
            {
                Console.Write($"{etiqueta}: ");
                string entrada = Console.ReadLine()?.Trim() ?? string.Empty;

                if (int.TryParse(entrada, out int valor) && valor >= minimo)
                    return valor;

                MostrarError($"Debe ingresar un número válido mayor o igual a {minimo}.");
            }
        }

        public static int LeerOpcion(string etiqueta, int minimo, int maximo)
        {
            while (true)
            {
                Console.Write($"{etiqueta}: ");
                string entrada = Console.ReadLine()?.Trim() ?? string.Empty;

                if (int.TryParse(entrada, out int opcion) && opcion >= minimo && opcion <= maximo)
                    return opcion;

                MostrarError($"Opción inválida. Elija un número entre {minimo} y {maximo}.");
            }
        }

        public static DateTime LeerFechaHora(string etiqueta)
        {
            while (true)
            {
                Console.Write($"{etiqueta} ({FormatoFecha}): ");
                string entrada = Console.ReadLine()?.Trim() ?? string.Empty;

                if (DateTime.TryParseExact(entrada, FormatoFecha, CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out DateTime fecha))
                    return fecha;

                MostrarError($"Formato inválido. Ejemplo: {DateTime.Now.AddDays(1):dd/MM/yyyy} 09:30");
            }
        }

        public static bool Confirmar(string pregunta)
        {
            Console.Write($"{pregunta} (S/N): ");
            string respuesta = Console.ReadLine()?.Trim().ToUpper() ?? string.Empty;

            return respuesta == "S" || respuesta == "SI";
        }

        public static void Ejecutar(Action accion)
        {
            try
            {
                accion();
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                MostrarError($"\nError: {ex.Message}");
            }

            Pausar();
        }

        private static void MostrarMensaje(string mensaje, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(mensaje);
            Console.ResetColor();
        }
    }
}
