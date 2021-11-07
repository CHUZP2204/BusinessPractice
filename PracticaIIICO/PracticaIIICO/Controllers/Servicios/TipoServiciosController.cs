using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracticaIIICO.Controllers.TipoServicios
{
    public class TipoServiciosController : Controller
    {
        // GET: TipoServicios
        public ActionResult Index()
        {
            return View();
        }

        // GET: TipoServicios/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TipoServicios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoServicios/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TipoServicios/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TipoServicios/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
