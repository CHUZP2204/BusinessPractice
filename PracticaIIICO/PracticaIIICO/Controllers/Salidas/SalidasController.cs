using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaIIICO.BD;
using PracticaIIICO.ViewModels;

namespace PracticaIIICO.Controllers.Salidas
{
    public class SalidasController : Controller
    {
        MotoRepuestosMakoEntities ModeloBD = new MotoRepuestosMakoEntities();
        // GET: Salidas
        public ActionResult Index()
        {
            return View();
        }

        // GET: Salidas/Details/5
        public ActionResult ListaSalidas(int pagina = 1)
        {
            List<sp_Retorna_SalidaID_Result> modeloVista = new List<sp_Retorna_SalidaID_Result>();
            modeloVista = this.ModeloBD.sp_Retorna_SalidaID(null).ToList();

            this.agregaUsuarios();

            //Con Paginador
            var cantidadRegistroPorPagina = 5; //parametro Para limitar la cantidad de elementos al mostrar
            using (var db = new MotoRepuestosMakoEntities())
            {
                var salidasObtenidos = db.sp_Retorna_SalidaID(null).Skip((pagina - 1) * cantidadRegistroPorPagina)
                    .Take(cantidadRegistroPorPagina).ToList();

                var totalRegistros = db.sp_Retorna_SalidaID(null).Count();

                var modelo = new ViewSalidasModel();

                modelo.ListaSalidas = salidasObtenidos;
                modelo.PaginaActual = pagina;
                modelo.TotalRegistros = totalRegistros;
                modelo.RegistrosPorPagina = cantidadRegistroPorPagina;

                return View(modelo);
            }
            //Usar Sin Paginador

            //return View(modeloVista);
        }

        // GET: Salidas/Create
        public ActionResult NuevaSalida()
        {
            string tipoUsuarioActual = "";

            if (Session["RoleUsuario"] != null)
            {
                tipoUsuarioActual = Session["RoleUsuario"].ToString();
            }
            else
            {
                tipoUsuarioActual = "Empleado";
            }


            this.ViewBag.UsuarioActual = tipoUsuarioActual;

            this.agregaUsuariosID();
            this.agregaUsuarios();
            return View();
        }

        // POST: Salidas/Create
        [HttpPost]
        public ActionResult NuevaSalida(sp_Retorna_SalidaID_Result vistaObtenida)
        {
            int cantRegistroAfectado = 0;
            string resultado = "";
            DateTime fechaIngreso;

            
            fechaIngreso = DateTime.Now;

            try
            {
                cantRegistroAfectado = this.ModeloBD.sp_Inserta_Salida(
                    vistaObtenida.ID_Usuario,
                    fechaIngreso,
                    vistaObtenida.Hora_Orden
                    );

                return RedirectToAction("ListaSalidas");
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
            
            this.agregaUsuarios();

            return View();
        }

        // GET: Salidas/Edit/5
        public ActionResult ModificaSalida(int id_Sal)
        {
            sp_Retorna_SalidaID_Result modeloObtenido = new sp_Retorna_SalidaID_Result();
            modeloObtenido = this.ModeloBD.sp_Retorna_SalidaID(id_Sal).FirstOrDefault();

            string tipoUsuarioActual = "";

            if (Session["RoleUsuario"] != null)
            {
                tipoUsuarioActual = Session["RoleUsuario"].ToString();
            }
            else
            {
                tipoUsuarioActual = "Empleado";
            }

            this.agregaUsuariosID();
            this.agregaUsuarios();
            return View(modeloObtenido);
        }

        // POST: Salidas/Edit/5
        [HttpPost]
        public ActionResult ModificaSalida(sp_Retorna_SalidaID_Result collection)
        {
            int cantRegistroAfectado = 0;
            string resultado = "";
            DateTime fechaIngreso;


            fechaIngreso = DateTime.Now;

            try
            {
                cantRegistroAfectado = this.ModeloBD.sp_Modifica_Salida(
                    collection.ID_Salida,
                    collection.ID_Usuario,
                    collection.Fecha_Registro,
                    collection.Hora_Orden
                    );

                return RedirectToAction("ListaSalidas");
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

            this.agregaUsuarios();
            return View();
        }

        public ActionResult MostrarSalida(int id_Sal)
        {
            ///Obtener Listado De Entrada Con Detalle
            List<sp_Retorna_DesgloseSalida_Result> collectionObtenido = new List<sp_Retorna_DesgloseSalida_Result>();
            collectionObtenido = this.ModeloBD.sp_Retorna_DesgloseSalida(id_Sal).ToList();
            ///

            string Mensaje = id_Sal.ToString();
            /*Session["Mensaje"]*/
            ViewBag.NumeroSalida = Mensaje;

            this.agregaSalidas(id_Sal);
            //this.agregaProveedores();
            this.agregaUsuarios();

            return View(collectionObtenido);
        }

        // GET: Salidas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Salidas/Delete/5
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
        void agregaSalidas(int idSal)
        {
            this.ViewBag.ListaSalidas = this.ModeloBD.sp_Retorna_SalidaID(idSal).ToList();
        }
        void agregaUsuarios()
        {
            this.ViewBag.ListaUsuarios = this.ModeloBD.sp_Retorna_Usuario(null, null).ToList();
        }
        void agregaUsuariosID()
        {
            string idConvertido = Session["IdUsuario"].ToString();
            int idUsuarioActual = int.Parse(idConvertido);
            this.ViewBag.ListaUsuariosID = this.ModeloBD.sp_Retorna_UsuarioID(idUsuarioActual,null, null).ToList();
        }
    }
}
