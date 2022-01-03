using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PracticaIIICO.Models;
using PracticaIIICO.BD;

namespace PracticaIIICO.ViewModels
{
    public class ViewCategoriasModel : BaseModelo
    {
        public List<sp_Retorna_Categorias_Result> ListaCategorias { get; set; }
    }
}