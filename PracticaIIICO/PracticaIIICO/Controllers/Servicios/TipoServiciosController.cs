using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaIIICO.BD;

namespace PracticaIIICO.Controllers.TipoServicios
{
    public class TipoServiciosController : Controller
    {
        MotoRepuestosMakoEntities ModeloBD = new MotoRepuestosMakoEntities();
        
        // GET: TipoServicios
        public ActionResult Index()
        {
            return View();
        }

        // GET: TipoServicios/Details/5
        public ActionResult ListTypeServices()
        {
            List<sp_Retorna_Tipo_Servicio_Result> datosObtenidos = new List<sp_Retorna_Tipo_Servicio_Result>();
            datosObtenidos = this.ModeloBD.sp_Retorna_Tipo_Servicio(null, null).ToList();

            return View(datosObtenidos);
        }

        // GET: TipoServicios/Create
        public ActionResult NuevoTipoS()
        {
            return View();
        }

        // POST: TipoServicios/Create
        [HttpPost]
        public ActionResult NuevoTipoS(sp_Retorna_Tipo_Servicio_Result collection)
        {
            int cantRegistroAfectado = 0;
            string resultado = "";

            try
            {
                cantRegistroAfectado = this.ModeloBD.sp_Inserta_TipoSERV(
                    collection.Nombre_TipoServicio,
                    collection.Descripcion_TipoServicio
                    );

                return RedirectToAction("ListTypeServices");
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
            
            return View();
        }

        // GET: TipoServicios/Edit/5
        public ActionResult ModificaTipoS(int id_TipoS)
        {
            sp_Retorna_Tipo_SERV_ID_Result datosObtenidos = new sp_Retorna_Tipo_SERV_ID_Result();
            datosObtenidos = this.ModeloBD.sp_Retorna_Tipo_SERV_ID(id_TipoS).FirstOrDefault();

            return View(datosObtenidos);
        }

        // POST: TipoServicios/Edit/5
        [HttpPost]
        public ActionResult ModificaTipoS(sp_Retorna_Tipo_SERV_ID_Result collection)
        {
            int cantRegistroAfectado = 0;
            string resultado = "";

            try
            {
                cantRegistroAfectado = this.ModeloBD.sp_Modifica_TipoSERV(
                    collection.ID_TipoServicio,
                    collection.Nombre_TipoServicio,
                    collection.Descripcion_TipoServicio
                    );

                return RedirectToAction("ListTypeServices");
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

            return View(collection);
        }

        public ActionResult MostrarTipoS(int id_TipoS)
        {
            sp_Retorna_Tipo_SERV_ID_Result datosObtenidos = new sp_Retorna_Tipo_SERV_ID_Result();
            datosObtenidos = this.ModeloBD.sp_Retorna_Tipo_SERV_ID(id_TipoS).FirstOrDefault();

            return View(datosObtenidos);
            
        }

        // GET: TipoServicios/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TipoServicios/Delete/5
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
