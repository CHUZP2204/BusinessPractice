using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PracticaIIICO.BD;
using PracticaIIICO.Models;

namespace PracticaIIICO.ViewModels
{
    public class ViewCotizacionModel : BaseModelo
    {
        public List<sp_Retorna_Cotizacion_Result> ListaCotizacion { get; set; }
    }
}