using SIPSProyecto.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SIPSProyecto.Controllers
{
    public class CoordinadorPracticasController : Controller
    {
        // GET: CoordinadorPracticas
        public async Task<ActionResult> Index(List<CoordinadorPracticas> coordinador)
        {
            using (DBModels contexto = new DBModels())
            {
                if (coordinador != null)
                {
                    return View(coordinador);
                }
                return View(await contexto.CoordinadorPracticas.ToListAsync());
            }
        }

        // GET: CoordinadorPracticas/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CoordinadorPracticas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CoordinadorPracticas/Create
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

        // GET: CoordinadorPracticas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CoordinadorPracticas/Edit/5
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

        // GET: CoordinadorPracticas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CoordinadorPracticas/Delete/5
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
