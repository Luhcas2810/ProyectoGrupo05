using SIPSProyecto.Models;
using SIPSProyecto.Permisos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SIPSProyecto.Controllers
{
    [ValidarSesion]
    public class TareaController : Controller
    {
        // GET: Tarea
        public ActionResult Index()
        {
            return View();
        }

        // GET: Tarea/Details/5
        public async Task<ActionResult> Details(int id)
        {
            using (DBModels context = new DBModels())
            {
                var tarea = await context.Tarea.FirstOrDefaultAsync(x => x.tar_iCodigo == id);
                if (tarea == null)
                {
                    return HttpNotFound();
                }
                return View(tarea);
            }
        }

        // GET: Tarea/Create
        public ActionResult Create(Tarea tarea)
        {
            using (DBModels context = new DBModels())
            {
                var estudiantes = context.Estudiante.Select(e => new
                {
                    est_iCodigo = e.est_iCodigo,
                    est_vcCodigo = e.est_vcCodigo
                }).ToList();
                ViewBag.Estudiantes = new SelectList(estudiantes, "est_iCodigo", "est_vcCodigo");
                if (tarea == null)
                {
                    return View();
                }
               
                return View(tarea);
            }
        }

        // POST: Tarea/Create
        [HttpPost]
        public ActionResult Create(Tarea tarea, FormCollection collection)
        {
            try
            {
                using (DBModels context = new DBModels())
                {
                    tarea.est_iCodigo =Convert.ToInt32( collection["id"]);
                    var estudiantes = context.Estudiante.Select(e => new
                    {
                        est_iCodigo = e.est_iCodigo,
                        est_vcCodigo = e.est_vcCodigo
                    }).ToList();
                    ViewBag.Estudiantes = new SelectList(estudiantes, "est_iCodigo", "est_vcCodigo");
                    tarea.tar_vcNotaObtenida = "NC";
                    context.Tarea.Add(tarea);
                    context.SaveChanges();
                    return RedirectToAction("Details",tarea.tar_iCodigo);
                }
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

        public  async Task<ActionResult> Search(int codigo_est)
        {
            try
            {

                using(DBModels contexto = new DBModels())
                {
                    var estudiantes = contexto.Estudiante.Select(e => new
                    {
                        est_iCodigo = e.est_iCodigo,
                        est_vcCodigo = e.est_vcCodigo
                    }).ToList();
                    ViewBag.Estudiantes = new SelectList(estudiantes, "est_iCodigo", "est_vcCodigo");
                    var query = contexto.Estudiante.AsQueryable();
                    query = query.Where(x => x.est_iCodigo == codigo_est);
                    Estudiante estudiante = await query.FirstOrDefaultAsync();
                    Tarea tarea = new Tarea();
                    tarea.est_iCodigo = estudiante.est_iCodigo;
                    return View("Create",tarea);
                }
            }
            catch
            {
                return View();
            }
            
        }
    }
}
