namespace ClinicaCitas.Validaciones
{
  
    public static class Validador
    {
        public const int HoraApertura = 8;   // 8:00 a.m.
        public const int HoraCierre = 17;    // 5:00 p.m.

        public static string TextoRequerido(string? valor, string campo)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException($"El campo '{campo}' es obligatorio.");

            return valor.Trim();
        }

        public static string CorreoValido(string? correo)
        {
            string valor = TextoRequerido(correo, "Correo");

            if (!valor.Contains('@') || !valor.Contains('.') || valor.StartsWith('@'))
                throw new ArgumentException("El correo no tiene un formato válido.");

            return valor;
        }

        public static DateTime FechaDeCitaValida(DateTime fechaHora)
        {
            if (fechaHora <= DateTime.Now)
                throw new ArgumentException("La fecha de la cita debe ser futura.");

            if (fechaHora.DayOfWeek == DayOfWeek.Sunday)
                throw new ArgumentException("La clínica no atiende los domingos.");

            if (fechaHora.Hour < HoraApertura || fechaHora.Hour >= HoraCierre)
                throw new ArgumentException($"El horario de atención es de {HoraApertura}:00 a {HoraCierre}:00.");

            return fechaHora;
        }

        public static int IdValido(int id, string campo)
        {
            if (id <= 0)
                throw new ArgumentException($"El campo '{campo}' debe ser un número mayor que cero.");

            return id;
        }
    }
}
