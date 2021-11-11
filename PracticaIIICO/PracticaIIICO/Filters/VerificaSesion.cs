using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaIIICO.BD;
using PracticaIIICO.Controllers.Sesion;

namespace PracticaIIICO.Filters
{
    public class VerificaSesion : ActionFilterAttribute
    {
        private sp_Retorna_UsuarioID_Result usuario;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            usuario = (sp_Retorna_UsuarioID_Result)HttpContext.Current.Session["UsuarioActual"];
            if (usuario == null)
            {
                if (filterContext.Controller is SesionController == false)
                {
                    filterContext.HttpContext.Response.Redirect("/Sesion/Iniciar");
                }
            }
        }
    }
}