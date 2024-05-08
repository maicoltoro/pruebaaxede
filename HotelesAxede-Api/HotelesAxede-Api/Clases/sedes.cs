using HotelesAxede_Api.Models;

namespace HotelesAxede_Api.Clases
{
    public class sedes
    {
        public List<Sede> listar()
        {
            using (HotelesContext context = new HotelesContext())
            {
                var sedes = context.Sedes.ToList();
                var sedesUnicas = sedes
                       .GroupBy(s => s.Sede1)
                       .Select(g => g.First())
                       .ToList();
                return sedesUnicas;
            }
        }
    }
}
