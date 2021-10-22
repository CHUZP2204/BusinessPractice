using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaIIICO.BD;

namespace PracticaIIICO.Controllers.Marca
{
    
    public class MarcaController : Controller
    {
        MotoRepuestosMakoEntities ModelBD = new MotoRepuestosMakoEntities();

        // GET: Marca
        public ActionResult Index()
        {
            return View();
        }

        // GET: Marca/Details/5
        public ActionResult ListaMarcas()
        {
            List<sp_Retorna_Marcas_Result> modeloVista = new List<sp_Retorna_Marcas_Result>();

            modeloVista = this.ModelBD.sp_Retorna_Marcas(null, null).ToList();

            return View(modeloVista);
        }

        // GET: Marca/Create
        public ActionResult NuevaMarca()
        {
            return View();
        }

        // POST: Marca/Create
        [HttpPost]
        public ActionResult NuevaMarca(sp_Retorna_Marcas_Result modeloVista)
        {
            int cantidadRegistrosAfectados = 0;
            string resultado = " ";
            try
            {
                cantidadRegistrosAfectados =
                    this.ModelBD.sp_Inserta_Marcas(
                        modeloVista.Nombre_Marca,
                        modeloVista.Descripcion_Marca
                        );
            }
            catch (Exception error)
            {
                resultado = "Ocurrio un error: " + error.Message;

            }
            finally
            {
                if (cantidadRegistrosAfectados > 0)
                {
                    resultado = "Registro Insertado";
                }
                else
                {
                    resultado += "No se pudo Insertar";
                }
            }
            ViewBag.Error = resultado;
            //Response.Write("<script languaje=javascript>alert('" + resultado + "');</script>");
            return View(modeloVista);
        }

        // GET: Marca/Edit/5
        public ActionResult ModificaMarca(int id_Marca)
        {
            sp_Retorna_Marcas_ID_Result modeloVista = this.ModelBD.sp_Retorna_Marcas_ID(id_Marca).FirstOrDefault();
            return View(modeloVista);
        }

        // POST: Marca/Edit/5
        [HttpPost]
        public ActionResult ModificaMarca(sp_Retorna_Marcas_ID_Result modeloVista)
        {
            int cantidadRegistrosAfectados = 0;
            string resultado = " ";
            try
            {
                cantidadRegistrosAfectados =
                    this.ModelBD.sp_Modifica_Marcas(
                        modeloVista.ID_Marca,
                        modeloVista.Nombre_Marca,
                        modeloVista.Descripcion_Marca
                        );
            }
            catch (Exception error)
            {
                resultado = "Ocurrio un error: " + error.Message;

            }
            finally
            {
                if (cantidadRegistrosAfectados > 0)
                {
                    resultado = "Registro Modificado";
                }
                else
                {
                    resultado += "No se pudo Modificar";
                }
            }
            ViewBag.Error = resultado;
            //Response.Write("<script languaje=javascript>alert('" + resultado + "');</script>");
            return View(modeloVista);
        }

        // GET: Marca/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Marca/Delete/5
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

        [HttpGet]
        public ActionResult _ModalMensaje(string mensaje)
        {
            var model = mensaje; // do whatever you need to get your model 
            return PartialView(model);
        }
    }
}
