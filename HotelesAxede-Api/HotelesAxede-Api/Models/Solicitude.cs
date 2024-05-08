using System;
using System.Collections.Generic;

namespace HotelesAxede_Api.Models
{
    public partial class Solicitude
    {
        public int Id { get; set; }
        public int? IdSede { get; set; }
        public int? IdAlojamiento { get; set; }
        public int? IdTemporada { get; set; }
        public int? CantidadHabitaciones { get; set; }
        public int? CantidadPersonas { get; set; }

        public virtual Sede? IdSedeNavigation { get; set; }
        public virtual Temporadum? IdTemporadaNavigation { get; set; }
    }
}
