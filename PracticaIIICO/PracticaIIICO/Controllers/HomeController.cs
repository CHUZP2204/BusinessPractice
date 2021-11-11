using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracticaIIICO.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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
    }
}