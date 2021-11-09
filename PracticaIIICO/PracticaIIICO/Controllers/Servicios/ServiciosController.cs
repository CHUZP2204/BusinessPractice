using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaIIICO.BD;

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
        public ActionResult ListaServicios()
        {
            List<sp_Retorna_Servicio_Result> datosObtenidos = new List<sp_Retorna_Servicio_Result>();
            datosObtenidos = this.ModeloBD.sp_Retorna_Servicio(null).ToList();

            this.agregaTipoServicios();
            return View(datosObtenidos);
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
