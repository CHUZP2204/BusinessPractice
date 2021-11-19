using PracticaIIICO.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracticaIIICO.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeUser : AuthorizeAttribute
    {

        private sp_Retorna_UsuarioID_Result usuario;
        private MotoRepuestosMakoEntities ModeloBD = new MotoRepuestosMakoEntities();
        private string tipoUsuario = "";

        public AuthorizeUser(string tipoUsuarioObtenid = "")
        {
            this.tipoUsuario = tipoUsuarioObtenid;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //String nombreTipouser = "";
            //String nombreModulo = "";

            try
            {
                usuario = (sp_Retorna_UsuarioID_Result)HttpContext.Current.Session["UsuarioActual"];
                if (usuario != null)
                {


                    //this.BD.sp_Retorna_Usuarios_ID(usuario.ID_Usuario).ToList();

                    sp_Retorna_TipoUsuario_ID_Result tipoUsuarioActual = new sp_Retorna_TipoUsuario_ID_Result();
                    tipoUsuarioActual = this.ModeloBD.sp_Retorna_TipoUsuario_ID(usuario.ID_TipoUsuario).FirstOrDefault();


                    if (tipoUsuarioActual.Nombre_TipoUsuario.Equals("Empleado"))
                    {
                        filterContext.Result = new RedirectResult("~/Error/UnauthorizeOperation?tipoUsuario=" + tipoUsuario);
                    }
                }

            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Error/UnauthorizeOperation?tipoUsuario=" + tipoUsuario + ex);
            }


        }
    }
}