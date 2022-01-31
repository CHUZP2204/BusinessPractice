using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaIIICO.BD;

namespace PracticaIIICO.Controllers.Entradas
{
    public class DetalleEntradasController : Controller
    {
        MotoRepuestosMakoEntities ModeloBD = new MotoRepuestosMakoEntities();

        // GET: DetalleEntradas
        public ActionResult ListDetalleEntr()
        {
            List<sp_Retorna_Detalle_EntrID_Result> vistaObtenida = new List<sp_Retorna_Detalle_EntrID_Result>();
            vistaObtenida = this.ModeloBD.sp_Retorna_Detalle_EntrID(null).ToList();

            this.agregaProductos();

            return View(vistaObtenida);
        }

        // GET: DetalleEntradas/Details/5
        public ActionResult MostrarDetEntr(int id_Entr)
        {
            List<sp_Retorna_Detalle_FacturaID_Result> vistaObtenida = new List<sp_Retorna_Detalle_FacturaID_Result>();
            vistaObtenida = this.ModeloBD.sp_Retorna_Detalle_FacturaID(id_Entr).ToList();

            string Mensaje = id_Entr.ToString();
            /*Session["Mensaje"]*/
            ViewBag.NumeroFactura = Mensaje;

            this.agregaProductos();
            this.agregaEntradaMontos(id_Entr);

            return View(vistaObtenida);
        }

        // GET: DetalleEntradas/Create
        public ActionResult AgregaDetEntr(int id_Entr)
        {
            sp_Retorna_Detalle_Entradas_Result modeloVista = new sp_Retorna_Detalle_Entradas_Result();

            modeloVista.ID_DetalleEntrada = 0;
            modeloVista.ID_Entrada = id_Entr;
            modeloVista.ID_Producto = 0;
            modeloVista.Cant_Adquirida_PROD = 0;
            modeloVista.PrecioXCant = 0;

            this.agregaProductos();

            return View(modeloVista);
        }

        // POST: DetalleEntradas/Create
        [HttpPost]
        public ActionResult AgregaDetEntr(sp_Retorna_Detalle_Entradas_Result modeloVista)
        {
            int cantRegistroAfectado = 0;
            int cantidadRegistrosAfectados1 = 0;
            string resultado = "";
            int estadoStock = 0;

            //Calcular Precio Por La Cantidad Adquirida
            sp_Retorna_Products_ID_Result productoObtenido = new sp_Retorna_Products_ID_Result();
            productoObtenido = this.ModeloBD.sp_Retorna_Products_ID(modeloVista.ID_Producto).FirstOrDefault();

            decimal precioPorCantidad = 0;
            precioPorCantidad = productoObtenido.Precio_PROD * modeloVista.Cant_Adquirida_PROD;
            //Calculos Factura

            decimal montoConDescuento;
            double montoDeIva;
            decimal montoTotal;
            double montoBrutoF;

            decimal montoFInalIva;
            decimal montoBruto = 0;


            //Calculos

            int stockActual = 0;
            int stockVista = 0;
            int stockFinal = 0;

            stockActual = (int)productoObtenido.Cantidad_PROD;
            stockVista = modeloVista.Cant_Adquirida_PROD;

            stockFinal = stockActual + stockVista;

            

            try
            {
                cantRegistroAfectado = this.ModeloBD.sp_Inserta_Detalle_Entrada(
                    modeloVista.ID_Entrada,
                    modeloVista.ID_Producto,
                    modeloVista.Cant_Adquirida_PROD,
                    precioPorCantidad
                    );

                ///Actualizar Montos de la Tabla Entradas
                List<sp_Retorna_EntradaID_Result> listaActualizar = new List<sp_Retorna_EntradaID_Result>();
                listaActualizar = this.ModeloBD.sp_Retorna_EntradaID(modeloVista.ID_Entrada).ToList();

                ///Obtener Total de monto de detalle Entradas
                List<sp_Retorna_Detalle_FacturaID_Result> listaDetalle = new List<sp_Retorna_Detalle_FacturaID_Result>();
                listaDetalle = this.ModeloBD.sp_Retorna_Detalle_FacturaID(modeloVista.ID_Entrada).ToList();

                ///Sumar Los Valores Precio Por Cantidad De Detalle Entradas 
                for (int posicionActual = 0; posicionActual < listaDetalle.Count; posicionActual++)
                {
                    montoBruto = montoBruto + listaDetalle[posicionActual].PrecioXCant;
                }

                ///Calculos
                montoBrutoF = (double)montoBruto;

                //montoConDescuento = montoBruto - modeloVista.Monto_Descuento;


                montoDeIva = montoBrutoF * 0.13;
                montoFInalIva = (Decimal)montoDeIva;

                montoTotal = montoBruto + montoFInalIva;

                ///Obtener Los Datos Del Encabezado Requeridos Para El Sp inserta Encabezado
                ///Esto Para Que Ningun Valor Cambie
                int idEntradaModifcar = 0;
                int idEntProveedorM = 0;
                int idEntUsuarioM = 0;
                int numfacturaM = 0;
                DateTime fechaFactura = DateTime.MinValue;
                DateTime fechaRegistro = DateTime.MinValue;

                for (int posicionActual = 0; posicionActual < listaActualizar.Count; posicionActual++)
                {
                    idEntradaModifcar = listaActualizar[posicionActual].ID_Entrada;
                    idEntProveedorM = listaActualizar[posicionActual].ID_Proveedor;
                    idEntUsuarioM = listaActualizar[posicionActual].ID_Usuario;
                    numfacturaM = listaActualizar[posicionActual].Numero_Factura;
                    fechaFactura = listaActualizar[posicionActual].Fecha_Factura;
                    fechaRegistro = listaActualizar[posicionActual].Fecha_Registro;
                }

                ///Actualizar Tabla Encabezado Factura Con El Valor Nuevo De Monto Total Final
                cantidadRegistrosAfectados1 = this.ModeloBD.sp_Modifica_Entrada(
                    idEntradaModifcar,
                    idEntProveedorM,
                    idEntUsuarioM,
                    numfacturaM,
                    fechaFactura,
                    fechaRegistro,
                    montoBruto,
                    0,
                    montoFInalIva,
                    montoTotal
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

                return RedirectToAction("ListaEntradas", "Entradas");
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
        //
        // POST: DetalleEntradas/Create
        [HttpPost]
        public ActionResult nuevoDetEntrModal(
            int pIdEntrada,
            int pIdProducto,
            int pCantidad,
            double pPrecioCant)
        {
            int cantRegistroAfectado = 0;
            int cantidadRegistrosAfectados1 = 0;
            string resultado = "";
            int estadoStock = 0;

            String MensajeFinal = "";
            int estadoRegistro = 0;

            //Calcular Precio Por La Cantidad Adquirida
            sp_Retorna_Products_ID_Result productoObtenido = new sp_Retorna_Products_ID_Result();
            productoObtenido = this.ModeloBD.sp_Retorna_Products_ID(pIdProducto).FirstOrDefault();

            decimal precioPorCantidad = 0;
            precioPorCantidad = productoObtenido.Precio_PROD * pCantidad;
            //Calculos Factura

            decimal montoConDescuento;
            double montoDeIva;
            decimal montoTotal;
            double montoBrutoF;

            decimal montoFInalIva;
            decimal montoBruto = 0;


            //Calculos

            int stockActual = 0;
            int stockVista = 0;
            int stockFinal = 0;

            stockActual = (int)productoObtenido.Cantidad_PROD;
            stockVista = pCantidad;

            stockFinal = stockActual + stockVista;



            try
            {
                cantRegistroAfectado = this.ModeloBD.sp_Inserta_Detalle_Entrada(
                    pIdEntrada,
                    pIdProducto,
                    pCantidad,
                    precioPorCantidad
                    );

                ///Actualizar Montos de la Tabla Entradas
                List<sp_Retorna_EntradaID_Result> listaActualizar = new List<sp_Retorna_EntradaID_Result>();
                listaActualizar = this.ModeloBD.sp_Retorna_EntradaID(pIdEntrada).ToList();

                ///Obtener Total de monto de detalle Entradas
                List<sp_Retorna_Detalle_FacturaID_Result> listaDetalle = new List<sp_Retorna_Detalle_FacturaID_Result>();
                listaDetalle = this.ModeloBD.sp_Retorna_Detalle_FacturaID(pIdEntrada).ToList();

                ///Sumar Los Valores Precio Por Cantidad De Detalle Entradas 
                for (int posicionActual = 0; posicionActual < listaDetalle.Count; posicionActual++)
                {
                    montoBruto = montoBruto + listaDetalle[posicionActual].PrecioXCant;
                }

                ///Calculos
                montoBrutoF = (double)montoBruto;

                //montoConDescuento = montoBruto - modeloVista.Monto_Descuento;


                montoDeIva = montoBrutoF * 0.13;
                montoFInalIva = (Decimal)montoDeIva;

                montoTotal = montoBruto + montoFInalIva;

                ///Obtener Los Datos Del Encabezado Requeridos Para El Sp inserta Encabezado
                ///Esto Para Que Ningun Valor Cambie
                int idEntradaModifcar = 0;
                int idEntProveedorM = 0;
                int idEntUsuarioM = 0;
                int numfacturaM = 0;
                DateTime fechaFactura = DateTime.MinValue;
                DateTime fechaRegistro = DateTime.MinValue;

                for (int posicionActual = 0; posicionActual < listaActualizar.Count; posicionActual++)
                {
                    idEntradaModifcar = listaActualizar[posicionActual].ID_Entrada;
                    idEntProveedorM = listaActualizar[posicionActual].ID_Proveedor;
                    idEntUsuarioM = listaActualizar[posicionActual].ID_Usuario;
                    numfacturaM = listaActualizar[posicionActual].Numero_Factura;
                    fechaFactura = listaActualizar[posicionActual].Fecha_Factura;
                    fechaRegistro = listaActualizar[posicionActual].Fecha_Registro;
                }

                ///Actualizar Tabla Encabezado Factura Con El Valor Nuevo De Monto Total Final
                cantidadRegistrosAfectados1 = this.ModeloBD.sp_Modifica_Entrada(
                    idEntradaModifcar,
                    idEntProveedorM,
                    idEntUsuarioM,
                    numfacturaM,
                    fechaFactura,
                    fechaRegistro,
                    montoBruto,
                    0,
                    montoFInalIva,
                    montoTotal
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
        // GET: DetalleEntradas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DetalleEntradas/Edit/5
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
        //
        [HttpPost]
        public ActionResult eliminaDetalleEntr(int idDetEntr)
        {
            int cantRegistroAfectado = 0;
            string MensajeFinal = "";
            int estadoRegistro = 0;

            int estadoStock = 0;
            int cantRegistroAfectado1 = 0;
           

            //Calculos Factura

            decimal montoConDescuento;
            double montoDeIva;
            decimal montoTotal;
            double montoBrutoF;

            decimal montoFInalIva;
            decimal montoBruto = 0;


            //Calculos

            int stockActual = 0;
            int stockVista = 0;
            int stockFinal = 0;


            sp_Retorna_Detalle_EntrID_Result detalleActual = new sp_Retorna_Detalle_EntrID_Result();
            detalleActual = this.ModeloBD.sp_Retorna_Detalle_EntrID(idDetEntr).FirstOrDefault();

            //Calcular Precio Por La Cantidad Adquirida
            sp_Retorna_Products_ID_Result productoObtenido = new sp_Retorna_Products_ID_Result();
            productoObtenido = this.ModeloBD.sp_Retorna_Products_ID(detalleActual.ID_Producto).FirstOrDefault();

            stockActual = (int)productoObtenido.Cantidad_PROD;
            stockVista = detalleActual.Cant_Adquirida_PROD;

            /*Restar la Cantidad Total Del Stock Al Cual se la habia agregado al detalle*/
            stockFinal = stockActual - stockVista;

            decimal precioCantidad = 0;
            precioCantidad = detalleActual.PrecioXCant;




            try
            {
                cantRegistroAfectado = this.ModeloBD.sp_Elimina_DetalleEntrada(
                    idDetEntr
                    );


                ///Actualizar Montos de la Tabla Entradas
                List<sp_Retorna_EntradaID_Result> listaActualizar = new List<sp_Retorna_EntradaID_Result>();
                listaActualizar = this.ModeloBD.sp_Retorna_EntradaID(detalleActual.ID_Entrada).ToList();

                ///Obtener Total de monto de detalle Entradas
                List<sp_Retorna_Detalle_FacturaID_Result> listaDetalle = new List<sp_Retorna_Detalle_FacturaID_Result>();
                listaDetalle = this.ModeloBD.sp_Retorna_Detalle_FacturaID(detalleActual.ID_Entrada).ToList();

                ///Sumar Los Valores Precio Por Cantidad De Detalle Entradas 
                for (int posicionActual = 0; posicionActual < listaDetalle.Count; posicionActual++)
                {
                    montoBruto = montoBruto + listaDetalle[posicionActual].PrecioXCant;
                }

                ///Calculos
                montoBrutoF = (double)montoBruto;

                //montoConDescuento = montoBruto - modeloVista.Monto_Descuento;


                montoDeIva = montoBrutoF * 0.13;
                montoFInalIva = (Decimal)montoDeIva;

                montoTotal = montoBruto + montoFInalIva;

                ///Obtener Los Datos Del Encabezado Requeridos Para El Sp inserta Encabezado
                ///Esto Para Que Ningun Valor Cambie
                int idEntradaModifcar = 0;
                int idEntProveedorM = 0;
                int idEntUsuarioM = 0;
                int numfacturaM = 0;
                DateTime fechaFactura = DateTime.MinValue;
                DateTime fechaRegistro = DateTime.MinValue;

                for (int posicionActual = 0; posicionActual < listaActualizar.Count; posicionActual++)
                {
                    idEntradaModifcar = listaActualizar[posicionActual].ID_Entrada;
                    idEntProveedorM = listaActualizar[posicionActual].ID_Proveedor;
                    idEntUsuarioM = listaActualizar[posicionActual].ID_Usuario;
                    numfacturaM = listaActualizar[posicionActual].Numero_Factura;
                    fechaFactura = listaActualizar[posicionActual].Fecha_Factura;
                    fechaRegistro = listaActualizar[posicionActual].Fecha_Registro;
                }

                ///Actualizar Tabla Encabezado Factura Con El Valor Nuevo De Monto Total Final
                cantRegistroAfectado1 = this.ModeloBD.sp_Modifica_Entrada(
                    idEntradaModifcar,
                    idEntProveedorM,
                    idEntUsuarioM,
                    numfacturaM,
                    fechaFactura,
                    fechaRegistro,
                    montoBruto,
                    0,
                    montoFInalIva,
                    montoTotal
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
                    MensajeFinal = "Se Elimino El Detalle: " + idDetEntr + "  Exitosamente!";
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
        // GET: DetalleEntradas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DetalleEntradas/Delete/5
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

        void agregaEntradaMontos(int idEntrada)
        {
            this.ViewBag.ListaEntradas = this.ModeloBD.sp_Retorna_EntradaID(idEntrada).ToList();
        }
        void agregaProductos()
        {
            this.ViewBag.ListaProductos = this.ModeloBD.sp_Retorna_Productos(null, null).ToList();
        }
    }
}
