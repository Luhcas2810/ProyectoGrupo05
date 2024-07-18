using SIPSProyecto.Models;
using SIPSProyecto.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIPSProyecto.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            usuario.usu_vcContrasena = UsuarioController.ConvertirContrasenia(usuario.usu_vcContrasena);
            using (DBModels context = new DBModels())
            {
                Usuario usuario2 = context.Usuario.FirstOrDefault(x => x.usu_vcCorreo == usuario.usu_vcCorreo);
                if (usuario2.usu_vcContrasena == usuario.usu_vcContrasena)
                {
                    Session["usuario"] = usuario;
                    Session["tipo"] = usuario.usu_vcTipo;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["Mensaje"] = "Credenciales incorrectas";
                    return View(usuario);
                }
            }
        }
    }
}