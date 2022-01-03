using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PracticaIIICO.BD;
using PracticaIIICO.Models;

namespace PracticaIIICO.ViewModels
{
    public class ViewTiposervicioModel : BaseModelo
    {
        public List<sp_Retorna_Tipo_Servicio_Result> ListaTipoServicio { get; set; }
    }
}