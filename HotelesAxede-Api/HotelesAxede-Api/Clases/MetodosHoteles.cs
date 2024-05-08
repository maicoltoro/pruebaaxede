using HotelesAxede_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelesAxede_Api.Clases
{
    public class MetodosHoteles
    {
        public List<SpSolicitudes> solicitudes()
        {
            using (HotelesContext context = new HotelesContext())
            {
                var data = context.Sp_Solicitude.FromSqlInterpolated($"EXEC ListarSolicitudes").ToList();
                return data;
            }
        }

        public bool deleteUser(int id)
        {
            using (HotelesContext context = new HotelesContext())
            {
                var userXId = context.Solicitudes.Where(e => e.Id == id).FirstOrDefault();
                if (userXId != null)
                {
                    context.Solicitudes.Remove(userXId);
                    context.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }

        public bool createUser(Solicitude data)
        {
            using (HotelesContext context = new HotelesContext())
            {
                context.Solicitudes.Add(data);
                context.SaveChanges();
                return true;
            }
        }
    }
}
