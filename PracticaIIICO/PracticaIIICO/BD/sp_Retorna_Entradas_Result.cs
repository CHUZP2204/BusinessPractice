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
    
    public partial class sp_Retorna_Entradas_Result
    {
        public int ID_Entrada { get; set; }
        public int ID_Proveedor { get; set; }
        public int ID_Usuario { get; set; }
        public int Numero_Factura { get; set; }
        public System.DateTime Fecha_Factura { get; set; }
        public System.DateTime Fecha_Registro { get; set; }
        public decimal Monto_Bruto { get; set; }
        public decimal Monto_Descuento { get; set; }
        public decimal Monto_IVA { get; set; }
        public decimal Monto_Final { get; set; }
    }
}