using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaIIICO.BD;
using PracticaIIICO.Filters;

namespace PracticaIIICO.Controllers.Usuarios
{

    public class UsuariosController : Controller
    {
        MotoRepuestosMakoEntities ModeloBD = new MotoRepuestosMakoEntities();
        // GET: Usuarios
        public ActionResult Index()
        {
            return View();
        }

        // GET: Usuarios/Details/5
        [AuthorizeUser]
        public ActionResult ListaUsuarios()
        {
            List<sp_Retorna_Usuario_Result> datosObtenidos = new List<sp_Retorna_Usuario_Result>();
            datosObtenidos = this.ModeloBD.sp_Retorna_Usuario(null, null).ToList();
            return View(datosObtenidos);
        }

        // GET: Usuarios/Create
        [AuthorizeUser]
        public ActionResult NuevoUsuario()
        {
            this.AgregTipoUsuariosViewBag();
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        public ActionResult NuevoUsuario(sp_Retorna_Usuario_Result collection)
        {
            int cantRegistroAfectado = 0;
            string resultado = "";
            string parteUnaNombre = "";
            string parteDosNombre = "";
            string usuarioFinal = "";

            string clavePorDefecto = "12345";

            string estadoUsuario = "ACTIVO";
            DateTime fechaCreado;
            DateTime fechaUltimaVes;

            ///Asignar fechas

            fechaCreado = DateTime.Now;
            fechaUltimaVes = DateTime.Now;

            ///Crear Un Usuario de Logueo a partir del nombre y apellido 
            parteUnaNombre = collection.Nombre_U.Substring(0, 3);
            parteDosNombre = collection.Apellido1_U.Substring(0, 3);
            ///Concatenar los datos obtnidos y unirlos
            ///Asi Se obtnedra el Nombre Usuario Final
            usuarioFinal = parteUnaNombre+parteDosNombre;


            try
            {
                cantRegistroAfectado = this.ModeloBD.sp_Inserta_Usuario(
                    collection.ID_TipoUsuario,
                    collection.Nombre_U,
                    collection.Apellido1_U,
                    collection.Apellido2_U,
                    collection.Direccion_U,
                    collection.Correo_U,
                    collection.Telefono_U,
                    clavePorDefecto,
                    usuarioFinal,
                    estadoUsuario,//dato en string
                    fechaCreado,
                    fechaUltimaVes
                    );

                return RedirectToAction("ListaUsuarios");
            }
            catch (Exception errorObtenido)
            {
                resultado = "Ocurrio Un Error: " + errorObtenido.Message;
            }
            finally
            {
                if (cantRegistroAfectado > 0)
                {
                    resultado = "Registro Insertado";
                }
                else
                {
                    resultado += "No se pudo Insertar";
                }
            }
            Response.Write("<script languaje=javascript>alert('" + resultado + "');</script>");
            this.AgregTipoUsuariosViewBag();
            return View();
        }

        // GET: Usuarios/Edit/5
        public ActionResult ModificaUsuario(int id_User)
        {
            sp_Retorna_UsuarioID_Result datosObtenidos = new sp_Retorna_UsuarioID_Result();
            datosObtenidos = this.ModeloBD.sp_Retorna_UsuarioID(id_User,null,null).FirstOrDefault();

            this.AgregTipoUsuariosViewBag();

            return View(datosObtenidos);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        public ActionResult ModificaUsuario(sp_Retorna_UsuarioID_Result collectionView)
        {
            sp_Retorna_UsuarioID_Result datosObtenidos = new sp_Retorna_UsuarioID_Result();
            datosObtenidos = this.ModeloBD.sp_Retorna_UsuarioID(collectionView.ID_Usuario, null, null).FirstOrDefault();
            /////
            int cantRegistroAfectado = 0;
            string resultado = "";

            DateTime fechaUltimaVes;

            ///Asignar fechas
            fechaUltimaVes = DateTime.Now;


            try
            {
                cantRegistroAfectado = this.ModeloBD.sp_Modifica_Usuario(
                    collectionView.ID_Usuario,
                    collectionView.ID_TipoUsuario,
                    collectionView.Nombre_U,
                    collectionView.Apellido1_U,
                    collectionView.Apellido2_U,
                    collectionView.Direccion_U,
                    collectionView.Correo_U,
                    collectionView.Telefono_U,
                    collectionView.Clave_U,
                    collectionView.Usuario_Ingreso,
                    datosObtenidos.Estado_U,//dato en string
                    datosObtenidos.Fecha_Creado,
                    fechaUltimaVes
                    );

                return RedirectToAction("ListaUsuarios");
            }
            catch (Exception errorObtenido)
            {
                resultado = "Ocurrio Un Error: " + errorObtenido.Message;
            }
            finally
            {
                if (cantRegistroAfectado > 0)
                {
                    resultado = "Registro Modificado";
                }
                else
                {
                    resultado += "No se pudo Modificar";
                }
            }
            Response.Write("<script languaje=javascript>alert('" + resultado + "');</script>");

            this.AgregTipoUsuariosViewBag();

            return View(collectionView);
        }


        //
        public ActionResult MostrarUsuario(int id_User)
        {
            sp_Retorna_UsuarioID_Result datosObtenidos = new sp_Retorna_UsuarioID_Result();
            datosObtenidos = this.ModeloBD.sp_Retorna_UsuarioID(id_User, null, null).FirstOrDefault();

            this.AgregTipoUsuariosViewBag();

            return View(datosObtenidos);
            
        }
        // GET: Usuarios/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuarios/Delete/5
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

        /// <summary>
        /// Agregar Tipos De Roles
        /// </summary>
        void AgregTipoUsuariosViewBag()
        {
            this.ViewBag.ListaTipoUsuarios = this.ModeloBD.sp_Retorna_TipoUsuario(null).ToList();
        }
    }
}
