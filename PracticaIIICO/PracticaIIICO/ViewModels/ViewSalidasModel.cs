using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PracticaIIICO.BD;
using PracticaIIICO.Models;

namespace PracticaIIICO.ViewModels
{
    public class ViewSalidasModel : BaseModelo
    {
        public List<sp_Retorna_SalidaID_Result> ListaSalidas { get; set; }
    }
}