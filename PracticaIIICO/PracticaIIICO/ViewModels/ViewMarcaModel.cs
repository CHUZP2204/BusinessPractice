using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PracticaIIICO.BD;
using PracticaIIICO.Models;

namespace PracticaIIICO.ViewModels
{
    public class ViewMarcaModel : BaseModelo
    {
        public List<sp_Retorna_Marcas_Result> ListaMarcas { get; set; }
    }
}