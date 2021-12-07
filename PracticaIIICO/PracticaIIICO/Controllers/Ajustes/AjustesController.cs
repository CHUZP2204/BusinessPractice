using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaIIICO.BD;

namespace PracticaIIICO.Controllers.Ajustes
{
    public class AjustesController : Controller
    {
        MotoRepuestosMakoEntities ModeloBD = new MotoRepuestosMakoEntities();
        // GET: Ajustes
        public ActionResult Index()
        {
            return View();
        }

        // GET: Ajustes/Details/5
        public ActionResult ListAjustes()
        {
            List<sp_Retorna_Ajustes_Result> modelObtenido = new List<sp_Retorna_Ajustes_Result>();
            modelObtenido = this.ModeloBD.sp_Retorna_Ajustes(null).ToList();

            this.agregaProductos();
            this.agregaUsuarios();

            return View(modelObtenido);
        }

        // GET: Ajustes/Create
        public ActionResult NuevoAjuste()
        {
            this.agregaProductos();
            this.agregaUsuarios();
            return View();
        }

        // POST: Ajustes/Create
        [HttpPost]
        public ActionResult NuevoAjuste(sp_Retorna_Ajustes_Result modeloVista)
        {

            int cantRegistroAfectado = 0;
            string resultado = "";
            DateTime fechaIngreso;
            fechaIngreso = DateTime.Now;

            try
            {
                // TODO: Add insert logic here
                cantRegistroAfectado = this.ModeloBD.sp_Inserta_Ajuste(
                    modeloVista.ID_Producto,
                    modeloVista.ID_Usuario,
                    modeloVista.Tipo_Ajuste,
                    modeloVista.Cantida_Ajustar,
                    fechaIngreso,
                    modeloVista.Descripcion_Ajsute
                    );

                return RedirectToAction("ListAjustes");
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
            this.agregaProductos();
            return View();
        }

        // GET: Ajustes/Edit/5
        public ActionResult ModificaAjuste(int id_Aju)
        {

            sp_Retorna_AjusteID_Result datosObtenidos = new sp_Retorna_AjusteID_Result();
            datosObtenidos = this.ModeloBD.sp_Retorna_AjusteID(id_Aju).FirstOrDefault();

            this.agregaProductos();
            this.agregaUsuarios();

            return View(datosObtenidos);
        }

        // POST: Ajustes/Edit/5
        [HttpPost]
        public ActionResult ModificaAjuste(sp_Retorna_AjusteID_Result modeloVista)
        {
            int cantRegistroAfectado = 0;
            string resultado = "";

            try
            {
                // TODO: Add update logic here

                cantRegistroAfectado = this.ModeloBD.sp_Modifica_Ajuste(
                    modeloVista.ID_Ajuste,
                    modeloVista.ID_Producto,
                    modeloVista.ID_Usuario,
                    modeloVista.Tipo_Ajuste,
                    modeloVista.Cantida_Ajustar,
                    modeloVista.Fecha_Ajuste,
                    modeloVista.Descripcion_Ajsute
                    );

                return RedirectToAction("Index");
            }
            catch (Exception errorObtenido)
            {
                resultado = "Ocurrio Un Error: " + errorObtenido.Message;
                this.agregaUsuarios();
                this.agregaProductos();
                Response.Write("<script languaje=javascript>alert('" + resultado + "');</script>");
                return View();

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
            //

           

        }


        public ActionResult MostrarAjuste(int id_Aju)
        {
            sp_Retorna_AjusteID_Result datosObtenidos = new sp_Retorna_AjusteID_Result();
            datosObtenidos = this.ModeloBD.sp_Retorna_AjusteID(id_Aju).FirstOrDefault();

            this.agregaProductos();
            this.agregaUsuarios();

            return View(datosObtenidos);
        }
        // GET: Ajustes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Ajustes/Delete/5
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

        void agregaProductos()
        {
            this.ViewBag.ListaProductos = this.ModeloBD.sp_Retorna_Productos(null, null).ToList();
        }
    }
}
