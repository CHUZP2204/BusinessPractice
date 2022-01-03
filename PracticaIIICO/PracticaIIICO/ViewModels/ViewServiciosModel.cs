using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PracticaIIICO.BD;
using PracticaIIICO.Models;

namespace PracticaIIICO.ViewModels
{
    public class ViewServiciosModel: BaseModelo
    {
        public List<sp_Retorna_Servicio_Result> ListaServicios { get; set; }
    }
}