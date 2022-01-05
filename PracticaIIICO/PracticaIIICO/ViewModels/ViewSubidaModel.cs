using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PracticaIIICO.ViewModels
{
    public class ViewSubidaModel
    {
        [Required]
        [DisplayName("Producto")]
        public int ID_Producto { get; set; }

        [Required]
        [DisplayName("Mi Archivo")]
        public HttpPostedFileBase Archivo1 { get; set; }
        [Required]
        [DisplayName("Nombre Producto")]
        public string CadenaNombre { get; set; }
        [Required]
        [DisplayName("DescripcionImg")]
        public string DescripcionImg { get; set; }

    }
}