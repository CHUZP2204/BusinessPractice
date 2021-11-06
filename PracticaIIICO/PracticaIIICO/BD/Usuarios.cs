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
    
    public partial class Usuarios
    {
        public Usuarios()
        {
            this.AjustesTbl = new HashSet<AjustesTbl>();
            this.CitasTbl = new HashSet<CitasTbl>();
            this.CotizacionTbl = new HashSet<CotizacionTbl>();
            this.EntradasTbl = new HashSet<EntradasTbl>();
            this.SalidasTbl = new HashSet<SalidasTbl>();
        }
    
        public int ID_Usuario { get; set; }
        public int ID_TipoUsuario { get; set; }
        public string Nombre_U { get; set; }
        public string Apellido1_U { get; set; }
        public string Apellido2_U { get; set; }
        public string Direccion_U { get; set; }
        public string Correo_U { get; set; }
        public string Telefono_U { get; set; }
        public string Clave_U { get; set; }
        public string Usuario_Ingreso { get; set; }
        public string Estado_U { get; set; }
        public Nullable<System.DateTime> Fecha_Creado { get; set; }
        public Nullable<System.DateTime> UltimaVezConectado { get; set; }
    
        public virtual ICollection<AjustesTbl> AjustesTbl { get; set; }
        public virtual ICollection<CitasTbl> CitasTbl { get; set; }
        public virtual ICollection<CotizacionTbl> CotizacionTbl { get; set; }
        public virtual ICollection<EntradasTbl> EntradasTbl { get; set; }
        public virtual ICollection<SalidasTbl> SalidasTbl { get; set; }
        public virtual TipoUsuario TipoUsuario { get; set; }
    }
}
