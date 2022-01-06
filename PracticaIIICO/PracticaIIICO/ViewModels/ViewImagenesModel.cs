using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PracticaIIICO.BD;
using PracticaIIICO.Models;

namespace PracticaIIICO.ViewModels
{
    public class ViewImagenesModel:BaseModelo
    {
        public List<sp_Retorna_ProductsImages_Result> ModeloImagensPROD { get; set; }
    }
}