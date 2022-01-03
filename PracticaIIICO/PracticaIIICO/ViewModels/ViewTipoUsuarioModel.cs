using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PracticaIIICO.BD;
using PracticaIIICO.Models;

namespace PracticaIIICO.ViewModels
{
    public class ViewTipoUsuarioModel: BaseModelo
    {
        public List<sp_Retorna_TipoUsuario_Result> ListaTipoUsuario { get; set; }
    }
}