using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIPSProyecto.Permisos
{
    public class PermisosUsuarioAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["tipo"] == null)
            {
                filterContext.Result = new RedirectResult("~/Login/Login");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}