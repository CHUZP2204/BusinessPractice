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
    
    public partial class sp_Retorna_Usuario_Result
    {
        public int ID_Usuario { get; set; }
        public int ID_TipoUsuario { get; set; }
        public string Nombre_U { get; set; }
        public string Apellido1_U { get; set; }
        public string Apellido2_U { get; set; }
        public string Direccion_U { get; set; }
        public string Correo_U { get; set; }
        public string Telefono_U { get; set; }
        public string Usuario_Ingreso { get; set; }
        public string Estado_U { get; set; }
        public Nullable<System.DateTime> Fecha_Creado { get; set; }
        public Nullable<System.DateTime> UltimaVezConectado { get; set; }
    }
}
