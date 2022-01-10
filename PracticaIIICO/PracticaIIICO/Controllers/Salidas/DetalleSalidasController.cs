using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaIIICO.BD;

namespace PracticaIIICO.Controllers.Salidas
{
    public class DetalleSalidasController : Controller
    {
        MotoRepuestosMakoEntities ModeloBD = new MotoRepuestosMakoEntities();
        // GET: DetalleSalidas
        public ActionResult Index()
        {
            return View();
        }

        // GET: DetalleSalidas/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DetalleSalidas/Create
        public ActionResult AgregaDetSal(int id_Sal)
        {
            sp_Retorna_Detalle_Salida_Result modeloVista = new sp_Retorna_Detalle_Salida_Result();

            modeloVista.ID_DetalleSalida = 0;
            modeloVista.ID_Salida = id_Sal;
            modeloVista.ID_Producto = 0;

            this.agregaProductos();

            return View(modeloVista);
        }

        // POST: DetalleSalidas/Create
        [HttpPost]
        public ActionResult AgregaDetSal(sp_Retorna_Detalle_Salida_Result collection)
        {
            int cantRegistroAfectado = 0;
            int estadoStock = 0;
            string resultado = "";

            //Obtener Datos Del Producto
            sp_Retorna_Products_ID_Result productoObtenido = new sp_Retorna_Products_ID_Result();
            productoObtenido = this.ModeloBD.sp_Retorna_Products_ID(collection.ID_Producto).FirstOrDefault();


            //Calculos

            int stockActual = 0;
            int stockVista = 0;
            int stockFinal = 0;

            stockActual = (int)productoObtenido.Cantidad_PROD;
            stockVista = collection.Cant_Salida_PROD;

            stockFinal = stockActual - stockVista;

            try
            {
                // Registrar Detalle Salida
                cantRegistroAfectado = this.ModeloBD.sp_Inserta_Detalle_Salida(
                    collection.ID_Salida,
                    collection.ID_Producto,
                    collection.Cant_Salida_PROD);

                // Actualizar STOCK Del Producto 
                //Restar la cantidad ingresada por vista ala cantidad actual

                // Actualizar el Inventario 

                 estadoStock = this.ModeloBD.sp_Modifica_Products(
                  productoObtenido.ID_Producto,
                  productoObtenido.ID_Categoria,
                  productoObtenido.Nombre_PROD,
                  productoObtenido.Precio_PROD,
                  productoObtenido.Descripcion_PROD,
                  productoObtenido.Estado_PROD,
                  stockFinal
                );

                return RedirectToAction("ListaSalidas","Salidas");
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


            this.agregaProductos();
            return View(collection);
        }

        // GET: DetalleSalidas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DetalleSalidas/Edit/5
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

        public ActionResult MostrarDetSal(int id_Sal)
        {
            List<sp_Retorna_DetalleFacturaSalida_Result> vistaObtenida = new List<sp_Retorna_DetalleFacturaSalida_Result>();
            vistaObtenida = this.ModeloBD.sp_Retorna_DetalleFacturaSalida(id_Sal).ToList();

            string Mensaje = id_Sal.ToString();
            /*Session["Mensaje"]*/
            ViewBag.NumeroDetalleSal = Mensaje;

            this.agregaProductos();

            return View(vistaObtenida);
        }

        // GET: DetalleSalidas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DetalleSalidas/Delete/5
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
    }
}
