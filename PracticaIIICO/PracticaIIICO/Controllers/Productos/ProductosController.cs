using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaIIICO.BD;

namespace PracticaIIICO.Controllers
{
    public class ProductosController : Controller
    {
        MotoRepuestosMakoEntities ModeloBD = new MotoRepuestosMakoEntities();   
        // GET: Productos
        public ActionResult Index()
        {
            return View();
        }

        // GET: Productos/Details/5
        public ActionResult ListaProductos()
        {
            List<sp_Retorna_Productos_Result> datosObtenidos = new List<sp_Retorna_Productos_Result>();
            datosObtenidos = this.ModeloBD.sp_Retorna_Productos(null, null).ToList();

            this.agregaCategorias();
            return View(datosObtenidos);
        }

        // GET: Productos/Create
        public ActionResult NuevoPROD()
        {
            this.agregaCategorias();
            return View();
        }

        // POST: Productos/Create
        [HttpPost]
        public ActionResult NuevoPROD(sp_Retorna_Productos_Result collection)
        {

            int cantidadRegistrosAfectados = 0;
            string resultado = " ";
            bool estadoPROD = true;
            try
            {
                cantidadRegistrosAfectados =
                this.ModeloBD.sp_Inserta_Products(
                    collection.ID_Categoria,
                    collection.Nombre_PROD,
                    collection.Precio_PROD,
                    collection.Descripcion_PROD,
                    estadoPROD
                    );
                return RedirectToAction("ListaProductos");
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
            this.agregaCategorias();
            return View(collection);
        }

        // GET: Productos/Edit/5
        public ActionResult ModificaPROD(int id_prod)
        {
            sp_Retorna_Products_ID_Result vistObtenida = new sp_Retorna_Products_ID_Result();
            vistObtenida = this.ModeloBD.sp_Retorna_Products_ID(id_prod).FirstOrDefault();

            this.agregaCategorias();
            return View(vistObtenida);
        }

        // POST: Productos/Edit/5
        [HttpPost]
        public ActionResult ModificaPROD(sp_Retorna_Products_ID_Result collection)
        {
            int cantidadRegistrosAfectados = 0;
            string resultado = " ";

            try
            {
                cantidadRegistrosAfectados =
                this.ModeloBD.sp_Modifica_Products(
                    collection.ID_Producto,
                    collection.ID_Categoria,
                    collection.Nombre_PROD,
                    collection.Precio_PROD,
                    collection.Descripcion_PROD,
                    collection.Estado_PROD
                    );
                return RedirectToAction("ListaProductos");
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
            this.agregaCategorias();
            return View(collection);
        }
        //
        public ActionResult MostrarPROD(int id_prod)
        {
            sp_Retorna_Products_ID_Result vistObtenida = new sp_Retorna_Products_ID_Result();
            vistObtenida = this.ModeloBD.sp_Retorna_Products_ID(id_prod).FirstOrDefault();

            this.agregaCategorias();
            return View(vistObtenida);
        }
        // GET: Productos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Productos/Delete/5
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

        void agregaCategorias()
        {
            this.ViewBag.ListaCategorias = this.ModeloBD.sp_Retorna_Categorias(null, null).ToList();
        }
    }
}
