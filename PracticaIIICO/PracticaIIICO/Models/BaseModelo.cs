using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracticaIIICO.Models
{
    public class BaseModelo
    {
        public int PaginaActual { get; set; }
        public int TotalRegistros { get; set; }
        public int RegistrosPorPagina { get; set; }
    }
}