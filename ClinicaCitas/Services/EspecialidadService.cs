using ClinicaCitas.Interfaces;
using ClinicaCitas.Models;

namespace ClinicaCitas.Services
{
    public class EspecialidadService : IRegistroEspecialidades
    {
        private readonly IRepositorio<Especialidad> _repositorio;

        public EspecialidadService(IRepositorio<Especialidad> repositorio)
        {
            _repositorio = repositorio;
        }

        public Especialidad Registrar(string nombre, string descripcion)
        {
            var especialidad = new Especialidad(nombre, descripcion);

            bool repetida = _repositorio.ObtenerTodos()
                .Any(e => e.Nombre.Equals(especialidad.Nombre, StringComparison.OrdinalIgnoreCase));

            if (repetida)
                throw new InvalidOperationException($"La especialidad '{especialidad.Nombre}' ya está registrada.");

            _repositorio.Agregar(especialidad);
            return especialidad;
        }

        public Especialidad ObtenerPorId(int id) =>
            _repositorio.ObtenerPorId(id)
            ?? throw new InvalidOperationException($"No existe una especialidad con el código {id}.");

        public IReadOnlyList<Especialidad> ObtenerTodas() => _repositorio.ObtenerTodos();
    }
}
