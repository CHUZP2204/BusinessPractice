using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PracticaIIICO.BD;
using PracticaIIICO.Filters;
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

        [LockInicioSesion]
        public ActionResult Iniciar()
        {
            return View();
        }

        [LockInicioSesion]
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
                    sp_Retorna_TipoUsuario_ID_Result tipoUsuarioActual = new sp_Retorna_TipoUsuario_ID_Result();
                    tipoUsuarioActual = this.ModeloBD.sp_Retorna_TipoUsuario_ID(usuarioActual.ID_TipoUsuario).FirstOrDefault();
                    ///
                    Session["UsuarioActual"] = usuarioActual;
                    //
                    Session["NombreUsuario"] = usuarioActual.Nombre_U;
                    Session["ApellidosUsuario"] = usuarioActual.Apellido1_U + " " + usuarioActual.Apellido2_U;
                    Session["IdUsuario"] = usuarioActual.ID_Usuario;
                    Session["RoleUsuario"] = tipoUsuarioActual.Nombre_TipoUsuario;
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

        [HttpGet]
        public ActionResult _ModalMensaje(string mensaje)
        {
            var model = mensaje; // do whatever you need to get your model 
            return PartialView(model);
        }


        public ActionResult Salir()
        {
            Session["NombreUsuario"] = "";
            Session["ApellidosUsuario"] = "";
            Session["RoleUsuario"] = "";
            Session["IdUsuario"] = "";
            Session["UsuarioActual"] = null;

            FormsAuthentication.SignOut();
            return this.RedirectToAction("Iniciar");
        }


        //Registrar Usuario Desde Login
        [HttpPost]
        public ActionResult RegistrarUsuario(
            string pNombreC,
            string pApellido1,
            string pApellido2,
            string pCorreo,
            string pContrasenia)
        {
            ///Variable Que Registra La Cantidad De Registros Afectados
            ///Si Un Procedimiento Que Ejecuta Insert, Update o Delete
            ///No Afecta Registros Implica Que Hubo Un Error
            int cantRegistrosAfectados = 0;
            string resultado = "";

            string parteUnaNombre = "";
            string parteDosNombre = "";
            string usuarioFinal = "";
            ///Crear Un Usuario de Logueo a partir del nombre y apellido 
            parteUnaNombre = pNombreC.Substring(0, 3);
            parteDosNombre = pApellido1.Substring(0, 3);
            ///Concatenar los datos obtnidos y unirlos
            ///Asi Se obtnedra el Nombre Usuario Final
            usuarioFinal = parteUnaNombre + parteDosNombre;

            ViewBag.NombreUsuario = usuarioFinal;

            int idTipoUsuario = 2; //2 por defecto Va Ser Cliente
            string direccion = "";
            string pTelefono = "";
            string estadoUsuario = "ACTIVO";

            ////
            DateTime fechaCreado;
            DateTime fechaUltimaVes;

            ///Asignar fechas

            fechaCreado = DateTime.Now;
            fechaUltimaVes = DateTime.Now;
            
            try
            {

                cantRegistrosAfectados = this.ModeloBD.sp_Inserta_Usuario(
                        idTipoUsuario,
                        pNombreC,
                        pApellido1,
                        pApellido2,
                        direccion,
                        pCorreo,
                        pTelefono,
                        pContrasenia,
                        usuarioFinal,
                        estadoUsuario,
                        fechaCreado,
                        fechaUltimaVes
                  );



            }
            catch (Exception error)
            {
                resultado = "Ocurrio Un Error" + error.Message;
            }
            finally
            {
                if (cantRegistrosAfectados > 0)
                {
                    resultado = "El Usuario Se Regitro Exitosamente, Su Usuario Es:"+usuarioFinal;
                }
                else
                {
                    resultado += " No Se Pudo Registrar";
                }
            }

            return Json(resultado);
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
