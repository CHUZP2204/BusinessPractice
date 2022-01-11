using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaIIICO.BD;
using PracticaIIICO.ViewModels;

namespace PracticaIIICO.Controllers.Citas
{
    public class CitasController : Controller
    {
        MotoRepuestosMakoEntities ModeloBD = new MotoRepuestosMakoEntities();

        // GET: Citas
        public ActionResult Index()
        {
            return View();
        }

        // GET: Citas/Details/5
        public ActionResult ListaCitas(int pagina = 1)
        {
            List<sp_Retorna_Citas_Result> listaCitas = new List<sp_Retorna_Citas_Result>();
            listaCitas = this.ModeloBD.sp_Retorna_Citas(null, null).ToList();

            this.agregaMarcas();
            this.agregaUsuarios();

            //Con Paginador
            var cantidadRegistroPorPagina = 5; //parametro Para limitar la cantidad de elementos al mostrar
            using (var db = new MotoRepuestosMakoEntities())
            {
                var citasObtenidas = db.sp_Retorna_Citas(null, null).Skip((pagina - 1) * cantidadRegistroPorPagina)
                    .Take(cantidadRegistroPorPagina).ToList();

                var totalRegistros = db.sp_Retorna_Citas(null, null).Count();

                var modelo = new ViewCitasModel();

                modelo.ListaCitas = citasObtenidas;
                modelo.PaginaActual = pagina;
                modelo.TotalRegistros = totalRegistros;
                modelo.RegistrosPorPagina = cantidadRegistroPorPagina;

                return View(modelo);
            }
            //

            //Usar sin paginador return View(listaCitas);
        }

        // GET: Citas/Create
        public ActionResult NuevaCita()
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

            this.agregaMarcas();
            this.agregaUsuarios();
            this.agregaUsuariosID();


            return View();
        }

        // POST: Citas/Create
        [HttpPost]
        public ActionResult NuevaCita(sp_Retorna_Citas_Result vistaObtenida)
        {
            int cantRegistroAfectado = 0;
            string resultado = "";
            DateTime fechaIngreso;
            fechaIngreso = DateTime.Now;

            try
            {
                // TODO: Add insert logic here

                cantRegistroAfectado = this.ModeloBD.sp_Inserta_Citas(
                    vistaObtenida.ID_Usuario,
                    vistaObtenida.ID_Marca,
                    vistaObtenida.Nombre_Cliente,
                    vistaObtenida.Numero_Cliente,
                    vistaObtenida.Placa_Moto,
                    vistaObtenida.Fecha_Cita,
                    vistaObtenida.Hora_Cita,
                    fechaIngreso
                    );

                return RedirectToAction("ListaCitas");
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
            this.agregaMarcas();
            return View(vistaObtenida);
        }

        // GET: Citas/Edit/5
        public ActionResult ModificaCita(int id_Cita)
        {
            sp_Retorna_CitasID_Result modeloVista = new sp_Retorna_CitasID_Result();
            modeloVista = this.ModeloBD.sp_Retorna_CitasID(id_Cita).FirstOrDefault();

            this.agregaUsuarios();
            this.agregaMarcas();

            return View(modeloVista);
        }

        // POST: Citas/Edit/5
        [HttpPost]
        public ActionResult ModificaCita(sp_Retorna_CitasID_Result vistaObtenida)
        {
            int cantRegistroAfectado = 0;
            string resultado = "";
            DateTime fechaIngreso;
            fechaIngreso = DateTime.Now;


            try
            {
                // TODO: Add update logic here
                cantRegistroAfectado = this.ModeloBD.sp_Modifica_Citas(
                    vistaObtenida.ID_Cita,
                    vistaObtenida.ID_Usuario,
                    vistaObtenida.ID_Marca,
                    vistaObtenida.Nombre_Cliente,
                    vistaObtenida.Numero_Cliente,
                    vistaObtenida.Placa_Moto,
                    vistaObtenida.Fecha_Cita,
                    vistaObtenida.Hora_Cita,
                    vistaObtenida.Fecha_Ingreso
                    );
                return RedirectToAction("ListaCitas");
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
            this.agregaMarcas();
            return View(vistaObtenida);
        }
        //
        public ActionResult MostrarCita(int id_Cita)
        {
            sp_Retorna_CitasID_Result datosObtenidos = new sp_Retorna_CitasID_Result();
            datosObtenidos = this.ModeloBD.sp_Retorna_CitasID(id_Cita).FirstOrDefault();

            this.agregaMarcas();
            this.agregaUsuarios();

            return View(datosObtenidos);
        }

        // GET: Citas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Citas/Delete/5
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

        void agregaUsuarios()
        {
            this.ViewBag.ListaUsuarios = this.ModeloBD.sp_Retorna_Usuario(null, null).ToList();
        }
        void agregaUsuariosID()
        {
            string idConvertido = Session["IdUsuario"].ToString();
            int idUsuarioActual = int.Parse(idConvertido);
            this.ViewBag.ListaUsuariosID = this.ModeloBD.sp_Retorna_UsuarioID(idUsuarioActual,null, null).ToList();
        }
        void agregaMarcas()
        {
            this.ViewBag.ListaMarcas = this.ModeloBD.sp_Retorna_Marcas(null, null).ToList();
        }
    }
}
