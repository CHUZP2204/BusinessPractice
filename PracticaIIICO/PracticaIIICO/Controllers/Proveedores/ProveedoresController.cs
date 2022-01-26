using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaIIICO.BD;
using PracticaIIICO.ViewModels;

namespace PracticaIIICO.Controllers.Proveedores
{
    public class ProveedoresController : Controller
    {
        MotoRepuestosMakoEntities ModeloBD = new MotoRepuestosMakoEntities();

        // GET: Proveedores
        public ActionResult ListaProveedores(int pagina = 1)
        {
            List<sp_Retorna_Proveedores_Result> listaProveedores = new List<sp_Retorna_Proveedores_Result>();
            listaProveedores = this.ModeloBD.sp_Retorna_Proveedores(null).ToList();

            //Con Paginador
            var cantidadRegistroPorPagina = 5; //parametro Para limitar la cantidad de elementos al mostrar
            using (var db = new MotoRepuestosMakoEntities())
            {
                var proveedoresObtenidos = db.sp_Retorna_Proveedores(null).Skip((pagina - 1) * cantidadRegistroPorPagina)
                    .Take(cantidadRegistroPorPagina).ToList();

                var totalRegistros = db.sp_Retorna_Proveedores(null).Count();

                var modelo = new ViewProveedoresModel();

                modelo.ListaProveedores = proveedoresObtenidos;
                modelo.PaginaActual = pagina;
                modelo.TotalRegistros = totalRegistros;
                modelo.RegistrosPorPagina = cantidadRegistroPorPagina;

                return View(modelo);
            }
            //Usar Sin Paginador
            //return View(listaProveedores);
        }

        // GET: Proveedores/Details/5
        
        public ActionResult Details(int id_Prov)
        {
            
            return View();
        }

        // GET: Proveedores/Create
        public ActionResult NuevoProveedor()
        {
            return View();
        }

        // POST: Proveedores/Create
        [HttpPost]
        public ActionResult NuevoProveedor(sp_Retorna_Proveedores_Result collection)
        {
            int cantidadRegistrosAfectados = 0;
            string resultado = " ";
            bool estadoProv = true;
            try
            {
                cantidadRegistrosAfectados = 
                this.ModeloBD.sp_Inserta_Proveedor(
                    collection.Nombre_PROV,
                    collection.Direccion_PROV,
                    collection.Correo_PROV,
                    collection.Telefono_PROV,
                    estadoProv);
            }
            catch(Exception errorObtenido)
            {
                resultado = "Ocurrio Un Error: "+errorObtenido.Message;
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

        [HttpPost]
        public ActionResult RegistrarPROVModal(
            string pNombrePROV,
            string pCorreoPROV,
            string pDireccionPROV,
            string pTelefonoPROV)
        {
            int cantRegistrosAfectados = 0;
            
            bool estadoProv = true;

            //Mensajes para JSON

            String MensajeFinal = "";
            int estadoRegistro = 0;
            try
            {
                cantRegistrosAfectados =
                this.ModeloBD.sp_Inserta_Proveedor(
                    pNombrePROV,
                    pDireccionPROV,
                    pCorreoPROV,
                    pTelefonoPROV,
                    estadoProv);
            }
            catch (Exception error)
            {
                MensajeFinal = "Ocurrio Un Error" + error.Message;
            }
            finally
            {
                if (cantRegistrosAfectados > 0)
                {
                    MensajeFinal = "El Proveedor " + pNombrePROV + " Se Regitro Exitosamente";
                    estadoRegistro = 1;
                }
                else
                {
                    MensajeFinal += " No Se Pudo Registrar El proveedor";
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

        // GET: Proveedores/Edit/5
        public ActionResult ModificaProveedor(int id_Prov)
        {
            sp_Retorna_ProveedorID_Result modeloVista = new sp_Retorna_ProveedorID_Result();
            modeloVista = this.ModeloBD.sp_Retorna_ProveedorID(id_Prov).FirstOrDefault();

            return View(modeloVista);
        }

        // POST: Proveedores/Edit/5
        [HttpPost]
        public ActionResult ModificaProveedor(sp_Retorna_ProveedorID_Result collection)
        {

            int cantidadRegistrosAfectados = 0;
            string resultado = " ";
            
            try
            {
                cantidadRegistrosAfectados =
                this.ModeloBD.sp_Modifica_ProveedorID(
                    collection.ID_Proveedor,
                    collection.Nombre_PROV,
                    collection.Direccion_PROV,
                    collection.Correo_PROV,
                    collection.Telefono_PROV,
                    collection.Estado_PROV);
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

        public ActionResult MostrarProveedor(int id_Prov)
        {
            sp_Retorna_ProveedorID_Result modeloVista = new sp_Retorna_ProveedorID_Result();
            modeloVista = this.ModeloBD.sp_Retorna_ProveedorID(id_Prov).FirstOrDefault();

            return View(modeloVista);
        }

        // GET: Proveedores/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Proveedores/Delete/5
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
