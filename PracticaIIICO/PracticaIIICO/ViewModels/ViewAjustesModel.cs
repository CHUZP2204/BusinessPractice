using PracticaIIICO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PracticaIIICO.BD;

namespace PracticaIIICO.ViewModels
{
    public class ViewAjustesModel : BaseModelo
    {
        public List<sp_Retorna_Ajustes_Result> ListaAjustes { get; set; }
    }
}