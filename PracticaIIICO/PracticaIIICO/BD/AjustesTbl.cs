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
    
    public partial class AjustesTbl
    {
        public int ID_Ajuste { get; set; }
        public int ID_Producto { get; set; }
        public int ID_Usuario { get; set; }
        public string Tipo_Ajuste { get; set; }
        public int Cantida_Ajustar { get; set; }
        public System.DateTime Fecha_Ajuste { get; set; }
        public string Descripcion_Ajsute { get; set; }
    
        public virtual Usuarios Usuarios { get; set; }
        public virtual ProductosTbl ProductosTbl { get; set; }
    }
}
