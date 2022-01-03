using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PracticaIIICO.BD;
using PracticaIIICO.Models;

namespace PracticaIIICO.ViewModels
{
    public class ViewEntradasModel : BaseModelo
    {
        public List<sp_Retorna_Entradas_Result> ListaEntradas { get; set; }
    }
}