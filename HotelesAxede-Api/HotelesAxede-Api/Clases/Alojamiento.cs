using HotelesAxede_Api.Models;

namespace HotelesAxede_Api.Clases
{
    public class Alojamiento
    {

        public List<TpoAlojamiento> Listar(string sede)
        {
            using (HotelesContext context = new HotelesContext())
            {

                var datos = context.Sedes
                        .Where(e => e.Sede1.Equals(sede))
                        .Select(e => e.IdTipoAlojamineto)
                        .ToList();

                var alojamientos = context.TpoAlojamientos.Where(e => datos.Contains(e.Id)).ToList();
                return alojamientos;
            }
        }
    }
}
