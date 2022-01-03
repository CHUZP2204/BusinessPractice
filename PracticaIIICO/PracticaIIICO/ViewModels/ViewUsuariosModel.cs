using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PracticaIIICO.BD;
using PracticaIIICO.Models;

namespace PracticaIIICO.ViewModels
{
    public class ViewUsuariosModel:BaseModelo
    {
        public List<sp_Retorna_Usuario_Result> ListaUsuarios { get; set; }
    }
}