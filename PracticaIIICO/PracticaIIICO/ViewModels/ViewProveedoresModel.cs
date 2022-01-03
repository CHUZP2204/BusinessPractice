using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PracticaIIICO.BD;
using PracticaIIICO.Models;

namespace PracticaIIICO.ViewModels
{
    public class ViewProveedoresModel : BaseModelo
    {
        public List<sp_Retorna_Proveedores_Result> ListaProveedores { get; set; }
    }
}