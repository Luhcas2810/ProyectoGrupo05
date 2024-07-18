using SIPSProyecto.Models;
using SIPSProyecto.Permisos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SIPSProyecto.Controllers
{
    [ValidarSesion]
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public async Task<ActionResult> Index(List<Usuario> usuarios)
        {
            using (DBModels contexto = new DBModels())
            {
                if (usuarios != null)
                {
                    return View(usuarios);
                }
                return View(await contexto.Usuario.ToListAsync());
            }
        }

        // GET: Usuario/Details/5
        public async Task<ActionResult> Details(int id)
        {
            using (DBModels context = new DBModels())
            {
                var usuario = await context.Usuario.FirstOrDefaultAsync(x => x.usu_iCodigo == id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                return View(usuario);
            }
        }

        // GET: Usuario/Create
        public ActionResult Create(Usuario usuario)
        {
            using (DBModels context = new DBModels())
            {
                if (usuario == null)
                {
                    return View();
                }
                var empresas = context.Empresa.Select(e => new
                {
                    emp_iCodigo = e.emp_iCodigo,
                    emp_vcNombre = e.emp_vcNombre
                }).ToList();
                var escuelas = context.Escuela.Select(e => new
                {
                    esc_iCodigo = e.esc_iCodigo,
                    esc_vcNombre = e.esc_vcNombre
                }).ToList();
                ViewBag.Empresas = new SelectList(empresas, "emp_iCodigo", "emp_vcNombre");
                ViewBag.Escuelas = new SelectList(escuelas, "esc_iCodigo", "esc_vcNombre");
                return View(usuario);
            }
        }

        // POST: Usuario/Create
        [HttpPost]
        public ActionResult Create(Usuario usuario, int id = 0)
        {
            try
            {
                string confirmar = usuario.confirmarContrasenia ?? "";
                using (DBModels context = new DBModels())
                {
                    var empresas = context.Empresa.Select(e => new
                    {
                        emp_iCodigo = e.emp_iCodigo,
                        emp_vcNombre = e.emp_vcNombre
                    }).ToList();

                    var escuelas = context.Escuela.Select(e => new
                    {
                        esc_iCodigo = e.esc_iCodigo,
                        esc_vcNombre = e.esc_vcNombre
                    }).ToList();

                    ViewBag.Empresas = new SelectList(empresas, "emp_iCodigo", "emp_vcNombre");
                    ViewBag.Escuelas = new SelectList(escuelas, "esc_iCodigo", "esc_vcNombre");
                    if (usuario.usu_vcContrasena == confirmar)
                    {
                        if (usuario.usu_vcTipo == "Administrador")
                        {
                            Administrador administrador = new Administrador();
                            administrador.usu_iCodigo = usuario.usu_iCodigo;
                            context.Administrador.Add(administrador);
                        }
                        if (usuario.usu_vcTipo == "Coordinador")
                        {
                            CoordinadorPracticas coordinador = new CoordinadorPracticas();
                            coordinador.usu_iCodigo = usuario.usu_iCodigo;
                            context.CoordinadorPracticas.Add(coordinador);
                        }
                        if (usuario.usu_vcTipo == "Asistente de empresa")
                        {
                            AsistenteEmpresa asistente = new AsistenteEmpresa();
                            asistente.usu_iCodigo = usuario.usu_iCodigo;
                            asistente.emp_iCodigo = usuario.Empresa;
                            context.AsistenteEmpresa.Add(asistente);
                        }
                        if (usuario.usu_vcTipo == "Estudiante")
                        {
                            Estudiante estudiante = new Estudiante();
                            estudiante.usu_iCodigo = usuario.usu_iCodigo;
                            estudiante.esc_iCodigo = usuario.Escuela;
                            estudiante.est_vcCodigo = usuario.Codigo;
                            context.Estudiante.Add(estudiante);
                        }
                        usuario.usu_vcContrasena = ConvertirContrasenia(usuario.usu_vcContrasena);
                        context.Usuario.Add(usuario);
                        context.SaveChanges();
                    }
                    else
                    {
                        Usuario usuario2 = new Usuario
                        {
                            usu_vcCorreo = usuario.usu_vcCorreo,
                            usu_vcNombres = usuario.usu_vcNombres,
                            usu_vcApellidos = usuario.usu_vcApellidos
                        };
                        TempData["AlertMessage"] = "Contraseñas no coinciden";
                        return RedirectToAction("Create", usuario2);
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            using (DBModels context = new DBModels())
            {
                var usuario = await context.Usuario.FindAsync(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                return View(usuario);
            }
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, FormCollection collection)
        {
            try
            {
                using (DBModels context = new DBModels())
                {

                    var usuario = await context.Usuario.Where(x => x.usu_iCodigo == id).FirstOrDefaultAsync();
                    if (usuario != null)
                    {
                        usuario.usu_vcNombres = collection["nombres"];
                        usuario.usu_vcApellidos = collection["apellidos"];
                        usuario.usu_vcCorreo = collection["correo"];

                        // Marcar la entidad como modificada
                        context.Entry(usuario).State = EntityState.Modified;

                        // Guardar los cambios en la base de datos
                        context.SaveChanges();
                    }
                    return View("Details", usuario);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            using (DBModels contexto = new DBModels())
            {
                var usuario = await contexto.Usuario.FirstOrDefaultAsync(x => x.usu_iCodigo == id);
                return View(usuario);
            }
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                using (DBModels context = new DBModels())
                {
                    var usuario = await context.Usuario.FirstOrDefaultAsync(x => x.usu_iCodigo == id);
                    context.Usuario.Remove(usuario);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> Search(string nombres_usu, string apellidos_usu, string correo_usu, string tipo_usu)
        {
            if (String.IsNullOrWhiteSpace(nombres_usu) && String.IsNullOrWhiteSpace(apellidos_usu) && String.IsNullOrWhiteSpace(correo_usu) && String.IsNullOrWhiteSpace(tipo_usu))
            {
                return RedirectToAction("Index");
            }
            List<Usuario> ListaUsuario = new List<Usuario>();
            try
            {
                using (DBModels contexto = new DBModels())
                {
                    var query = contexto.Usuario.AsQueryable();
                    if (!string.IsNullOrWhiteSpace(nombres_usu))
                    {
                        query = query.Where(f => f.usu_vcNombres.Contains(nombres_usu));
                    }
                    if (!string.IsNullOrWhiteSpace(apellidos_usu))
                    {
                        query = query.Where(f => f.usu_vcApellidos.Contains(apellidos_usu));
                    }
                    if (!string.IsNullOrWhiteSpace(correo_usu))
                    {
                        query = query.Where(f => f.usu_vcCorreo.Contains(correo_usu));
                    }
                    if (!string.IsNullOrWhiteSpace(tipo_usu))
                    {
                        query = query.Where(f => f.usu_vcTipo.Contains(tipo_usu));
                    }
                    ListaUsuario = await query.ToListAsync();
                    if (ListaUsuario.Count == 0)
                    {
                        TempData["AlertMessage"] = "No existen esos datos";
                        return RedirectToAction("Index");
                    }
                }
                return View("Index", ListaUsuario);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public static string ConvertirContrasenia(string cont)
        {
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(cont));
                foreach (byte b in result)
                {
                    Sb.Append(b.ToString("x2"));
                }
            }
            return Sb.ToString();
        }
    }
}
