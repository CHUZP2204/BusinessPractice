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
            ViewBag.NumeroCotizacion = Mensaje;

            this.agregaCotizaciones(id_Cot);
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
            int cantRegistroAfectado1 = 0;
            string resultado = "";

            
            decimal costoFinalCOT = 0;
            decimal costoIvaCOT = 0;

            //Producto Seleccionado
            sp_Retorna_Products_ID_Result productoSeleccionado = new sp_Retorna_Products_ID_Result();
            productoSeleccionado = this.ModeloBD.sp_Retorna_Products_ID(collection.ID_Producto).FirstOrDefault();


            decimal precioPorCantidad = 0;
            precioPorCantidad = productoSeleccionado.Precio_PROD * collection.Cant_AdquiPROD;

            





            try
            {
                cantRegistroAfectado = this.ModeloBD.sp_Inserta_DetalleCoti(
                    collection.ID_Cotizacion,
                    collection.ID_Producto,
                    collection.ID_Servicio,
                    collection.Cant_AdquiPROD,
                    precioPorCantidad
                    );

                //Maestro Cotizacion Con Datos
                List<sp_Retorna_DesgloseCotizacion_Result> modeloCotizacion = new List<sp_Retorna_DesgloseCotizacion_Result>();

                modeloCotizacion = this.ModeloBD.sp_Retorna_DesgloseCotizacion(collection.ID_Cotizacion).ToList();

                // Maestro Cotizacion A Actualizar
                ///Actualizar Montos de la Tabla Entradas
                List<sp_Retorna_CotizacionID_Result> listaActualizar = new List<sp_Retorna_CotizacionID_Result>();
                listaActualizar = this.ModeloBD.sp_Retorna_CotizacionID(collection.ID_Cotizacion).ToList();

                ///Sumar Los Valores Precio Por Cantidad De Detalle Entradas 
                for (int posicionActual = 0; posicionActual < modeloCotizacion.Count; posicionActual++)
                {
                    costoFinalCOT = costoFinalCOT + modeloCotizacion[posicionActual].PrecioXCant;
                }

                ///Obtener Los Datos Del Encabezado Requeridos Para El Sp inserta Encabezado
                ///Esto Para Que Ningun Valor Cambie
                int idCotModifcar = 0;
                int idCotUsuarioM = 0;
                int numCotM = 0;
                string nomClientCot = "";
                string telClientCot = "";
                string emailClientCot = "";
                DateTime fechaCotM = DateTime.MinValue;
                TimeSpan horaCotM = TimeSpan.MinValue ;

                for (int posicionActual = 0; posicionActual < listaActualizar.Count; posicionActual++)
                {
                    idCotModifcar = listaActualizar[posicionActual].ID_Cotizacion;
                    idCotUsuarioM = listaActualizar[posicionActual].ID_Usuario;
                    numCotM = listaActualizar[posicionActual].Numero_Cotizacion;
                    nomClientCot = listaActualizar[posicionActual].Nombre_Cliente;
                    telClientCot = listaActualizar[posicionActual].Telefono_Cliente;
                    emailClientCot = listaActualizar[posicionActual].Correo_Cliente;
                    fechaCotM = listaActualizar[posicionActual].Fecha_Cotizacion;
                    horaCotM = listaActualizar[posicionActual].Hora_Cotizacion;
                }


                //Llamar al SP Update de Cotizacion

                cantRegistroAfectado1 = this.ModeloBD.sp_Modifica_Cotizacion(
                    idCotModifcar,
                    idCotUsuarioM,
                    numCotM, 
                    nomClientCot,
                    telClientCot,
                    emailClientCot,
                    fechaCotM,
                    horaCotM,
                    costoFinalCOT);


                return RedirectToAction("ListaCotizacion", "Cotizacion");
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
            this.agregaServicios();

            return View();
        }

        // POST: DetalleCotizacion/Create
        [HttpPost]
        public ActionResult NuevaDetCotModal(
            int pIdCotizacion,
            int pIdProducto,
            int pIdServicio,
            int pCantidad,
            int pPrecioCant)
        {

            String MensajeFinal = "";
            int estadoRegistro = 0;

            int cantRegistroAfectado = 0;
            int cantRegistroAfectado1 = 0;



            decimal costoFinalCOT = 0;
            decimal costoIvaCOT = 0;

           

            //Calcular Precio Por La Cantidad Adquirida
            sp_Retorna_Products_ID_Result productoObtenido = new sp_Retorna_Products_ID_Result();
            productoObtenido = this.ModeloBD.sp_Retorna_Products_ID(pIdProducto).FirstOrDefault();

            decimal precioPorCantidad = 0;
            precioPorCantidad = productoObtenido.Precio_PROD * pCantidad;

            try
            {
                cantRegistroAfectado = this.ModeloBD.sp_Inserta_DetalleCoti(
                    pIdCotizacion,
                    pIdProducto,
                    pIdServicio,
                    pCantidad,
                    precioPorCantidad
                    );

                //Maestro Cotizacion Con Datos
                List<sp_Retorna_DesgloseCotizacion_Result> modeloCotizacion = new List<sp_Retorna_DesgloseCotizacion_Result>();

                modeloCotizacion = this.ModeloBD.sp_Retorna_DesgloseCotizacion(pIdCotizacion).ToList();

                // Maestro Cotizacion A Actualizar
                ///Actualizar Montos de la Tabla Entradas
                List<sp_Retorna_CotizacionID_Result> listaActualizar = new List<sp_Retorna_CotizacionID_Result>();
                listaActualizar = this.ModeloBD.sp_Retorna_CotizacionID(pIdCotizacion).ToList();

                ///Sumar Los Valores Precio Por Cantidad De Detalle Entradas 
                for (int posicionActual = 0; posicionActual < modeloCotizacion.Count; posicionActual++)
                {
                    costoFinalCOT = costoFinalCOT + modeloCotizacion[posicionActual].PrecioXCant;
                }

                ///Obtener Los Datos Del Encabezado Requeridos Para El Sp inserta Encabezado
                ///Esto Para Que Ningun Valor Cambie
                int idCotModifcar = 0;
                int idCotUsuarioM = 0;
                int numCotM = 0;
                string nomClientCot = "";
                string telClientCot = "";
                string emailClientCot = "";
                DateTime fechaCotM = DateTime.MinValue;
                TimeSpan horaCotM = TimeSpan.MinValue;

                for (int posicionActual = 0; posicionActual < listaActualizar.Count; posicionActual++)
                {
                    idCotModifcar = listaActualizar[posicionActual].ID_Cotizacion;
                    idCotUsuarioM = listaActualizar[posicionActual].ID_Usuario;
                    numCotM = listaActualizar[posicionActual].Numero_Cotizacion;
                    nomClientCot = listaActualizar[posicionActual].Nombre_Cliente;
                    telClientCot = listaActualizar[posicionActual].Telefono_Cliente;
                    emailClientCot = listaActualizar[posicionActual].Correo_Cliente;
                    fechaCotM = listaActualizar[posicionActual].Fecha_Cotizacion;
                    horaCotM = listaActualizar[posicionActual].Hora_Cotizacion;
                }


                //Llamar al SP Update de Cotizacion

                cantRegistroAfectado1 = this.ModeloBD.sp_Modifica_Cotizacion(
                    idCotModifcar,
                    idCotUsuarioM,
                    numCotM,
                    nomClientCot,
                    telClientCot,
                    emailClientCot,
                    fechaCotM,
                    horaCotM,
                    costoFinalCOT);

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
        [HttpPost]
        public ActionResult eliminaDetalleCot(int idDetCot)
        {
            int cantRegistroAfectado = 0;
            string MensajeFinal = "";
            int estadoRegistro = 0;

            int cantRegistroAfectado1 = 0;
            decimal costoFinalCOT = 0;

            sp_Retorna_DetalleCoti_Result detalleActual = new sp_Retorna_DetalleCoti_Result();
            detalleActual = this.ModeloBD.sp_Retorna_DetalleCoti(idDetCot).FirstOrDefault();

            decimal precioCantidad = 0;
            precioCantidad = detalleActual.PrecioXCant;

            


            try
            {
                cantRegistroAfectado = this.ModeloBD.sp_Elimina_DetalleCoti(
                    idDetCot
                    );

                //Maestro Cotizacion Con Datos
                List<sp_Retorna_DesgloseCotizacion_Result> modeloCotizacion = new List<sp_Retorna_DesgloseCotizacion_Result>();

                modeloCotizacion = this.ModeloBD.sp_Retorna_DesgloseCotizacion(detalleActual.ID_Cotizacion).ToList();

                // Maestro Cotizacion A Actualizar
                ///Actualizar Montos de la Tabla Entradas
                List<sp_Retorna_CotizacionID_Result> listaActualizar = new List<sp_Retorna_CotizacionID_Result>();
                listaActualizar = this.ModeloBD.sp_Retorna_CotizacionID(detalleActual.ID_Cotizacion).ToList();


                for (int posicionActual = 0; posicionActual < modeloCotizacion.Count; posicionActual++)
                {
                    costoFinalCOT = costoFinalCOT + modeloCotizacion[posicionActual].PrecioXCant;
                }

                ///Obtener Los Datos Del Encabezado Requeridos Para El Sp inserta Encabezado
                ///Esto Para Que Ningun Valor Cambie
                int idCotModifcar = 0;
                int idCotUsuarioM = 0;
                int numCotM = 0;
                string nomClientCot = "";
                string telClientCot = "";
                string emailClientCot = "";
                DateTime fechaCotM = DateTime.MinValue;
                TimeSpan horaCotM = TimeSpan.MinValue;

                for (int posicionActual = 0; posicionActual < listaActualizar.Count; posicionActual++)
                {
                    idCotModifcar = listaActualizar[posicionActual].ID_Cotizacion;
                    idCotUsuarioM = listaActualizar[posicionActual].ID_Usuario;
                    numCotM = listaActualizar[posicionActual].Numero_Cotizacion;
                    nomClientCot = listaActualizar[posicionActual].Nombre_Cliente;
                    telClientCot = listaActualizar[posicionActual].Telefono_Cliente;
                    emailClientCot = listaActualizar[posicionActual].Correo_Cliente;
                    fechaCotM = listaActualizar[posicionActual].Fecha_Cotizacion;
                    horaCotM = listaActualizar[posicionActual].Hora_Cotizacion;
                }


                //Llamar al SP Update de Cotizacion

                cantRegistroAfectado1 = this.ModeloBD.sp_Modifica_Cotizacion(
                    idCotModifcar,
                    idCotUsuarioM,
                    numCotM,
                    nomClientCot,
                    telClientCot,
                    emailClientCot,
                    fechaCotM,
                    horaCotM,
                    costoFinalCOT);

            }
            catch (Exception error)
            {
                MensajeFinal += "Ocurrio Un Error" + error.Message;
            }
            finally
            {
                if (cantRegistroAfectado > 0)
                {
                    MensajeFinal = "Se Elimino El Detalle: " + idDetCot + "  Exitosamente!";
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

        void agregaCotizaciones(int idCot)
        {
            this.ViewBag.ListaCotizaciones = this.ModeloBD.sp_Retorna_CotizacionID(idCot).ToList();
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
