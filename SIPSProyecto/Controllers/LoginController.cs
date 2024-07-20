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

        public ActionResult Principal()
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
                    Session["usuario"] = usuario2;
                    Session["tipo"] = usuario2.usu_vcTipo;
                    Session["id"] = usuario2.usu_iCodigo;
                    if(usuario2.usu_vcTipo == "Administrador")
                    {
                        return RedirectToAction("Index", "Usuario");
                    }
                    else if(usuario2.usu_vcTipo == "Coordinador")
                    {
                        return RedirectToAction("Index", "Estudiante");
                    }
                    else if(usuario2.usu_vcTipo == "Estudiante")
                    {
                        return RedirectToAction("Details", "Usuario", new { id = usuario2.usu_iCodigo});
                    }
                    else
                    {
                        return RedirectToAction("Create", "Incidencia");
                    }
                    
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