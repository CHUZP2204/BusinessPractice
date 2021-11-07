using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaIIICO.BD;

namespace PracticaIIICO.Controllers.Proveedores
{
    public class ProveedoresController : Controller
    {
        MotoRepuestosMakoEntities ModeloBD = new MotoRepuestosMakoEntities();

        // GET: Proveedores
        public ActionResult ListaProveedores()
        {
            List<sp_Retorna_Proveedores_Result> listaProveedores = new List<sp_Retorna_Proveedores_Result>();
            listaProveedores = this.ModeloBD.sp_Retorna_Proveedores(null).ToList();

            return View(listaProveedores);
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
