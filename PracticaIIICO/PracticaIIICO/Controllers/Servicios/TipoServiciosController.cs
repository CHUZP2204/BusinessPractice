using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaIIICO.BD;
using PracticaIIICO.ViewModels;

namespace PracticaIIICO.Controllers.TipoServicios
{
    public class TipoServiciosController : Controller
    {
        MotoRepuestosMakoEntities ModeloBD = new MotoRepuestosMakoEntities();

        // GET: TipoServicios
        public ActionResult Index()
        {
            return View();
        }

        // GET: TipoServicios/Details/5
        public ActionResult ListTypeServices(int pagina = 1)
        {
            List<sp_Retorna_Tipo_Servicio_Result> datosObtenidos = new List<sp_Retorna_Tipo_Servicio_Result>();
            datosObtenidos = this.ModeloBD.sp_Retorna_Tipo_Servicio(null, null).ToList();

            //Con Paginador
            var cantidadRegistroPorPagina = 5; //parametro Para limitar la cantidad de elementos al mostrar
            using (var db = new MotoRepuestosMakoEntities())
            {
                var TserviciosObtenidos = db.sp_Retorna_Tipo_Servicio(null, null).Skip((pagina - 1) * cantidadRegistroPorPagina)
                    .Take(cantidadRegistroPorPagina).ToList();

                var totalRegistros = db.sp_Retorna_Tipo_Servicio(null, null).Count();

                var modelo = new ViewTiposervicioModel();

                modelo.ListaTipoServicio = TserviciosObtenidos;
                modelo.PaginaActual = pagina;
                modelo.TotalRegistros = totalRegistros;
                modelo.RegistrosPorPagina = cantidadRegistroPorPagina;

                return View(modelo);
            }
            //Usar Sin Paginador
            //return View(datosObtenidos);
        }

        // GET: TipoServicios/Create
        public ActionResult NuevoTipoS()
        {
            return View();
        }

        // POST: TipoServicios/Create
        [HttpPost]
        public ActionResult NuevoTipoS(sp_Retorna_Tipo_Servicio_Result collection)
        {
            int cantRegistroAfectado = 0;
            string resultado = "";

            try
            {
                cantRegistroAfectado = this.ModeloBD.sp_Inserta_TipoSERV(
                    collection.Nombre_TipoServicio,
                    collection.Descripcion_TipoServicio
                    );

                return RedirectToAction("ListTypeServices");
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

        // POST: TipoServicios/Create Modal
        [HttpPost]
        public ActionResult NuevoTipoSERVModal(string pNombreTipoSERV, string pDescripcionTSERV)
        {
            int cantRegistroAfectado = 0;
            //Mensajes para JSON

            String MensajeFinal = "";
            int estadoRegistro = 0;
            try
            {
                cantRegistroAfectado = this.ModeloBD.sp_Inserta_TipoSERV(
                    pNombreTipoSERV,
                    pDescripcionTSERV
                    );
            }
            catch (Exception errorObtenido)
            {
                MensajeFinal = "Ocurrio Un Error " + errorObtenido.Message;
            }
            finally
            {
                if (cantRegistroAfectado > 0)
                {
                    MensajeFinal = "El Tipo Servicio " + pNombreTipoSERV + " Se Regitro Exitosamente!";
                    estadoRegistro = 1;
                }
                else
                {
                    MensajeFinal += " No Se Pudo Registrar";
                    estadoRegistro = 0;
                }
            }

            return Json(new
            {
                resultado = MensajeFinal,
                estado = estadoRegistro
            });
        }

        // GET: TipoServicios/Edit/5
        public ActionResult ModificaTipoS(int id_TipoS)
        {
            sp_Retorna_Tipo_SERV_ID_Result datosObtenidos = new sp_Retorna_Tipo_SERV_ID_Result();
            datosObtenidos = this.ModeloBD.sp_Retorna_Tipo_SERV_ID(id_TipoS).FirstOrDefault();

            return View(datosObtenidos);
        }

        // POST: TipoServicios/Edit/5
        [HttpPost]
        public ActionResult ModificaTipoS(sp_Retorna_Tipo_SERV_ID_Result collection)
        {
            int cantRegistroAfectado = 0;
            string resultado = "";

            try
            {
                cantRegistroAfectado = this.ModeloBD.sp_Modifica_TipoSERV(
                    collection.ID_TipoServicio,
                    collection.Nombre_TipoServicio,
                    collection.Descripcion_TipoServicio
                    );

                return RedirectToAction("ListTypeServices");
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

            return View(collection);
        }

        public ActionResult MostrarTipoS(int id_TipoS)
        {
            sp_Retorna_Tipo_SERV_ID_Result datosObtenidos = new sp_Retorna_Tipo_SERV_ID_Result();
            datosObtenidos = this.ModeloBD.sp_Retorna_Tipo_SERV_ID(id_TipoS).FirstOrDefault();

            return View(datosObtenidos);

        }

        // GET: TipoServicios/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TipoServicios/Delete/5
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
