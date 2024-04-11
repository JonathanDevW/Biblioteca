using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    [Authorize]
    public class MantenimientoController : Controller
    {
        public IActionResult Usuarios()
        {
            Modelo query = new Modelo();
            ViewBag.query = query.BuscarTodo("vista_Usuarios");

            return View();
        }


        public IActionResult Roles()
        {
            Modelo query = new Modelo();
            ViewBag.query = query.BuscarTodo("vista_Roles");

            return View();
        }

        public IActionResult Estados()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AgregarUsuario()
        {
            string nickname = Request.Form["usuario"];
            string password = Request.Form["password"];
            string nombre = Request.Form["nomCom"];
            string correo = Request.Form["correo"];
            string rol = Request.Form["rol"];
            string estado = Request.Form["estado"];

            string[] datos = { "nickname:" + nickname, "password:" + password, "nombre:" + nombre, "correo:" + correo, "id_rol:" + rol, "id_estado:" + estado };
            Modelo com = new Modelo();
            try
            {
                com.Agregar("usuario", datos);
                ViewBag.msg = "El registro ha sido agregado con éxito";
            }
            catch
            {
                ViewBag.msg = "Error al procesar los datos";
            }

            ViewBag.query = com.BuscarTodo("vista_Usuarios");
            return View("Usuarios");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AgregarRol()
        {
            string rol = Request.Form["rol"];
            string descripcion = Request.Form["descripcion"];


            string[] datos = { "rol:" + rol, "descripcion:" + descripcion };
            Modelo com = new Modelo();
            try
            {
                com.Agregar("rol", datos);
                ViewBag.msg = "El registro ha sido agregado con éxito";
            }
            catch
            {
                ViewBag.msg = "Error al procesar los datos";
            }

            ViewBag.query = com.BuscarTodo("vista_Roles");
            return View("Rol");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AgregarEstado()
        {
            string rol = Request.Form["estado"];
            string descripcion = Request.Form["descripcion"];


            string[] datos = { "estado:" + rol, "descripcion:" + descripcion };
            Modelo com = new Modelo();
            try
            {
                com.Agregar("estado", datos);
                ViewBag.msg = "El registro ha sido agregado con éxito";
            }
            catch
            {
                ViewBag.msg = "Error al procesar los datos";
            }

            ViewBag.query = com.BuscarTodo("vista_Estados");
            return View("Estado");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Restablecer()
        {
            Modelo com = new Modelo();

            ViewBag.query = com.BuscarTodo("vista_Usuarios");
            return View("Usuarios");
        }

    }
}
