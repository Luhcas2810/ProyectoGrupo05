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
        public ActionResult Create()
        {
            return View();
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
    }
}
