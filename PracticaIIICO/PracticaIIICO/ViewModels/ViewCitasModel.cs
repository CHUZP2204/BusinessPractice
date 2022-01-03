using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PracticaIIICO.BD;
using PracticaIIICO.ViewModels;
using PracticaIIICO.Models;

namespace PracticaIIICO.ViewModels
{
    public class ViewCitasModel : BaseModelo
    {
        public List<sp_Retorna_Citas_Result> ListaCitas { get; set; }
    }
}