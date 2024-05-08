using System;
using System.Collections.Generic;

namespace HotelesAxede_Api.Models
{
    public partial class Temporadum
    {
        public Temporadum()
        {
            Solicitudes = new HashSet<Solicitude>();
        }

        public int Id { get; set; }
        public string? Temporada { get; set; }

        public virtual ICollection<Solicitude> Solicitudes { get; set; }
    }
}
