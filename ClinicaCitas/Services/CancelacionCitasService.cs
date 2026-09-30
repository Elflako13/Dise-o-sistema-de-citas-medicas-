using ClinicaCitas.Interfaces;
using ClinicaCitas.Models;

namespace ClinicaCitas.Services
{
   
    public class CancelacionCitasService : ICancelacionCitas
    {
        private readonly IConsultaCitas _consulta;

        public CancelacionCitasService(IConsultaCitas consulta)
        {
            _consulta = consulta;
        }

        public Cita Cancelar(int citaId, string motivo)
        {
            Cita cita = _consulta.ObtenerPorId(citaId);
            cita.Cancelar(motivo);

            return cita;
        }
    }
}
