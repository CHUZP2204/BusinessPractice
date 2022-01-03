using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaIIICO.BD;
using PracticaIIICO.ViewModels;

namespace PracticaIIICO.Controllers.Servicios
{
    public class ServiciosController : Controller
    {
        MotoRepuestosMakoEntities ModeloBD = new MotoRepuestosMakoEntities();
        // GET: Servicios
        public ActionResult Index()
        {
            return View();
        }

        // GET: Servicios/Details/5
        public ActionResult ListaServicios(int pagina = 1)
        {
            List<sp_Retorna_Servicio_Result> datosObtenidos = new List<sp_Retorna_Servicio_Result>();
            datosObtenidos = this.ModeloBD.sp_Retorna_Servicio(null).ToList();

            this.agregaTipoServicios();

            //Con Paginador
            var cantidadRegistroPorPagina = 5; //parametro Para limitar la cantidad de elementos al mostrar
            using (var db = new MotoRepuestosMakoEntities())
            {
                var serviciosObtenidos = db.sp_Retorna_Servicio(null).Skip((pagina - 1) * cantidadRegistroPorPagina)
                    .Take(cantidadRegistroPorPagina).ToList();

                var totalRegistros = db.sp_Retorna_Servicio(null).Count();

                var modelo = new ViewServiciosModel();

                modelo.ListaServicios = serviciosObtenidos;
                modelo.PaginaActual = pagina;
                modelo.TotalRegistros = totalRegistros;
                modelo.RegistrosPorPagina = cantidadRegistroPorPagina;

                return View(modelo);
            }
            //Usar Sin Paginador
            //return View(datosObtenidos);
        }

        // GET: Servicios/Create
        public ActionResult NuevoSERV()
        {
            this.agregaTipoServicios();
            return View();
        }

        // POST: Servicios/Create
        [HttpPost]
        public ActionResult NuevoSERV(sp_Retorna_Servicio_Result collection)
        {
            int cantRegistroAfectado = 0;
            string resultado = "";

            try
            {
                cantRegistroAfectado = this.ModeloBD.sp_Inserta_Servicio(
                    collection.ID_TipoServicio,
                    collection.Nombre_Servicio,
                    collection.Precio_Servicio
                    );

                return RedirectToAction("ListaServicios");
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

            this.agregaTipoServicios();
            return View();
        }

        [HttpPost]
        public ActionResult NuevoServicioModal(int pIdTipoServicio,string pNombreServicio,decimal pPrecioServicio)
        {
            int cantRegistroAfectado = 0;
            //Mensajes para JSON

            String MensajeFinal = "";
            int estadoRegistro = 0;

            try
            {
                cantRegistroAfectado = this.ModeloBD.sp_Inserta_Servicio(
                    pIdTipoServicio,
                    pNombreServicio,
                    pPrecioServicio
                    );

                
            }
            catch (Exception errorObtenido)
            {
                MensajeFinal = "Ocurrio Un Error " + errorObtenido.Message;
            }
            finally
            {
                if (cantRegistroAfectado > 0)
                {
                    MensajeFinal = "El Servicio "+pNombreServicio+" Se Regitro Exitosamente!";
                    estadoRegistro = 1;
                }
                else
                {
                    MensajeFinal += " No Se Pudo Registrar";
                    estadoRegistro = 0;
                }
            }
            
            return Json(new
            {
                resultado = MensajeFinal,
                estado = estadoRegistro
            });
        }
        // GET: Servicios/Edit/5
        public ActionResult ModificaSERV(int id_Serv)
        {
            sp_Retorna_Serv_ID_Result datosObtenidos = new sp_Retorna_Serv_ID_Result();
            datosObtenidos = this.ModeloBD.sp_Retorna_Serv_ID(id_Serv).FirstOrDefault();

            this.agregaTipoServicios();
            return View(datosObtenidos);
        }

        // POST: Servicios/Edit/5
        [HttpPost]
        public ActionResult ModificaSERV(sp_Retorna_Serv_ID_Result collection)
        {

            int cantRegistroAfectado = 0;
            string resultado = "";

            try
            {
                cantRegistroAfectado = this.ModeloBD.sp_Modifica_Servicio(
                    collection.ID_Servicio,
                    collection.ID_TipoServicio,
                    collection.Nombre_Servicio,
                    collection.Precio_Servicio
                    );

                return RedirectToAction("ListaServicios");
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

            this.agregaTipoServicios();
            return View();
        }

        //
        public ActionResult MostrarSERV(int id_Serv)
        {
            sp_Retorna_Serv_ID_Result datosObtenidos = new sp_Retorna_Serv_ID_Result();
            datosObtenidos = this.ModeloBD.sp_Retorna_Serv_ID(id_Serv).FirstOrDefault();

            this.agregaTipoServicios();
            return View(datosObtenidos);
        }
        // GET: Servicios/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Servicios/Delete/5
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

        void agregaTipoServicios()
        {
            this.ViewBag.ListaTipoServicios = this.ModeloBD.sp_Retorna_Tipo_Servicio(null,null).ToList();
        }
    }
}
