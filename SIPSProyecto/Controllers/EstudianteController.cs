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
    public class EstudianteController : Controller
    {
        // GET: Estudiante
        public async Task<ActionResult> Index(List<Estudiante> estudiantes)
        {
            using (DBModels contexto = new DBModels())
            {
                if (estudiantes != null)
                {
                    return View(estudiantes);
                }
                else
                {
                    return View(await contexto.Estudiante.ToListAsync());
                }
            }
        }

        // GET: Estudiante/Details/5
        public ActionResult Details(int id)
        {
            using (DBModels contexto = new DBModels())
            {
                var estudiante = contexto.Estudiante.FirstOrDefault(x => x.est_iCodigo == id);
                if (estudiante == null)
                {
                    return HttpNotFound();
                }
                return View(estudiante);
            }
        }

        // GET: Estudiante/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Estudiante/Create
        [HttpPost]
        public ActionResult Create(Estudiante estudiante)
        {
            try
            {
                using (DBModels context = new DBModels())
                {
                    context.Estudiante.Add(estudiante);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Estudiante/Edit/5
        public ActionResult Edit(int id)
        {
            using (DBModels contexto = new DBModels())
            {
                return View(contexto.Estudiante.Where(x => x.est_iCodigo == id).FirstOrDefault());
            }
        }

        // POST: Estudiante/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            Estudiante estudiante = null;
            try
            {
                using (DBModels context = new DBModels())
                {
                    estudiante = context.Estudiante.Where(x => x.est_iCodigo == id).FirstOrDefault();
                    if (estudiante != null)
                    {
                        // Actualizar las propiedades del estudiante con los valores del formulario
                        estudiante.est_vcCodigo = collection["est_vcCodigo"];

                        // Marcar la entidad como modificada
                        context.Entry(estudiante).State = EntityState.Modified;

                        // Guardar los cambios en la base de datos
                        context.SaveChanges();
                    }
                }

                return View("Details", estudiante);
            }
            catch
            {
                return View();
            }
        }

        // GET: Estudiante/Delete/5
        public ActionResult Delete(int id)
        {
            using (DBModels contexto = new DBModels())
            {
                return View(contexto.Estudiante.Where(x => x.est_iCodigo == id).FirstOrDefault());
            }
        }

        // POST: Estudiante/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (DBModels context = new DBModels())
                {
                    Estudiante estudiante = context.Estudiante.Where(x => x.esc_iCodigo == id).FirstOrDefault();
                    context.Estudiante.Remove(estudiante);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
