using SIPSProyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIPSProyecto.Controllers
{
    public class TareaController : Controller
    {
        // GET: Tarea
        public ActionResult Index()
        {
            return View();
        }

        // GET: Tarea/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tarea/Create
        public ActionResult Create(Tarea actividad)
        {
            using (DBModels context = new DBModels())
            {
                if (actividad == null)
                {
                    return View();
                }
                var estudiantes = context.Estudiante.Select(e => new
                {
                    est_iCodigo = e.est_iCodigo,
                    est_vcCodigo = e.est_vcCodigo
                }).ToList();
                ViewBag.Estudiantes = new SelectList(estudiantes, "est_iCodigo", "est_vcCodigo");
                return View(actividad);
            }
        }

        // POST: Tarea/Create
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

        // GET: Tarea/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tarea/Edit/5
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

        // GET: Tarea/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tarea/Delete/5
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
