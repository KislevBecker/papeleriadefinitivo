using ProgramaDeRequisas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProgramaDeRequisas.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string message= "")
        {
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string user, string contraseña)
        {
            if(!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(contraseña))
            {
                SITEPLUSEntities2 db = new SITEPLUSEntities2();
                var usuario = db.TBL_UsuariosSolicitantes.FirstOrDefault(e => e.nombreSolicitante == user && e.pass == contraseña);
                //string prefijo = "farinternet\\";
                //var usuario = db.TSP_Usuarios.FirstOrDefault(e => e.Usuario_Nombre == user && e.Usuario_Id == prefijo + contraseña);
                //si el usuario es diferente de null
                if (usuario != null)
                {
                    //se encontró el usuario
                    //FormsAuthentication.SetAuthCookie(usuario.Usuario_Nombre, true);
                    FormsAuthentication.SetAuthCookie(usuario.nombreSolicitante, true);

                    //se almacena el nombre de usuario en una sesion para usar en permisos 
                    Session["Usuario"] = user;


                    RedirectToAction("Dashboard", "Home");
                }
                else
                {
                    return RedirectToAction("Index", new { message = "Datos Incorrectos para este usuario, por favor verifique de nuevo" });
                }
            }
            else 
            {
                return RedirectToAction("Index", new { message = "Todos los campos son obligatorios" });
            }
            return RedirectToAction("Dashboard", "Home");
        }

        [Authorize]
        public ActionResult Dashboard()
        {
                return View();
        }

        public ActionResult AccesoDenegado()
        {
            ViewBag.Message = "No tienes permisos para ver esta pantalla, por favor regresa a la página anterior. ";

            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            //FormsAuthentication.SignOut();
            //Session.Abandon();
            //return RedirectToAction("Index");
            
            Response.Cookies.Clear();
            FormsAuthentication.SignOut();
            HttpCookie c = new HttpCookie("Login");
            c.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(c);
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}