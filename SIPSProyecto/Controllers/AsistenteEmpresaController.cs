using SIPSProyecto.Models;
using SIPSProyecto.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIPSProyecto.Controllers
{
    [ValidarSesion]
    public class AsistenteEmpresaController : Controller
    {
        // GET: AsistenteEmpresa
        public ActionResult Index()
        {
            return View();
        }

        // GET: AsistenteEmpresa/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AsistenteEmpresa/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: AsistenteEmpresa/Create
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

        // GET: AsistenteEmpresa/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AsistenteEmpresa/Edit/5
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

        // GET: AsistenteEmpresa/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AsistenteEmpresa/Delete/5
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
