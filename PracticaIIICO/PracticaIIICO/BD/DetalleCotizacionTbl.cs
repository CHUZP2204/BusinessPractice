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
    
    public partial class DetalleCotizacionTbl
    {
        public int ID_DetalleCotizar { get; set; }
        public int ID_Cotizacion { get; set; }
        public int ID_Producto { get; set; }
        public int ID_Servicio { get; set; }
        public int Cant_AdquiPROD { get; set; }
        public decimal PrecioXCant { get; set; }
    
        public virtual CotizacionTbl CotizacionTbl { get; set; }
        public virtual ServiciosTbl ServiciosTbl { get; set; }
        public virtual ProductosTbl ProductosTbl { get; set; }
    }
}
