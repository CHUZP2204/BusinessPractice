using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaIIICO.BD;
using PracticaIIICO.Filters;
using PracticaIIICO.ViewModels;

namespace PracticaIIICO.Controllers.TipoUsuarios
{
    
    public class TipoUsuariosController : Controller
    {
        MotoRepuestosMakoEntities ModeloBD = new MotoRepuestosMakoEntities();

        // GET: TipoUsuarios
        public ActionResult Index()
        {
            return View();
        }

        // GET: TipoUsuarios/Details/5
        [AuthorizeUser]
        public ActionResult ListTypeUser(int pagina = 1)
        {
            List<sp_Retorna_TipoUsuario_Result> modeloObtenido = new List<sp_Retorna_TipoUsuario_Result>();
            modeloObtenido = this.ModeloBD.sp_Retorna_TipoUsuario(null).ToList();

            //Con Paginador
            var cantidadRegistroPorPagina = 5; //parametro Para limitar la cantidad de elementos al mostrar
            using (var db = new MotoRepuestosMakoEntities())
            {
                var TusuariosObtenidos = db.sp_Retorna_TipoUsuario(null).Skip((pagina - 1) * cantidadRegistroPorPagina)
                    .Take(cantidadRegistroPorPagina).ToList();

                var totalRegistros = db.sp_Retorna_TipoUsuario(null).Count();

                var modelo = new ViewTipoUsuarioModel();

                modelo.ListaTipoUsuario = TusuariosObtenidos;
                modelo.PaginaActual = pagina;
                modelo.TotalRegistros = totalRegistros;
                modelo.RegistrosPorPagina = cantidadRegistroPorPagina;

                return View(modelo);
            }
            //Usar Sin Paginador
            //return View(modeloObtenido);
        }

        // GET: TipoUsuarios/Create
        [AuthorizeUser]
        public ActionResult NuevoTipoUsuario()
        {
            return View();
        }

        // POST: TipoUsuarios/Create
        [HttpPost]
        public ActionResult NuevoTipoUsuario(sp_Retorna_TipoUsuario_ID_Result modeloVista)
        {
            int cantRegistroAfectado = 0;
            string resultado = "";
            try
            {
                cantRegistroAfectado = this.ModeloBD.sp_Inserta_TipoUsuario(
                    modeloVista.Nombre_TipoUsuario
                    );

                return RedirectToAction("ListTypeUser");
            }
            catch(Exception errorObtenido)
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

        // GET: TipoUsuarios/Edit/5
        [AuthorizeUser]
        public ActionResult ModificaTipoUsuario(int id_TipoU)
        {
            sp_Retorna_TipoUsuario_ID_Result modeloObtenido = new sp_Retorna_TipoUsuario_ID_Result();

            modeloObtenido = this.ModeloBD.sp_Retorna_TipoUsuario_ID(id_TipoU).FirstOrDefault();

            return View(modeloObtenido);
        }

        // POST: TipoUsuarios/Edit/5
        [HttpPost]
        public ActionResult ModificaTipoUsuario(sp_Retorna_TipoUsuario_ID_Result collection)
        {
            int cantRegistroAfectado = 0;
            string resultado = "";
            try
            {
                cantRegistroAfectado = this.ModeloBD.sp_Modifica_TipoUsuario(
                    collection.ID_TipoUsuario,
                    collection.Nombre_TipoUsuario
                    );

                return RedirectToAction("ListTypeUser");
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
                    resultado += "No se pudo Modificar";
                }
            }
            Response.Write("<script languaje=javascript>alert('" + resultado + "');</script>");
            return View();
        }

        // GET: TipoUsuarios/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TipoUsuarios/Delete/5
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
