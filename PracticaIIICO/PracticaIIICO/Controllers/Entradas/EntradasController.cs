using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaIIICO.BD;
using PracticaIIICO.ViewModels;

namespace PracticaIIICO.Controllers.Entradas
{
    public class EntradasController : Controller
    {
        MotoRepuestosMakoEntities ModeloBD = new MotoRepuestosMakoEntities();

        // GET: Entradas
        public ActionResult Index()
        {
            return View();
        }

        // GET: Entradas/Details/5
        public ActionResult ListaEntradas(int pagina = 1)
        {
            List<sp_Retorna_Entradas_Result> vistaObtenida = new List<sp_Retorna_Entradas_Result>();
            vistaObtenida = this.ModeloBD.sp_Retorna_Entradas(null).ToList();
            this.agregaUsuarios();
            this.agregaUsuariosID();
            this.agregaProveedores();

            //Con Paginador
            var cantidadRegistroPorPagina = 5; //parametro Para limitar la cantidad de elementos al mostrar
            using (var db = new MotoRepuestosMakoEntities())
            {
                var entradasObtenidas = db.sp_Retorna_Entradas(null).Skip((pagina - 1) * cantidadRegistroPorPagina)
                    .Take(cantidadRegistroPorPagina).ToList();

                var totalRegistros = db.sp_Retorna_Entradas(null).Count();

                var modelo = new ViewEntradasModel();

                modelo.ListaEntradas = entradasObtenidas;
                modelo.PaginaActual = pagina;
                modelo.TotalRegistros = totalRegistros;
                modelo.RegistrosPorPagina = cantidadRegistroPorPagina;

                return View(modelo);
            }
            //

            //Usar sin paginador

            //return View(vistaObtenida);
        }

        // GET: Entradas/Create
        public ActionResult NuevaEntrada()
        {
            string tipoUsuarioActual = "";

            if (Session["RoleUsuario"] != null)
            {
                tipoUsuarioActual = Session["RoleUsuario"].ToString();
            }
            else
            {
                tipoUsuarioActual = "Empleado";
            }


            this.ViewBag.UsuarioActual = tipoUsuarioActual;

            this.agregaProveedores();
            this.agregaUsuarios();
            this.agregaUsuariosID();

            return View();
        }

        // POST: Entradas/Create
        [HttpPost]
        public ActionResult NuevaEntrada(sp_Retorna_Entradas_Result modeloVista)
        {
            int cantRegistroAfectado = 0;
            string resultado = "";
            DateTime fechaIngreso;

            decimal montoConDescuento;
            double montoDeIva;
            decimal montoTotal;
            double montoBrutoF;

            decimal montoFInalIva;

            montoBrutoF = (double)modeloVista.Monto_Bruto;

            montoConDescuento = modeloVista.Monto_Bruto - modeloVista.Monto_Descuento;


            montoDeIva = montoBrutoF * 0.13;
            montoFInalIva = (Decimal)montoDeIva;

            montoTotal = montoConDescuento + montoFInalIva;


            fechaIngreso = DateTime.Now;

            try
            {
                cantRegistroAfectado = this.ModeloBD.sp_Inserta_Entrada(
                    modeloVista.ID_Proveedor,
                    modeloVista.ID_Usuario,
                    modeloVista.Numero_Factura,
                    modeloVista.Fecha_Factura,
                    fechaIngreso,
                    modeloVista.Monto_Bruto,
                    modeloVista.Monto_Descuento,
                    montoFInalIva,
                    montoTotal
                    );

                return RedirectToAction("ListaEntradas");
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
            this.agregaProveedores();
            this.agregaUsuarios();
            this.agregaUsuariosID();

            return View();
        }

        //Entrada Modal
        [HttpPost]
        public ActionResult NuevaEntradaModal(int pIdProveedor, int pIdUsuario, int pNumFactura, DateTime pFecha)
        {

            int cantRegistroAfectado = 0;

            DateTime fechaIngreso;
            String MensajeFinal = "";
            int estadoRegistro = 0;

            decimal montoConDescuento = 0;
            double montoDeIva = 0;
            decimal montoTotal = 0;


            decimal montoBruto = 0;

            decimal montoFInalIva;

            montoConDescuento = 0;


            montoDeIva = 0 * 0.13;
            montoFInalIva = (Decimal)montoDeIva;

            montoTotal = montoConDescuento + montoFInalIva;


            fechaIngreso = DateTime.Now;

            try
            {
                cantRegistroAfectado = this.ModeloBD.sp_Inserta_Entrada(
                    pIdProveedor,
                    pIdUsuario,
                    pNumFactura,
                    pFecha,
                    fechaIngreso,
                    montoBruto,
                    montoConDescuento,
                    montoFInalIva,
                    montoTotal
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
                    MensajeFinal = "La Entrada Se Regitro Exitosamente!";
                    estadoRegistro = 1;
                }
                else
                {
                    MensajeFinal += " No Se Pudo Registrar La Entrada";
                    estadoRegistro = 0;
                }
            }

            return Json(new
            {
                resultado = MensajeFinal,
                estado = estadoRegistro
            });
        }

        // GET: Entradas/Edit/5
        public ActionResult ModificaEntrada(int id_Entr)
        {
            sp_Retorna_EntradaID_Result CollectionObtenido = new sp_Retorna_EntradaID_Result();
            CollectionObtenido = this.ModeloBD.sp_Retorna_EntradaID(id_Entr).FirstOrDefault();

            this.agregaProveedores();
            this.agregaUsuarios();

            return View(CollectionObtenido);
        }

        // POST: Entradas/Edit/5
        [HttpPost]
        public ActionResult ModificaEntrada(sp_Retorna_EntradaID_Result modeloVista)
        {
            int cantRegistroAfectado = 0;
            string resultado = "";

            DateTime fechaIngreso;

            decimal montoConDescuento;
            double montoDeIva;
            decimal montoTotal;
            double montoBrutoF;

            decimal montoFInalIva;

            montoBrutoF = (double)modeloVista.Monto_Bruto;

            montoConDescuento = modeloVista.Monto_Bruto - modeloVista.Monto_Descuento;


            montoDeIva = montoBrutoF * 0.13;
            montoFInalIva = (Decimal)montoDeIva;

            montoTotal = montoConDescuento + montoFInalIva;


            fechaIngreso = DateTime.Now;

            try
            {
                cantRegistroAfectado = this.ModeloBD.sp_Modifica_Entrada(
                    modeloVista.ID_Entrada,
                    modeloVista.ID_Proveedor,
                    modeloVista.ID_Usuario,
                    modeloVista.Numero_Factura,
                    modeloVista.Fecha_Factura,
                    modeloVista.Fecha_Registro,
                    modeloVista.Monto_Bruto,
                    modeloVista.Monto_Descuento,
                    montoFInalIva,
                    montoTotal
                    );

                return RedirectToAction("ListaEntradas");
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
                    resultado += "No se pudo Modifcar";
                }
            }
            Response.Write("<script languaje=javascript>alert('" + resultado + "');</script>");
            this.agregaProveedores();
            this.agregaUsuarios();

            return View();
        }
        //Get: Entradas/Mostrar
        public ActionResult MostrarEntrada(int id_Entr)
        {
            ///Obtener Listado De Entrada Con Detalle
            List<sp_Retorna_FacturaID_Result> collectionObtenido = new List<sp_Retorna_FacturaID_Result>();
            collectionObtenido = this.ModeloBD.sp_Retorna_FacturaID(id_Entr).ToList();
            ///

            string Mensaje = id_Entr.ToString();
            /*Session["Mensaje"]*/
            ViewBag.NumeroFactura = Mensaje;

            this.agregaEntradaMontos(id_Entr);
            this.agregaProveedores();
            this.agregaUsuarios();

            return View(collectionObtenido);
        }

        // GET: Entradas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Entradas/Delete/5
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

        public ActionResult ImprimirEntrada(int idEntradaR)
        {
            List<sp_Retorna_FacturaID_Result> vistaObtenida = new List<sp_Retorna_FacturaID_Result>();
            vistaObtenida = this.ModeloBD.sp_Retorna_FacturaID(idEntradaR).ToList();

            string Mensaje = idEntradaR.ToString();
            /*Session["Mensaje"]*/
            ViewBag.NumeroFactura = Mensaje;

            this.agregaUsuarios();
            this.agregaProductos();
            this.agregaEntradaMontos(idEntradaR);

            return View(vistaObtenida);
        }


        public ActionResult GenerarReporte(int idEntradaR)
        {
            List<sp_Retorna_FacturaID_Result> vistaObtenida = new List<sp_Retorna_FacturaID_Result>();
            vistaObtenida = this.ModeloBD.sp_Retorna_FacturaID(idEntradaR).ToList();

            string Mensaje = idEntradaR.ToString();
            /*Session["Mensaje"]*/
            ViewBag.NumeroFactura = Mensaje;

            this.agregaUsuarios();
            this.agregaProductos();
            this.agregaEntradaMontos(idEntradaR);

            return View(vistaObtenida);


           
        }

        void agregaEntradaMontos(int idEntrada)
        {
            this.ViewBag.ListaEntradas = this.ModeloBD.sp_Retorna_EntradaID(idEntrada).ToList();
        }
        void agregaProveedores()
        {
            this.ViewBag.ListaProveedores = this.ModeloBD.sp_Retorna_Proveedores(null).ToList();
        }

        void agregaUsuarios()
        {
            this.ViewBag.ListaUsuarios = this.ModeloBD.sp_Retorna_Usuario(null, null).ToList();
        }

        void agregaUsuariosID()
        {
            if (Session["IdUsuario"] != null)
            {
                string idConvertido = Session["IdUsuario"].ToString();
                int idUsuarioActual = int.Parse(idConvertido);
                this.ViewBag.ListaUsuariosID = this.ModeloBD.sp_Retorna_UsuarioID(idUsuarioActual, null, null).ToList();

            }
            else
            {
                this.ViewBag.ListaUsuariosID = this.ModeloBD.sp_Retorna_UsuarioID(null, null, null).ToList();
            }
        }

        void agregaProductos()
        {
            this.ViewBag.ListaProductos = this.ModeloBD.sp_Retorna_Productos(null, null).ToList();
        }
    }
}
