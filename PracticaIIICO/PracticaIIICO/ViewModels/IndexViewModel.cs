using PracticaIIICO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PracticaIIICO.BD;

namespace PracticaIIICO.ViewModels
{
    public class IndexViewModel : BaseModelo
    {
        public List<sp_Retorna_Productos_Result> Productos { get; set; }
    }
}