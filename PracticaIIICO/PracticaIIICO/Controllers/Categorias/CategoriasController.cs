using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaIIICO.BD;
using PracticaIIICO.ViewModels;

namespace PracticaIIICO.Controllers.Categorias
{
    public class CategoriasController : Controller
    {
        MotoRepuestosMakoEntities ModeloBD = new MotoRepuestosMakoEntities();
        // GET: Categorias
        public ActionResult Index()
        {
            return View();
        }

        // GET: Categorias/Details/5
        public ActionResult ListaCategorias(int pagina = 1)
        {
            //Sin Paginador
            List<sp_Retorna_Categorias_Result> datosObtenidos = new List<sp_Retorna_Categorias_Result>();
            datosObtenidos = this.ModeloBD.sp_Retorna_Categorias(null, null).ToList();

            //Con Pagiandor


            var cantidadRegistroPorPagina = 5; //parametro Para limitar la cantidad de elementos al mostrar
            using (var db = new MotoRepuestosMakoEntities())
            {
                var categoriasObtenidas = db.sp_Retorna_Categorias(null,null).Skip((pagina - 1) * cantidadRegistroPorPagina)
                    .Take(cantidadRegistroPorPagina).ToList();

                var totalRegistros = db.sp_Retorna_Categorias(null,null).Count();

                var modelo = new ViewCategoriasModel();

                modelo.ListaCategorias = categoriasObtenidas;
                modelo.PaginaActual = pagina;
                modelo.TotalRegistros = totalRegistros;
                modelo.RegistrosPorPagina = cantidadRegistroPorPagina;

                return View(modelo);
            }
           //Usar Sin Paginador return View(datosObtenidos);
        }

        // GET: Categorias/Create
        public ActionResult NuevoCAT()
        {
            return View();
        }

        // POST: Categorias/Create
        [HttpPost]
        public ActionResult NuevoCAT(sp_Retorna_Categorias_Result collection)
        {
            int cantidadRegistrosAfectados = 0;
            string resultado = " ";
            
            try
            {
                cantidadRegistrosAfectados =
                this.ModeloBD.sp_Inserta_Categorias(
                    collection.Nombre_Categoria,
                    collection.Descripcion_Categoria
                    );
                return RedirectToAction("ListaCategorias");
            }
            catch (Exception errorObtenido)
            {
                resultado = "Ocurrio Un Error: " + errorObtenido.Message;
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
            Response.Write("<script languaje=javascript>alert('" + resultado + "');</script>");
            return View(collection);
        }

        // GET: Categorias/Edit/5
        public ActionResult ModificaCAT(int id_cat)
        {
            sp_Retorna_Categorias_ID_Result vistaObtenida = new sp_Retorna_Categorias_ID_Result();
            vistaObtenida = this.ModeloBD.sp_Retorna_Categorias_ID(id_cat).FirstOrDefault();

            return View(vistaObtenida);
        }

        // POST: Categorias/Edit/5
        [HttpPost]
        public ActionResult ModificaCAT(sp_Retorna_Categorias_ID_Result collection)
        {
            int cantidadRegistrosAfectados = 0;
            string resultado = " ";

            try
            {
                cantidadRegistrosAfectados =
                this.ModeloBD.sp_Modifica_Categorias(
                    collection.ID_Categoria,
                    collection.Nombre_Categoria,
                    collection.Descripcion_Categoria
                    );
                return RedirectToAction("ListaCategorias");
            }
            catch (Exception errorObtenido)
            {
                resultado = "Ocurrio Un Error: " + errorObtenido.Message;
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
            Response.Write("<script languaje=javascript>alert('" + resultado + "');</script>");
            return View(collection);
        }

        //
        public ActionResult MostrarCAT(int id_cat)
        {
            sp_Retorna_Categorias_ID_Result vistaObtenida = new sp_Retorna_Categorias_ID_Result();
            vistaObtenida = this.ModeloBD.sp_Retorna_Categorias_ID(id_cat).FirstOrDefault();

            return View(vistaObtenida);
        }
        // GET: Categorias/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Categorias/Delete/5
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
