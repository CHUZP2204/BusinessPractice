using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PracticaIIICO.Models
{
    [Table("ProductosTbl")]
    public class Productos
    {
        [Key]
        public int Posicion { get; set; }
        public string Nombre { get; set; }

        public decimal Precio { get; set; }

    }
}