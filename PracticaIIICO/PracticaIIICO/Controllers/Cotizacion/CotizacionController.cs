using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaIIICO.BD;
using PracticaIIICO.ViewModels;

namespace PracticaIIICO.Controllers.Cotizacion
{
    public class CotizacionController : Controller
    {
        MotoRepuestosMakoEntities ModeloBD = new MotoRepuestosMakoEntities();
        // GET: Cotizacion
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult index2()
        {
            List<sp_Retorna_FacturaID_Result> vistaObtenida = new List<sp_Retorna_FacturaID_Result>();
            vistaObtenida = this.ModeloBD.sp_Retorna_FacturaID(3).ToList();

            string Mensaje = "5";
            /*Session["Mensaje"]*/
            ViewBag.NumeroFactura = Mensaje;

            this.agregaUsuarios();
            this.agregaProductos();
            this.agregaEntradaMontos(3);

            return View(vistaObtenida);
            
        }

       
        // GET: Cotizacion/Details/5
        public ActionResult ListaCotizacion(int pagina = 1)
        {
            List<sp_Retorna_Cotizacion_Result> vistaObtenida = new List<sp_Retorna_Cotizacion_Result>();
            vistaObtenida = this.ModeloBD.sp_Retorna_Cotizacion(null, null).ToList();

            this.agregaUsuarios();
            this.agregaUsuariosID();

            //Con Paginador
            var cantidadRegistroPorPagina = 5; //parametro Para limitar la cantidad de elementos al mostrar
            using (var db = new MotoRepuestosMakoEntities())
            {
                var cotizacionesObtenidas = db.sp_Retorna_Cotizacion(null, null).Skip((pagina - 1) * cantidadRegistroPorPagina)
                    .Take(cantidadRegistroPorPagina).ToList();

                var totalRegistros = db.sp_Retorna_Cotizacion(null, null).Count();

                var modelo = new ViewCotizacionModel();

                modelo.ListaCotizacion = cotizacionesObtenidas;
                modelo.PaginaActual = pagina;
                modelo.TotalRegistros = totalRegistros;
                modelo.RegistrosPorPagina = cantidadRegistroPorPagina;

                return View(modelo);
            }
            //

            //Usar sin paginador
            //return View(vistaObtenida);
        }

        // GET: Cotizacion/Create
        public ActionResult NuevaCotizacion()
        {
            string tipoUsuarioActual = "";

            if (Session["RoleUsuario"] != null)
            {
                 tipoUsuarioActual= Session["RoleUsuario"].ToString();
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

        // POST: Cotizacion/Create
        [HttpPost]
        public ActionResult NuevaCotizacion(sp_Retorna_Cotizacion_Result collection)
        {
            int cantRegistroAfectado = 0;
            string resultado = "";
            DateTime fechaIngreso;
            string HoraIngreso;
            TimeSpan HoraIngresoConvertida;


            fechaIngreso = DateTime.Now;
            HoraIngreso = DateTime.Now.ToString("hh:mm:ss");

            HoraIngresoConvertida =  TimeSpan.Parse(HoraIngreso);


            try
            {
                cantRegistroAfectado = this.ModeloBD.sp_Inserta_Cotizacion(
                    collection.ID_Usuario,
                    collection.Numero_Cotizacion,
                    collection.Nombre_Cliente,
                    collection.Telefono_Cliente,
                    collection.Correo_Cliente,
                    fechaIngreso,
                    HoraIngresoConvertida,
                    collection.Costo
                    );

                return RedirectToAction("ListaCotizacion");
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

            this.agregaUsuariosID();
            this.agregaUsuarios();
            return View();

        }

        // Registro Cotizacion Modal
        [HttpPost]
        public ActionResult NuevaCotizacionModal(
            int pIdUsuario,
            int pNumeroCotizacion,
            string pNombreCliente,
            string pTelefonoCliente,
            string pCorreoCliente,
            TimeSpan pHoraCot,
            decimal pCosto)
        {

            

            int cantRegistroAfectado = 0;
            string resultado = "";
            DateTime fechaIngreso;

            String MensajeFinal = "";
            int estadoRegistro = 0;


            fechaIngreso = DateTime.Now;

            try
            {
                cantRegistroAfectado = this.ModeloBD.sp_Inserta_Cotizacion(
                    pIdUsuario,
                    pNumeroCotizacion,
                    pNombreCliente,
                    pTelefonoCliente,
                    pCorreoCliente,
                    fechaIngreso,
                    pHoraCot,
                    pCosto
                    );

               
            }
            catch (Exception error)
            {
                MensajeFinal = "Ocurrio Un Error" + error.Message;
            }
            finally
            {
                if (cantRegistroAfectado > 0)
                {
                    MensajeFinal = "La Cotizacion Se Regitro Exitosamente!";
                    estadoRegistro = 1;
                }
                else
                {
                    MensajeFinal += " No Se Pudo Registrar La Cotizacion";
                    estadoRegistro = 0;
                }
            }

            return Json(new
            {
                resultado = MensajeFinal,
                estado = estadoRegistro
            });

        }

        // GET: Cotizacion/Edit/5
        public ActionResult ModificaCotizacion(int id_Cot)
        {
            sp_Retorna_CotizacionID_Result modeloVista = new sp_Retorna_CotizacionID_Result();
            modeloVista = this.ModeloBD.sp_Retorna_CotizacionID(id_Cot).FirstOrDefault();

            this.agregaUsuarios();

            return View(modeloVista);
        }

        // POST: Cotizacion/Edit/5
        [HttpPost]
        public ActionResult ModificaCotizacion(sp_Retorna_Cotizacion_Result collection)
        {
            int cantRegistroAfectado = 0;
            string resultado = "";
            DateTime fechaIngreso;


            fechaIngreso = DateTime.Now;

            try
            {
                cantRegistroAfectado = this.ModeloBD.sp_Modifica_Cotizacion(
                    collection.ID_Cotizacion,
                    collection.ID_Usuario,
                    collection.Numero_Cotizacion,
                    collection.Nombre_Cliente,
                    collection.Telefono_Cliente,
                    collection.Correo_Cliente,
                    collection.Fecha_Cotizacion,
                    collection.Hora_Cotizacion,
                    collection.Costo
                    );

                return RedirectToAction("ListaCotizacion");
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

        public ActionResult MostrarCotizacion(int id_Cot)
        {
            List<sp_Retorna_DesgloseCotizacion_Result> modeloVista = new List<sp_Retorna_DesgloseCotizacion_Result>();
            modeloVista = this.ModeloBD.sp_Retorna_DesgloseCotizacion(id_Cot).ToList();

            string Mensaje = id_Cot.ToString();
            /*Session["Mensaje"]*/
            ViewBag.NumeroCotizacion = Mensaje;

            this.agregaCotizaciones(id_Cot);
            this.agregaProductos();
            this.agregaServicios();
            this.agregaUsuarios();

            return View(modeloVista);
        }

        // GET: Cotizacion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cotizacion/Delete/5
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

        void agregaUsuarios()
        {
            this.ViewBag.ListaUsuarios = this.ModeloBD.sp_Retorna_Usuario(null, null).ToList();
        }

        void agregaUsuariosID()

        {
            if (Session["IdUsuario"] != null)
            {
                string idConvertido = Session["IdUsuario"].ToString();

                int idUsuarioActual = int.Parse(idConvertido);
                this.ViewBag.ListaUsuariosID = this.ModeloBD.sp_Retorna_UsuarioID(idUsuarioActual, null, null).ToList();
            }
            else
            {
                this.ViewBag.ListaUsuariosID = this.ModeloBD.sp_Retorna_UsuarioID(null, null, null).ToList();
            }

        }
        void agregaCotizaciones(int idCot)
        {
            this.ViewBag.ListaCotizaciones = this.ModeloBD.sp_Retorna_CotizacionID(idCot).ToList();
        }

        void agregaProductos()
        {
            this.ViewBag.ListaProductos = this.ModeloBD.sp_Retorna_Productos(null, null).ToList();
        }

        void agregaServicios()
        {
            this.ViewBag.ListaServicios = this.ModeloBD.sp_Retorna_Servicio(null).ToList();
        }
        void agregaEntradaMontos(int idEntrada)
        {
            this.ViewBag.ListaEntradas = this.ModeloBD.sp_Retorna_EntradaID(idEntrada).ToList();
        }
    }
}
