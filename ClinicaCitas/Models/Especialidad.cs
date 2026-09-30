using ClinicaCitas.Validaciones;

namespace ClinicaCitas.Models
{
    public class Especialidad : EntidadBase
    {
        public string Nombre { get; }
        public string Descripcion { get; }

        public Especialidad(string nombre, string descripcion)
        {
            Nombre = Validador.TextoRequerido(nombre, "Nombre de la especialidad");
            Descripcion = descripcion?.Trim() ?? string.Empty;
        }

        public override string ToString() => Nombre;
    }
}
