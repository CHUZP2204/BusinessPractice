using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PracticaIIICO.BD;
using PracticaIIICO.Models;

namespace PracticaIIICO.Controllers.Sesion
{
    public class SesionController : Controller
    {
        MotoRepuestosMakoEntities ModeloBD = new MotoRepuestosMakoEntities();
        // GET: Sesion
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Iniciar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Iniciar(SesionModel model)
        {
            sp_Verifica_Usuario_Result datosObtenidos = this.ModeloBD.sp_Verifica_Usuario(model.NombreUsuario, model.Clave).FirstOrDefault();
            string Mensaje = "";

            if (ModelState.IsValid)
            {
                if (datosObtenidos != null)
                {
                    sp_Retorna_UsuarioID_Result usuarioActual = new sp_Retorna_UsuarioID_Result();
                    usuarioActual = this.ModeloBD.sp_Retorna_UsuarioID(datosObtenidos.ID_Usuario, null, null).FirstOrDefault();
                    ////
                    ///
                    Session["UsuarioActual"] = usuarioActual;
                    //
                    Session["NombreUsuario"] = usuarioActual.Nombre_U;
                    Session["ApellidosUsuario"] = usuarioActual.Apellido1_U + " " + usuarioActual.Apellido2_U;
                    Session["RoleUsuario"] = usuarioActual.ID_TipoUsuario;
                    FormsAuthentication.SetAuthCookie(datosObtenidos.Nombre_U, true);
                    return this.RedirectToAction("Index", "Home");
                    //return this.RedirectToAction("Index", "tbl_Usuarios", new { Area = "Admin" });
                }

                else
                {
                    Mensaje = "Contraseña o Usuario Incorrecto";
                    ViewBag.Error = Mensaje;
                    //Session["Mensaje"] = Mensaje;
                    //return this.Redirect("~/Error/MessageError?error=" + Mensaje);

                }
            }
            else
            {
                Mensaje = "Ingresa Tu Clave y Contraseña Para Ingresar Al Sistema";
                /*Session["Mensaje"]*/
                ViewBag.Error = Mensaje;
            }


            return View();
        }

        // GET: Sesion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Sesion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sesion/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sesion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Sesion/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sesion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Sesion/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
