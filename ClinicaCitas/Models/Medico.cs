using ClinicaCitas.Validaciones;

namespace ClinicaCitas.Models
{
    public class Medico : Persona
    {
        public string Exequatur { get; }

        public Especialidad? Especialidad { get; private set; }

        public Medico(string nombre, string apellido, string exequatur, string telefono, string correo)
            : base(nombre, apellido, telefono, correo)
        {
            Exequatur = Validador.TextoRequerido(exequatur, "Exequátur");
        }

        public void AsignarEspecialidad(Especialidad especialidad)
        {
            Especialidad = especialidad ?? throw new ArgumentException("La especialidad es obligatoria.");
        }

        public string NombreEspecialidad => Especialidad?.Nombre ?? "Sin especialidad";

        public override string ToString() => $"Dr(a). {NombreCompleto} - {NombreEspecialidad}";
    }
}
