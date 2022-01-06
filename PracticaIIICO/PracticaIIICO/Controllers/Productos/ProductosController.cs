using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaIIICO.BD;
using PagedList;
using PracticaIIICO.ViewModels;

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


        public ActionResult ListaProductos(int pagina = 1)
        {
            //Sin Paginador
            List<sp_Retorna_Productos_Result> datosObtenidos = new List<sp_Retorna_Productos_Result>();
            datosObtenidos = this.ModeloBD.sp_Retorna_Productos(null, null).ToList();

            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }

            this.agregaCategorias();
            //Parte Con Paginador

            var cantidadRegistroPorPagina = 5; //parametro
            using (var db = new MotoRepuestosMakoEntities())
            {
                var productos = db.sp_Retorna_Productos(null, null).Skip((pagina - 1) * cantidadRegistroPorPagina)
                    .Take(cantidadRegistroPorPagina).ToList();

                var totalRegistros = db.sp_Retorna_Productos(null, null).Count();

                var modelo = new IndexViewModel();

                modelo.Productos = productos;
                modelo.PaginaActual = pagina;
                modelo.TotalRegistros = totalRegistros;
                modelo.RegistrosPorPagina = cantidadRegistroPorPagina;

                return View(modelo);
            }
            //

            //Return en caso De No Usar Paginador
            //return View(datosObtenidos);
        }

        //Paginacion
        public ActionResult Listado2(int pagina = 1)
        {
            var cantidadRegistroPorPagina = 5; //parametro
            using (var db = new MotoRepuestosMakoEntities())
            {
                var productos = db.sp_Retorna_Productos(null, null).Skip((pagina - 1) * cantidadRegistroPorPagina)
                    .Take(cantidadRegistroPorPagina).ToList();

                var totalRegistros = db.sp_Retorna_Productos(null, null).Count();

                var modelo = new IndexViewModel();

                modelo.Productos = productos;
                modelo.PaginaActual = pagina;
                modelo.TotalRegistros = totalRegistros;
                modelo.RegistrosPorPagina = cantidadRegistroPorPagina;

                return View(modelo);
            }
        }
        //Paginacion FIN

        public ActionResult CatalogoProductos(int pagina = 1)
        {
            List<sp_Retorna_ProductsImages_Result> modeloVista = new List<sp_Retorna_ProductsImages_Result>();
            modeloVista = this.ModeloBD.sp_Retorna_ProductsImages(null).ToList();

            var cantidadRegistroPorPagina = 4; //parametro
            using (var db = new MotoRepuestosMakoEntities())
            {
                var productos = db.sp_Retorna_ProductsImages(null).Skip((pagina - 1) * cantidadRegistroPorPagina)
                    .Take(cantidadRegistroPorPagina).ToList();

                var totalRegistros = db.sp_Retorna_ProductsImages(null).Count();

                var modelo = new ViewImagenesModel();

                modelo.ModeloImagensPROD = productos;
                modelo.PaginaActual = pagina;
                modelo.TotalRegistros = totalRegistros;
                modelo.RegistrosPorPagina = cantidadRegistroPorPagina;

                return View(modelo);
            }

            //return View(modeloVista);
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
                    estadoPROD,
                    collection.Cantidad_PROD
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
                    collection.Estado_PROD,
                    collection.Cantidad_PROD
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

        /// <summary>
        /// Registrar Producto Desde un Modal
        /// </summary>

        //Registrar Usuario Desde Login
        [HttpPost]
        public ActionResult RegistrarProducto(
            int pID_Categoria,
            string pNombre_PROD,
            decimal pPrecio_PROD,
            string pDescripcion_PROD,
            int pCantidad_PROD)
        {
            ///Variable Que Registra La Cantidad De Registros Afectados
            ///Si Un Procedimiento Que Ejecuta Insert, Update o Delete
            ///No Afecta Registros Implica Que Hubo Un Error


            bool pEstadoPROD = true;

            int cantRegistrosAfectados = 0;

            //Mensajes para JSON

            String MensajeFinal = "";
            int estadoRegistro = 0;
            try
            {

                cantRegistrosAfectados = this.ModeloBD.sp_Inserta_Products(
                     pID_Categoria,
                     pNombre_PROD,
                     pPrecio_PROD,
                     pDescripcion_PROD,
                     pEstadoPROD,
                     pCantidad_PROD
                     );



            }
            catch (Exception error)
            {
                MensajeFinal = "Ocurrio Un Error" + error.Message;
            }
            finally
            {
                if (cantRegistrosAfectados > 0)
                {
                    MensajeFinal = "El Producto " + pNombre_PROD + " Se Regitro Exitosamente";
                    estadoRegistro = 1;
                }
                else
                {
                    MensajeFinal += " No Se Pudo Registrar El producto";
                    estadoRegistro = 0;
                }
            }

            //return Json(resultado);

            return Json(new
            {
                resultado = MensajeFinal,
                estado = estadoRegistro
            });
        }

        void agregaCategorias()
        {
            this.ViewBag.ListaCategorias = this.ModeloBD.sp_Retorna_Categorias(null, null).ToList();
        }

    }

}

