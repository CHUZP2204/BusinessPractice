//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PracticaIIICO.BD
{
    using System;
    using System.Collections.Generic;
    
    public partial class MarcaTbl
    {
        public MarcaTbl()
        {
            this.CitasTbl = new HashSet<CitasTbl>();
        }
    
        public int ID_Marca { get; set; }
        public string Nombre_Marca { get; set; }
        public string Descripcion_Marca { get; set; }
    
        public virtual ICollection<CitasTbl> CitasTbl { get; set; }
    }
}
