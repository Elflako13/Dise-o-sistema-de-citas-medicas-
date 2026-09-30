using ClinicaCitas.Validaciones;

namespace ClinicaCitas.Models
{
    public class Paciente : Persona
    {
        public string Cedula { get; }

        public Paciente(string nombre, string apellido, string cedula, string telefono, string correo)
            : base(nombre, apellido, telefono, correo)
        {
            Cedula = Validador.TextoRequerido(cedula, "Cédula");
        }

        public override string ToString() => $"{NombreCompleto} (Cédula: {Cedula})";
    }
}
