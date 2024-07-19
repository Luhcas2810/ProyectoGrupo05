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
    public class IncidenciaController : Controller
    {
        // GET: Incidencia
        public async Task<ActionResult> Index(List<Incidencia> incidencias)
        {
            using (DBModels contexto = new DBModels())
            {
                if (incidencias != null)
                {
                    return View(incidencias);
                }
                return View(await contexto.Incidencia.ToListAsync());
            }
        }

        // GET: Incidencia/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Incidencia/Create
        public ActionResult Create(Incidencia incidencia)
        {
            if (incidencia == null)
            {
                return View();
            }
            using (DBModels context = new DBModels())
            {
                var administradores = context.Administrador.Select(e => new
                {
                    adm_iCodigo = e.adm_iCodigo,
                    adm_vcNombre = e.Usuario.usu_vcNombres + " " + e.Usuario.usu_vcApellidos
                }).ToList();
                ViewBag.Administradores = new SelectList(administradores, "adm_iCodigo", "adm_vcNombre");
            }
            return View(incidencia);
        }

        // POST: Incidencia/Create
        [HttpPost]
        public ActionResult Create(Incidencia incidencia, int id = 1)
        {
            try
            {
                using (DBModels context = new DBModels())
                {
                    context.Incidencia.Add(incidencia);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Listo(int id)
        {
            try
            {
                using (DBModels context = new DBModels())
                {
                    var incidencia = await context.Incidencia.FirstOrDefaultAsync(x => x.inc_iCodigo == id);
                    if (incidencia == null)
                    {
                        return HttpNotFound();
                    }
                    incidencia.inc_txDescripcion = "(RESUELTO) - " + incidencia.inc_txDescripcion;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Incidencia/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Incidencia/Edit/5
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

        // GET: Incidencia/Delete/5
        public ActionResult Delete(int id)
        {
            {
                return View();
            }
        }

        // POST: Incidencia/Delete/5
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
