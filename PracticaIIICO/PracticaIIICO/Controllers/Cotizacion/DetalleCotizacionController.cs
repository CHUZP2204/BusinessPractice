using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaIIICO.BD;

namespace PracticaIIICO.Controllers.Cotizacion
{
    public class DetalleCotizacionController : Controller
    {
        MotoRepuestosMakoEntities ModeloBD = new MotoRepuestosMakoEntities();
        // GET: DetalleCotizacion
        public ActionResult Index()
        {
            return View();
        }

        // GET: DetalleCotizacion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult MostrarDetCot(int id_Cot)
        {
            List<sp_Retorna_DetalleCotID_Result> modeloVista = new List<sp_Retorna_DetalleCotID_Result>();
            modeloVista = this.ModeloBD.sp_Retorna_DetalleCotID(id_Cot).ToList();

            string Mensaje = id_Cot.ToString();
            /*Session["Mensaje"]*/
            ViewBag.NumeroCotizacion= Mensaje;

            this.agregaProductos();
            this.agregaServicios();

            return View(modeloVista);
        }
        // GET: DetalleCotizacion/Create
        public ActionResult AgregaDetCot(int id_Cot)
        {
            DetalleCotizacionTbl modeloVista = new DetalleCotizacionTbl();
            modeloVista.ID_DetalleCotizar = 0;
            modeloVista.ID_Cotizacion = id_Cot;
            modeloVista.ID_Producto = 0;
            modeloVista.ID_Servicio = 0;
            modeloVista.Cant_AdquiPROD = 0;
            modeloVista.PrecioXCant = 0;

            this.agregaProductos();
            this.agregaServicios();

            return View(modeloVista);
        }

        // POST: DetalleCotizacion/Create
        [HttpPost]
        public ActionResult AgregaDetCot(sp_Retorna_DetalleCoti_Result collection)
        {
            int cantRegistroAfectado = 0;
            string resultado = "";

            try
            {
                cantRegistroAfectado = this.ModeloBD.sp_Inserta_DetalleCoti(
                    collection.ID_Cotizacion,
                    collection.ID_Producto,
                    collection.ID_Servicio,
                    collection.Cant_AdquiPROD,
                    collection.PrecioXCant
                    );

                return RedirectToAction("ListaCotizacion","Cotizacion");
            }
            catch(Exception errorObtenido)
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

            this.agregaProductos();
            this.agregaServicios();

            return View();
        }

        // GET: DetalleCotizacion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DetalleCotizacion/Edit/5
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

        // GET: DetalleCotizacion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DetalleCotizacion/Delete/5
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

        void agregaProductos()
        {
            this.ViewBag.ListaProductos = this.ModeloBD.sp_Retorna_Productos(null, null).ToList();
        }

        void agregaServicios()
        {
            this.ViewBag.ListaServicios = this.ModeloBD.sp_Retorna_Servicio(null).ToList();
        }
    }
}
