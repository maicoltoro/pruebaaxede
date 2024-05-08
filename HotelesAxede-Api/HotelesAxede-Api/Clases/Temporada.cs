using HotelesAxede_Api.Models;

namespace HotelesAxede_Api.Clases
{
    public class Temporada
    {
        public List<Temporadum> Listar()
        {
            using (HotelesContext context = new HotelesContext())
            {
                return context.Temporada.ToList();
            }
        }
    }
}
