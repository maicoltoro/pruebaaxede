using System;
using System.Collections.Generic;

namespace HotelesAxede_Api.Models
{
    public partial class Sede
    {
        public Sede()
        {
            Solicitudes = new HashSet<Solicitude>();
        }

        public int Id { get; set; }
        public string Sede1 { get; set; } = null!;
        public int IdTipoAlojamineto { get; set; }
        public int? CantidadHabitaciones { get; set; }
        public int? CantidadPersonas { get; set; }

        public virtual TpoAlojamiento IdTipoAlojaminetoNavigation { get; set; } = null!;
        public virtual ICollection<Solicitude> Solicitudes { get; set; }
    }
}
