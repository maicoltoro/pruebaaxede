using System;
using System.Collections.Generic;

namespace HotelesAxede_Api.Models
{
    public partial class CantidadMaximaPersona
    {
        public int Id { get; set; }
        public int? IdSede { get; set; }
        public int? Cantidad { get; set; }
    }
}
