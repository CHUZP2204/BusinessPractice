using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaIIICO.BD;

namespace PracticaIIICO.Filters
{
    public class Lock : ActionFilterAttribute
    {
        private sp_Retorna_UsuarioID_Result usuario;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            String estado = "";
            usuario = (sp_Retorna_UsuarioID_Result)HttpContext.Current.Session["UsuarioActual"];
            try
            {
                usuario = (sp_Retorna_UsuarioID_Result)HttpContext.Current.Session["UsuarioActual"];

                //this.BD.sp_Retorna_Usuarios_ID(usuario.ID_Usuario).ToList();
                //tipoUsuario = usuario.Tipo_Usuario;


                if (usuario != null)
                {
                    estado = usuario.Nombre_U + " Se Encuentra Conectado";
                    filterContext.Result = new RedirectResult("~/Error/Unauthorize?error=" + estado);
                }
            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Error/Unauthorize?error=" + ex);
            }
        }
    }
}