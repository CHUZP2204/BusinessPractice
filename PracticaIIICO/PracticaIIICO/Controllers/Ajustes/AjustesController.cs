using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaIIICO.BD;
using PracticaIIICO.ViewModels;

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
        public ActionResult ListAjustes(int pagina = 1)
        {
            //Sin Paginador
            List<sp_Retorna_Ajustes_Result> modelObtenido = new List<sp_Retorna_Ajustes_Result>();
            modelObtenido = this.ModeloBD.sp_Retorna_Ajustes(null).ToList();


            //Cuando Se Modifca STOCK
            if (TempData["MensajeStock"] != null)
            {
                ViewBag.MensajeStock = TempData["MensajeStock"].ToString();
            }

            this.agregaUsuariosID();
            this.agregaProductos();
            this.agregaUsuarios();

            //Con Paginador

            var cantidadRegistroPorPagina = 5; //parametro Para limitar la cantidad de elementos al mostrar
            using (var db = new MotoRepuestosMakoEntities())
            {
                var ajustesObtenidos = db.sp_Retorna_Ajustes(null).Skip((pagina - 1) * cantidadRegistroPorPagina)
                    .Take(cantidadRegistroPorPagina).ToList();

                var totalRegistros = db.sp_Retorna_Ajustes(null).Count();

                var modelo = new ViewAjustesModel();

                modelo.ListaAjustes = ajustesObtenidos;
                modelo.PaginaActual = pagina;
                modelo.TotalRegistros = totalRegistros;
                modelo.RegistrosPorPagina = cantidadRegistroPorPagina;

                return View(modelo);
            }

            //Sin Paginador / return View(modelObtenido);
        }

        // GET: Ajustes/Create
        public ActionResult NuevoAjuste()
        {
            if (Session["RoleUsuario"] != null)
            {
                string tipoUsuarioActual = Session["RoleUsuario"].ToString();

                this.ViewBag.UsuarioActual = tipoUsuarioActual;
            }
            

            this.agregaProductos();
            this.agregaUsuarios();
            this.agregaUsuariosID();
            return View();
        }

        // POST: Ajustes/Create
        [HttpPost]
        public ActionResult NuevoAjuste(sp_Retorna_Ajustes_Result modeloVista)
        {

            int cantRegistroAfectado = 0;
            int estadoStock = 0;
            string resultado = "";


            sp_Retorna_Products_ID_Result productoActual = new sp_Retorna_Products_ID_Result();
            productoActual = this.ModeloBD.sp_Retorna_Products_ID(modeloVista.ID_Producto).FirstOrDefault();

            //Calculos

            int stockActual = 0;
            int stockVista = 0;
            int stockFinal = 0;
            bool estadoInventario = false;
            stockVista = modeloVista.Cantida_Ajustar;
            stockActual = (int)productoActual.Cantidad_PROD;



            DateTime fechaIngreso;
            fechaIngreso = DateTime.Now;



            try
            {
                if (modeloVista.Tipo_Ajuste.Equals("Entrada"))
                {
                    stockFinal = stockActual + stockVista;
                    // TODO: Add insert logic here
                    cantRegistroAfectado = this.ModeloBD.sp_Inserta_Ajuste(
                        modeloVista.ID_Producto,
                        modeloVista.ID_Usuario,
                        modeloVista.Tipo_Ajuste,
                        modeloVista.Cantida_Ajustar,
                        fechaIngreso,
                        modeloVista.Descripcion_Ajsute
                        );
                    //Actualizar el Inventario el campo STOCK

                    estadoStock = this.ModeloBD.sp_Modifica_Products(
                     productoActual.ID_Producto,
                     productoActual.ID_Categoria,
                     productoActual.Nombre_PROD,
                     productoActual.Precio_PROD,
                     productoActual.Descripcion_PROD,
                     productoActual.Estado_PROD,
                     stockFinal
                   );

                    if (modeloVista.Tipo_Ajuste == "Entrada")
                    {
                        @TempData["MensajeStock"] = "Se Actualizo el STOCK del producto " + productoActual.Nombre_PROD + " \n " +
                        " Se Agregaron " + stockVista + " Unidades"; //Es Parecido al ViewBag Pero Vive mas
                    }

                    return RedirectToAction("ListAjustes");
                }
                else if (modeloVista.Tipo_Ajuste.Equals("Salida"))
                {
                    if (stockActual <= 0)
                    {
                        resultado += "No Hay Productos En Inventario: Cantidad Actual 0 ";
                        estadoInventario = true;
                    }
                    else if (stockActual < stockVista)
                    {
                        resultado += "La Cantidad Solicitada Supera la existencia del Producto \n" +
                                 " en el Inventario, Solo hay: " + stockActual + " productos en existencia";
                        estadoInventario = true;
                    }
                    else
                    {
                        stockFinal = stockActual - stockVista;

                        // TODO: Add insert logic here
                        cantRegistroAfectado = this.ModeloBD.sp_Inserta_Ajuste(
                            modeloVista.ID_Producto,
                            modeloVista.ID_Usuario,
                            modeloVista.Tipo_Ajuste,
                            modeloVista.Cantida_Ajustar,
                            fechaIngreso,
                            modeloVista.Descripcion_Ajsute
                            );
                        //Actualizar el Inventario el campo STOCK

                        estadoStock = this.ModeloBD.sp_Modifica_Products(
                         productoActual.ID_Producto,
                         productoActual.ID_Categoria,
                         productoActual.Nombre_PROD,
                         productoActual.Precio_PROD,
                         productoActual.Descripcion_PROD,
                         productoActual.Estado_PROD,
                         stockFinal
                       );
                        if (modeloVista.Tipo_Ajuste == "Salida")
                        {
                            @TempData["MensajeStock"] = "Se Actualizo el STOCK del producto " + productoActual.Nombre_PROD + " \n " +
                            " Se Retiraron " + stockVista + " Unidades"; //Es Parecido al ViewBag Pero Vive mas
                        }
                        return RedirectToAction("ListAjustes");
                    }

                }

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
            this.agregaUsuariosID();
            this.agregaProductos();
            return View();
        }

        //Regsitro Modal Ajustes

        [HttpPost]
        public ActionResult NuevoAjusteModal(
            int pIdProducto,
            int pIdUsuario,
            string pTipoAjuste,
            int pCantAjustar,
            string pDescripAjuste)
        {

            int cantRegistroAfectado = 0;
            int estadoStock = 0;


            String MensajeFinal = "";
            int estadoRegistro = 0;


            sp_Retorna_Products_ID_Result productoActual = new sp_Retorna_Products_ID_Result();
            productoActual = this.ModeloBD.sp_Retorna_Products_ID(pIdProducto).FirstOrDefault();

            //Calculos

            int stockActual = 0;
            int stockVista = 0;
            int stockFinal = 0;

            stockVista = pCantAjustar;
            stockActual = (int)productoActual.Cantidad_PROD;



            DateTime fechaIngreso;
            fechaIngreso = DateTime.Now;

            try
            {
                if (pTipoAjuste.Equals("Entrada"))
                {
                    stockFinal = stockActual + stockVista;

                    // TODO: Add insert logic here
                    cantRegistroAfectado = this.ModeloBD.sp_Inserta_Ajuste(
                       pIdProducto,
                       pIdUsuario,
                       pTipoAjuste,
                       pCantAjustar,
                       fechaIngreso,
                       pDescripAjuste
                        );
                    //Actualizar el Inventario el campo STOCK

                    estadoStock = this.ModeloBD.sp_Modifica_Products(
                     productoActual.ID_Producto,
                     productoActual.ID_Categoria,
                     productoActual.Nombre_PROD,
                     productoActual.Precio_PROD,
                     productoActual.Descripcion_PROD,
                     productoActual.Estado_PROD,
                     stockFinal
                   );

                    if (pTipoAjuste == "Entrada")
                    {
                        @TempData["MensajeStock"] = "Se Actualizo el STOCK del producto " + productoActual.Nombre_PROD + " \n " +
                        " Se Agregaron " + stockVista + " Unidades"; //Es Parecido al ViewBag Pero Vive mas
                    }
                }
                else if (pTipoAjuste.Equals("Salida"))
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
                        // TODO: Add insert logic here
                        cantRegistroAfectado = this.ModeloBD.sp_Inserta_Ajuste(
                           pIdProducto,
                           pIdUsuario,
                           pTipoAjuste,
                           pCantAjustar,
                           fechaIngreso,
                           pDescripAjuste
                            );
                        //Actualizar el Inventario el campo STOCK

                        estadoStock = this.ModeloBD.sp_Modifica_Products(
                         productoActual.ID_Producto,
                         productoActual.ID_Categoria,
                         productoActual.Nombre_PROD,
                         productoActual.Precio_PROD,
                         productoActual.Descripcion_PROD,
                         productoActual.Estado_PROD,
                         stockFinal
                       );

                        if (pTipoAjuste == "Salida")
                        {
                            @TempData["MensajeStock"] = "Se Actualizo el STOCK del producto " + productoActual.Nombre_PROD + " \n " +
                            " Se Retiraron " + stockVista + " Unidades"; //Es Parecido al ViewBag Pero Vive mas
                        }

                    }
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
                    if (pTipoAjuste == "Entrada")
                    {
                        MensajeFinal = "El Ajuste Se Regitro Exitosamente! " + "\n" +
                            " Se Actualizo el STOCK del producto " + productoActual.Nombre_PROD + " \n " +
                            " Se Agregaron " + stockVista + " Unidades"; //Es Parecido al ViewBag Pero Vive mas
                    }
                    else if (pTipoAjuste == "Salida")
                    {
                        MensajeFinal = "El Ajuste Se Regitro Exitosamente! " + "\n" +
                            "Se Actualizo el STOCK del producto " + productoActual.Nombre_PROD + " \n " +
                            "Se Retiraron " + stockVista + " Unidades"; //Es Parecido al ViewBag Pero Vive mas
                    }

                    estadoRegistro = 1;
                }
                else
                {
                    MensajeFinal += " No Se Pudo Registrar El Ajuste";
                    estadoRegistro = 0;
                }
            }

            return Json(new
            {
                resultado = MensajeFinal,
                estado = estadoRegistro
            });
        }

        // GET: Ajustes/Edit/5
        public ActionResult ModificaAjuste(int id_Aju)
        {

            sp_Retorna_AjusteID_Result datosObtenidos = new sp_Retorna_AjusteID_Result();
            datosObtenidos = this.ModeloBD.sp_Retorna_AjusteID(id_Aju).FirstOrDefault();

            this.agregaProductos();
            this.agregaUsuarios();
            this.agregaUsuariosID();

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
                this.agregaUsuariosID();
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

        void agregaUsuariosID()
        {
            if (Session["IdUsuario"] != null)
            {
                string idConvertido = Session["IdUsuario"].ToString();
                int idUsuarioActual = int.Parse(idConvertido);
                this.ViewBag.ListaUsuarioID = this.ModeloBD.sp_Retorna_UsuarioID(idUsuarioActual, null, null).ToList();
            }
            else
            {
                this.ViewBag.ListaUsuarioID = this.ModeloBD.sp_Retorna_UsuarioID(null, null, null).ToList();
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
