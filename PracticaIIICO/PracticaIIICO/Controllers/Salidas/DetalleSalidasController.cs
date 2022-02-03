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

            

            try
            {
                if (stockActual <= 0)
                {
                    resultado += "No Hay Productos En Inventario: Cantidad Actual 0 ";
                }
                else if (stockActual < stockVista)
                {
                    resultado += "La Cantidad Solicitada Supera la existencia del Producto \n" +
                                 " en el Inventario, Solo hay: " + stockActual + " productos en existencia";
                }
                else
                {
                    stockFinal = stockActual - stockVista;
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

                    return RedirectToAction("ListaSalidas", "Salidas");
                }

            }
            catch (Exception errorObtenido)
            {
                resultado = "Ocurrio Un Error: " + errorObtenido.Message +"\n ";
            }
            finally
            {
                if (cantRegistroAfectado > 0)
                {
                    resultado = "Registro Insertado";
                }
                else
                {
                    resultado += " No se pudo Registrar el Detalle";
                }
            }

            Response.Write("<script languaje=javascript>alert('" + resultado + "');</script>");
            this.agregaProductos();
            return View(collection);
        }

        // POST: DetalleSalidas/Create
        [HttpPost]
        public ActionResult nuevoDetSalModal(
            int pIDSalida,
            int pIDProducto,
            int pCantSalidaPROD)
        {
            int cantRegistroAfectado = 0;
            int estadoStock = 0;
            string resultado = "";

            String MensajeFinal = "";
            int estadoRegistro = 0;

            //Obtener Datos Del Producto
            sp_Retorna_Products_ID_Result productoObtenido = new sp_Retorna_Products_ID_Result();
            productoObtenido = this.ModeloBD.sp_Retorna_Products_ID(pIDProducto).FirstOrDefault();


            //Calculos

            int stockActual = 0;
            int stockVista = 0;
            int stockFinal = 0;

            stockActual = (int)productoObtenido.Cantidad_PROD;
            stockVista = pCantSalidaPROD;



            try
            {
                if (stockActual <= 0)
                {
                    MensajeFinal += "No Hay Productos En Inventario: Cantidad Actual 0 ";
                }
                else if (stockActual < stockVista)
                {
                    MensajeFinal += "La Cantidad Solicitada Supera la existencia del Producto \n" +
                        " en el Inventario, Solo hay: " + stockActual + " productos en existencia";
                }
                else
                {
                    stockFinal = stockActual - stockVista;
                    // Registrar Detalle Salida
                    cantRegistroAfectado = this.ModeloBD.sp_Inserta_Detalle_Salida(
                        pIDSalida,
                        pIDProducto,
                        pCantSalidaPROD);

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
                }


            }
            catch (Exception error)
            {
                MensajeFinal = "Ocurrio Un Error" + error.Message;
            }
            finally
            {
                if (cantRegistroAfectado > 0)
                {
                    MensajeFinal = "Se Agrego El Detalle Exitosamente!";
                    estadoRegistro = 1;
                }
                else
                {
                    MensajeFinal += " No Se Pudo Registrar El Detalle";
                    estadoRegistro = 0;
                }
            }

            return Json(new
            {
                resultado = MensajeFinal,
                estado = estadoRegistro
            });
        }

        //
        [HttpPost]
        public ActionResult eliminaDetalleSal(int idDetSal)
        {
            int cantRegistroAfectado = 0;
            string MensajeFinal = "";
            int estadoRegistro = 0;

            int estadoStock = 0;
            int cantRegistroAfectado1 = 0;





            //Calculos

            int stockActual = 0;
            int stockVista = 0;
            int stockFinal = 0;


            sp_Retorna_Detalle_Salida_Result detalleActual = new sp_Retorna_Detalle_Salida_Result();
            detalleActual = this.ModeloBD.sp_Retorna_Detalle_Salida(idDetSal).FirstOrDefault();

            //Calcular Precio Por La Cantidad Adquirida
            sp_Retorna_Products_ID_Result productoObtenido = new sp_Retorna_Products_ID_Result();
            productoObtenido = this.ModeloBD.sp_Retorna_Products_ID(detalleActual.ID_Producto).FirstOrDefault();

            stockActual = (int)productoObtenido.Cantidad_PROD;
            stockVista = detalleActual.Cant_Salida_PROD;

            /*Restar la Cantidad Total Del Stock Al Cual se la habia agregado al detalle*/
            stockFinal = stockActual + stockVista;


            try
            {
                cantRegistroAfectado = this.ModeloBD.sp_Elimina_DetalleSalida(
                    idDetSal
                    );


                //Actualizar el Inventario el campo STOCK Sumar al STOCk actual lo que ingreso en detalle entradas

                estadoStock = this.ModeloBD.sp_Modifica_Products(
                 productoObtenido.ID_Producto,
                 productoObtenido.ID_Categoria,
                 productoObtenido.Nombre_PROD,
                 productoObtenido.Precio_PROD,
                 productoObtenido.Descripcion_PROD,
                 productoObtenido.Estado_PROD,
                 stockFinal
               );

            }
            catch (Exception error)
            {
                MensajeFinal += "Ocurrio Un Error" + error.Message;
            }
            finally
            {
                if (cantRegistroAfectado > 0)
                {
                    MensajeFinal = "Se Elimino El Detalle: " + idDetSal + "  Exitosamente!";
                    estadoRegistro = 1;
                }
                else
                {
                    MensajeFinal += " No Se Elimino El Detalle \n Intente De Nuevo";
                    estadoRegistro = 0;
                }
            }

            return Json(new
            {
                resultado = MensajeFinal,
                estado = estadoRegistro
            });


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
