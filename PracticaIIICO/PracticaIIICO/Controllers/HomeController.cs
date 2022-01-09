using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaIIICO.BD;
using PracticaIIICO.ViewModels;

namespace PracticaIIICO.Controllers
{
    public class HomeController : Controller
    {
        MotoRepuestosMakoEntities ModeloBD = new MotoRepuestosMakoEntities();
        public ActionResult Index()
        {
            List<sp_Retorna_Productos_Result> modeloProductosBD = new List<sp_Retorna_Productos_Result>();

            modeloProductosBD = this.ModeloBD.sp_Retorna_Productos(null, null).ToList();

            List<sp_Retorna_Productos_Result> modeloPRODAgotados = new List<sp_Retorna_Productos_Result>();

            string mensajeAgotados = "";

            for (int i = 0; i < modeloProductosBD.Count; i++)
            {
                if (modeloProductosBD[i].Cantidad_PROD >= 0 && modeloProductosBD[i].Cantidad_PROD <= 10)
                {
                    //Creamos un objeto de tipo Productos cuando el if se cumpla
                    //Lo anterior realizara o obtendra los datos del inventario que esten 
                    //en el rango de 0 a 10, que seria el inventario bajo
                    sp_Retorna_Productos_Result productoAgotado = new sp_Retorna_Productos_Result();

                    // agregamos los datos delproducto que esta vacio o bajo nivel 
                    productoAgotado.ID_Producto = modeloProductosBD[i].ID_Producto;
                    productoAgotado.ID_Categoria = modeloProductosBD[i].ID_Categoria;
                    productoAgotado.Nombre_PROD = modeloProductosBD[i].Nombre_PROD;
                    productoAgotado.Precio_PROD = modeloProductosBD[i].Precio_PROD;
                    productoAgotado.Descripcion_PROD = modeloProductosBD[i].Descripcion_PROD;
                    productoAgotado.Estado_PROD = modeloProductosBD[i].Estado_PROD;
                    productoAgotado.Cantidad_PROD = modeloProductosBD[i].Cantidad_PROD;

                    modeloPRODAgotados.Add(productoAgotado);
                }


            }
            if (modeloPRODAgotados != null)
            {
                Session["AlertasCantidad"] = modeloPRODAgotados.Count;
            }
            else
            {
                Session["AlertasCantidad"] = null;
            }

           

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Configuracion()
        {
            return View();
        }

        //Evento que Bloquea El Menu Lateral
        [HttpPost]
        public ActionResult RetornaOpcionSidebarBloqueo()
        {
            Session["SidebarFijo"] = "fijar";
            String mensaje = "Se Cambio el estado del SideBar Fijo";


            return Json(new
            {
                resultado = mensaje
            });


        }
        //Evento que Desbloquea El Menu Lateral
        [HttpPost]
        public ActionResult RetornaOpcionSidebarDesbloqueo()
        {
            //string mostrarSession = Session["SidebarFijo"].ToString(); //Obtenia el valor Actual De La Variable Session
            Session["SidebarFijo"] = null;


            String mensaje = "Se Cambio el estado del SideBar a flexible";


            return Json(new
            {
                resultado = mensaje
            });


        }

        public ActionResult Alertas()
        {

            List<sp_Retorna_Productos_Result> modeloProductosBD = new List<sp_Retorna_Productos_Result>();

            modeloProductosBD = this.ModeloBD.sp_Retorna_Productos(null, null).ToList();

            List<sp_Retorna_Productos_Result> modeloPRODAgotados = new List<sp_Retorna_Productos_Result>();

            string mensajeAgotados = "";

            for (int i = 0; i < modeloProductosBD.Count; i++)
            {
                if (modeloProductosBD[i].Cantidad_PROD >= 0 && modeloProductosBD[i].Cantidad_PROD <= 10)
                {
                    //Creamos un objeto de tipo Productos cuando el if se cumpla
                    //Lo anterior realizara o obtendra los datos del inventario que esten 
                    //en el rango de 0 a 10, que seria el inventario bajo
                    sp_Retorna_Productos_Result productoAgotado = new sp_Retorna_Productos_Result();

                    // agregamos los datos delproducto que esta vacio o bajo nivel 
                    productoAgotado.ID_Producto = modeloProductosBD[i].ID_Producto;
                    productoAgotado.ID_Categoria = modeloProductosBD[i].ID_Categoria;
                    productoAgotado.Nombre_PROD = modeloProductosBD[i].Nombre_PROD;
                    productoAgotado.Precio_PROD = modeloProductosBD[i].Precio_PROD;
                    productoAgotado.Descripcion_PROD = modeloProductosBD[i].Descripcion_PROD;
                    productoAgotado.Estado_PROD = modeloProductosBD[i].Estado_PROD;
                    productoAgotado.Cantidad_PROD = modeloProductosBD[i].Cantidad_PROD;

                    modeloPRODAgotados.Add(productoAgotado);
                }


            }

            if (modeloPRODAgotados != null)
            {
                Session["AlertasCantidad"] = modeloPRODAgotados.Count;
            }
            else
            {
                Session["AlertasCantidad"] = null;
            }

            @TempData["MessageAgotados"] = "El siguiente producto Se Encuentran Agotado"; //Es Parecido al ViewBag Pero Vive mas

            //alerta Mis Datos
            sp_Retorna_UsuarioID_Result UsuarioActual = new sp_Retorna_UsuarioID_Result();

            sp_Retorna_UsuarioID_Result UsuarioSinDatos = new sp_Retorna_UsuarioID_Result();

            UsuarioActual = (sp_Retorna_UsuarioID_Result)Session["UsuarioActual"];

            string mensajeAlerta = "";
            int estado = 0;

            if (UsuarioActual != null)
            {
                if (UsuarioActual.Direccion_U.Equals(""))
                {
                    mensajeAlerta = "Falta Registrar Tu Direccion";
                    estado = 1;
                }
                if (UsuarioActual.Telefono_U.Equals(""))
                {
                    mensajeAlerta +=" Falta Registrar Tu Numero Telefono";
                    estado = 1;
                }
            }
            else
            {
                mensajeAlerta = "Sin Problemas Con Sus Datos";
                estado = 0;
            }
            this.ViewBag.EstadoAlertaDatos = estado;
            this.ViewBag.AlertaMisdatos = mensajeAlerta;

            return View(modeloPRODAgotados);
        }


    }
}