using System;
using System.Collections.Generic;

namespace HotelesAxede_Api.Models
{
    public partial class TpoAlojamiento
    {
        public TpoAlojamiento()
        {
            Sedes = new HashSet<Sede>();
        }

        public int Id { get; set; }
        public string? Alojamiento { get; set; }

        public virtual ICollection<Sede> Sedes { get; set; }
    }
}
