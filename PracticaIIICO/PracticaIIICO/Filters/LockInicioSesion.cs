using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaIIICO.BD;

namespace PracticaIIICO.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class LockInicioSesion : AuthorizeAttribute
    {
        private sp_Retorna_UsuarioID_Result usuario;
        private MotoRepuestosMakoEntities BD = new MotoRepuestosMakoEntities();
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            String tipoUsuario = "";
            String estado = "";

            try
            {
                usuario = (sp_Retorna_UsuarioID_Result)HttpContext.Current.Session["UsuarioActual"];

                //this.BD.sp_Retorna_Usuarios_ID(usuario.ID_Usuario).ToList();
                //tipoUsuario = usuario.Tipo_Usuario;


                if (usuario != null)
                {
                    estado = usuario.Nombre_U + " Se Encuentra Conectado";
                    filterContext.Result = new RedirectResult("~/Error/Unauthorize?error=" + estado);
                    //filterContext.HttpContext.Response.Redirect("/Home/Index");
                }
            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Error/Unauthorize?error=" + ex);
            }


        }
    }
}