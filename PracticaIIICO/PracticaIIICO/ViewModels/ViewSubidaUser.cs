using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PracticaIIICO.ViewModels
{
    public class ViewSubidaUser
    {
        [Required]
        [DisplayName("Usuario")]
        public int ID_Usuario { get; set; }

        [Required]
        [DisplayName("Mi Archivo")]
        public HttpPostedFileBase Archivo1 { get; set; }
        [Required]
        [DisplayName("Nombre Archivo")]
        public string CadenaNombre { get; set; }
        [Required]
        [DisplayName("DescripcionImg")]
        public string DescripcionImg { get; set; }
    }
}