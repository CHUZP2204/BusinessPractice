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
    
    public partial class ProductsImagesView
    {
        public int ID_Producto { get; set; }
        public int ID_Imagen { get; set; }
        public int ID_Categoria { get; set; }
        public string Nombre_PROD { get; set; }
        public decimal Precio_PROD { get; set; }
        public string Descripcion_PROD { get; set; }
        public bool Estado_PROD { get; set; }
        public Nullable<int> Cantidad_PROD { get; set; }
        public string Url_Imagen { get; set; }
        public string Descripcion_Imagen { get; set; }
    }
}
