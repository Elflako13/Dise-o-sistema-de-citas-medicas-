using ClinicaCitas.Validaciones;

namespace ClinicaCitas.Models
{
    public abstract class Persona : EntidadBase
    {
        public string Nombre { get; }
        public string Apellido { get; }
        public string Telefono { get; }
        public string Correo { get; }

        protected Persona(string nombre, string apellido, string telefono, string correo)
        {
            Nombre = Validador.TextoRequerido(nombre, "Nombre");
            Apellido = Validador.TextoRequerido(apellido, "Apellido");
            Telefono = Validador.TextoRequerido(telefono, "Teléfono");
            Correo = Validador.CorreoValido(correo);
        }

        public string NombreCompleto => $"{Nombre} {Apellido}";

        public override string ToString() => NombreCompleto;
    }
}
