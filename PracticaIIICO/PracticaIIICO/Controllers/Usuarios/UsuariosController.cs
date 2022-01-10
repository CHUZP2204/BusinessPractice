using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaIIICO.BD;
using PracticaIIICO.Filters;
using PracticaIIICO.ViewModels;

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

        public ActionResult ListaNueva()
        {
            List<sp_Retorna_Usuario_Result> datosObtenidos = new List<sp_Retorna_Usuario_Result>();
            datosObtenidos = this.ModeloBD.sp_Retorna_Usuario(null, null).ToList();

            this.AgregTipoUsuariosViewBag();
            return View(datosObtenidos);
        }

        // GET: Usuarios/Details/5
        [AuthorizeUser]
        public ActionResult ListaUsuarios(int pagina = 1)
        {
            List<sp_Retorna_Usuario_Result> datosObtenidos = new List<sp_Retorna_Usuario_Result>();
            datosObtenidos = this.ModeloBD.sp_Retorna_Usuario(null, null).ToList();

            this.AgregTipoUsuariosViewBag();

            //Con Paginador
            var cantidadRegistroPorPagina = 5; //parametro Para limitar la cantidad de elementos al mostrar
            using (var db = new MotoRepuestosMakoEntities())
            {
                var usuariosObtenidos = db.sp_Retorna_Usuario(null, null).Skip((pagina - 1) * cantidadRegistroPorPagina)
                    .Take(cantidadRegistroPorPagina).ToList();

                var totalRegistros = db.sp_Retorna_Usuario(null, null).Count();

                var modelo = new ViewUsuariosModel();

                modelo.ListaUsuarios = usuariosObtenidos;
                modelo.PaginaActual = pagina;
                modelo.TotalRegistros = totalRegistros;
                modelo.RegistrosPorPagina = cantidadRegistroPorPagina;

                return View(modelo);
            }
            //return View(datosObtenidos);
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
                    fechaUltimaVes,
                    collection.Cedula_U
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


            if (TempData["MessageUSER"] != null)
            {
                ViewBag.MessageUSER = TempData["MessageUSER"].ToString();
            }

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
                    fechaUltimaVes,
                    collectionView.Cedula_U
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

        //Registrar Usuario Por Medio De Un modal
        [HttpPost]
        public ActionResult RegistrarUsuarioModal(
            string pNombreC,
            string pApellido1,
            string pApellido2,
            string pCorreo,
            string pContrasenia,
            string pCedula)
        {
            ///Variable Que Registra La Cantidad De Registros Afectados
            ///Si Un Procedimiento Que Ejecuta Insert, Update o Delete
            ///No Afecta Registros Implica Que Hubo Un Error
            int cantRegistrosAfectados = 0;

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

            //Mensajes para JSON

            String MensajeFinal = "";
            int estadoRegistro = 0;
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
                        fechaUltimaVes,
                        pCedula
                  );



            }
            catch (Exception error)
            {
                MensajeFinal = "Ocurrio Un Error" + error.Message;
            }
            finally
            {
                if (cantRegistrosAfectados > 0)
                {
                    MensajeFinal = "El Usuario Se Regitro Exitosamente, Su Usuario Es:" + usuarioFinal;
                    estadoRegistro = 1;
                }
                else
                {
                    MensajeFinal += " No Se Pudo Registrar";
                    estadoRegistro = 0;
                }
            }

            //return Json(resultado);

            return Json(new
            {
                resultado = MensajeFinal,
                estado = estadoRegistro
            });
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
