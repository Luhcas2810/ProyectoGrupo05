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
    public class ConvenioController : Controller
    {
        // GET: Convenio
        public async Task<ActionResult> Index(List<Convenio> convenio)
        {
            using (DBModels contexto = new DBModels())
            {
                if (convenio != null)
                {
                    return View(convenio);
                }
                else
                {
                    return View(await contexto.Convenio.ToListAsync());
                }
            }
        }

        // GET: Estudiante/Details/5
        public async Task<ActionResult> Details(int id)
        {
            using (DBModels contexto = new DBModels())
            {
                var convenio = await contexto.Convenio.FirstOrDefaultAsync(x => x.con_iCodigo == id);
                if (convenio == null)
                {
                    return HttpNotFound();
                }
                return View(convenio);
            }
        }

        // GET: Convenio/Create
        public ActionResult Create(Convenio convenio)
        {
            if(convenio == null)
            {
                return View();
            }
            using(DBModels contexto = new DBModels())
            {
                var empresas = contexto.Empresa.Select(e => new
                {
                    emp_iCodigo = e.emp_iCodigo,
                    emp_vcNombre = e.emp_vcNombre
                }).ToList();
                ViewBag.Empresas = new SelectList(empresas, "emp_iCodigo", "emp_vcNombre");
                var estuiantes = contexto.Estudiante.Select(e => new
                {
                    est_iCodigo = e.est_iCodigo,
                    nombreestudiante = e.Usuario.usu_vcNombres + e.Usuario.usu_vcApellidos
                }).ToList();
                ViewBag.Empresas = new SelectList(empresas, "emp_iCodigo", "emp_vcNombre");
                ViewBag.Estudiantes = new SelectList(estuiantes, "est_iCodigo", "nombreestudiante");
                return View(convenio);
            }
        }

        // POST: Convenio/Create
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

        // GET: Convenio/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Convenio/Edit/5
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

        // GET: Convenio/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Convenio/Delete/5
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
        public async Task<ActionResult> Search(string nombre_con, string nombre_emp)
        {
            if (String.IsNullOrWhiteSpace(nombre_con) && String.IsNullOrWhiteSpace(nombre_emp))
            {
                return RedirectToAction("Index");
            }
            List<Convenio> ListaConvenio = new List<Convenio>();
            try
            {
                using (DBModels contexto = new DBModels())
                {
                    var query = from c in contexto.Convenio
                                join e in contexto.Empresa on c.emp_iCodigo equals e.emp_iCodigo
                                select new { e, c };
                    if (!string.IsNullOrWhiteSpace(nombre_con))
                    {
                        query = query.Where(f => f.c.con_vcPuesto.Contains(nombre_con));
                    }
                    if (!string.IsNullOrWhiteSpace(nombre_emp))
                    {
                        query = query.Where(f => f.e.emp_vcNombre.Contains(nombre_emp));
                    }
                    ListaConvenio = await query.Select(f => f.c).ToListAsync();
                    if (ListaConvenio.Count == 0)
                    {
                        TempData["AlertMessage"] = "No existen esos datos";
                        return RedirectToAction("Index");
                    }
                }
                return View("Index", ListaConvenio);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
