﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracticaIIICO.Controllers.Ajustes
{
    public class AjustesController : Controller
    {
        // GET: Ajustes
        public ActionResult Index()
        {
            return View();
        }

        // GET: Ajustes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Ajustes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ajustes/Create
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

        // GET: Ajustes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ajustes/Edit/5
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

        // GET: Ajustes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Ajustes/Delete/5
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