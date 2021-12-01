﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaIIICO.BD;

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

        // GET: Cotizacion/Details/5
        public ActionResult ListaCotizacion()
        {
            List<sp_Retorna_Cotizacion_Result> vistaObtenida = new List<sp_Retorna_Cotizacion_Result>();
            vistaObtenida = this.ModeloBD.sp_Retorna_Cotizacion(null, null).ToList();

            this.agregaUsuarios();
            return View(vistaObtenida);
        }

        // GET: Cotizacion/Create
        public ActionResult NuevaCotizacion()
        {
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


            fechaIngreso = DateTime.Now;

            try
            {
                cantRegistroAfectado = this.ModeloBD.sp_Inserta_Cotizacion(
                    collection.ID_Usuario,
                    collection.Numero_Cotizacion,
                    collection.Nombre_Cliente,
                    collection.Telefono_Cliente,
                    collection.Correo_Cliente,
                    fechaIngreso,
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
        void agregaCotizaciones(int idCot)
        {
            this.ViewBag.ListaCotizaciones = this.ModeloBD.sp_Retorna_CotizacionID(idCot).ToList();
        }

        void agregaProductos()
        {
            this.ViewBag.ListaProductos = this.ModeloBD.sp_Retorna_Productos(null,null).ToList();
        }

        void agregaServicios()
        {
            this.ViewBag.ListaServicios = this.ModeloBD.sp_Retorna_Servicio(null).ToList();
        }
    }
}
