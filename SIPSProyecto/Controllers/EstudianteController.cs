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
        public async Task<ActionResult> Search(string nombres_est, string apellidos_est, string codigo_est)
        {
            if (String.IsNullOrWhiteSpace(nombres_est) && String.IsNullOrWhiteSpace(apellidos_est) && String.IsNullOrWhiteSpace(codigo_est))
            {
                return RedirectToAction("Index");
            }
            List<Estudiante> ListaEstudiante = new List<Estudiante>();
            try
            {
                using (DBModels contexto = new DBModels())
                {
                    var query = from e in contexto.Estudiante
                                join c in contexto.Usuario on e.usu_iCodigo equals c.usu_iCodigo select new { e, c };
                    if (!string.IsNullOrWhiteSpace(nombres_est))
                    {
                        query = query.Where(f => f.c.usu_vcNombres.Contains(nombres_est));
                    }
                    if (!string.IsNullOrWhiteSpace(apellidos_est))
                    {
                        query = query.Where(f => f.c.usu_vcApellidos.Contains(apellidos_est));
                    }
                    if (!string.IsNullOrWhiteSpace(codigo_est))
                    {
                        query = query.Where(f => f.e.est_vcCodigo.Contains(codigo_est));
                    }
                    ListaEstudiante = await query.Select(f => f.e).ToListAsync();
                    if (ListaEstudiante.Count == 0)
                    {
                        TempData["AlertMessage"] = "No existen esos datos";
                        return RedirectToAction("Index");
                    }
                }
                return View("Index", ListaEstudiante);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
