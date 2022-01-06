using PracticaIIICO.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaIIICO.BD;

namespace PracticaIIICO.Controllers.SubirArchivos
{
    public class SubirArchivoController : Controller
    {
        MotoRepuestosMakoEntities ModeloBD = new MotoRepuestosMakoEntities();
        // GET: SubirArchivo
        public ActionResult IndexCarga(int idPRODIMG)
        {
           

            ViewSubidaModel VistaNueva = new ViewSubidaModel();

            VistaNueva.ID_Producto = idPRODIMG;
            VistaNueva.Archivo1 = null;
            VistaNueva.CadenaNombre = "";
            VistaNueva.CadenaNombre = "";

            return View(VistaNueva);
        }

        [HttpPost]
        public ActionResult SaveFile(ViewSubidaModel model)
        {
            string RutaSitio = Server.MapPath("~/");   //Direccion De Nuestro Proyecto
            int idProductoAEditar = model.ID_Producto; //ID del Producto al cual se le agregara la imagen
            string nombreArchivo = model.CadenaNombre; //Nombre asignado a la imagen 
            string PathArchivo1 = Path.Combine(RutaSitio + "/Files/"+ nombreArchivo+"PROD.png"); //Ubicacion Donde se almacena la imagenes

            string urlImagen = "/Files/"+nombreArchivo+"PROD.png"; //direccion para utilizar en Vistas

            string descripImg = model.DescripcionImg;  //Descripcion de la imagen

            if (!ModelState.IsValid)
            {
                return View("IndexCarga", model.ID_Producto);
            }

            if (ModelState.IsValid)
            {
                this.ModeloBD.sp_Inserta_Imagenes(idProductoAEditar,urlImagen,descripImg);
            }

            model.Archivo1.SaveAs(PathArchivo1);//Almacena La Imagen en la direccion dada

            @TempData["Message"] = "Se Cargaron Los Archivos Al Sistema"; //Es Parecido al ViewBag Pero Vive mas
            return RedirectToAction("ListaProductos","Productos");
        }

        // GET: SubirArchivo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SubirArchivo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SubirArchivo/Create
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

        // GET: SubirArchivo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SubirArchivo/Edit/5
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

        // GET: SubirArchivo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SubirArchivo/Delete/5
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
