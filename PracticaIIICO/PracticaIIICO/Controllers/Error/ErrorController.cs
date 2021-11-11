using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracticaIIICO.Controllers.Error
{
    public class ErrorController : Controller
    {
        // GET: Error
        [HttpGet]
        public ActionResult UnauthorizeOperation(String tipoUsuario)
        {
            ViewBag.tipoUsuario = tipoUsuario;
            return View();
        }

        [HttpGet]
        public ActionResult Unauthorize(String error)
        {
            ViewBag.tipoError = error;
            return View();
        }

        [HttpGet]
        public ActionResult MessageError(String error)
        {
            ViewBag.Message = error;
            return View();
        }
    }
}