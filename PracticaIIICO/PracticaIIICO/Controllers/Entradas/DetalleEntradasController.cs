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

        void agregaProductos()
        {
            this.ViewBag.ListaProductos = this.ModeloBD.sp_Retorna_Productos(null, null).ToList();
        }
    }
}
