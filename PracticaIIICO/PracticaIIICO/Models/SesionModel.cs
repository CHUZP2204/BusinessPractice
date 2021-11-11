using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PracticaIIICO.Models
{
    public class SesionModel
    {

        [Display(Name = "Usuario")]
        [Required]
        public string NombreUsuario { get; set; }

        [Display(Name = "Contraseña")]
        [Required]
        [DataType(DataType.Password)]
        public string Clave { get; set; }
    }
}