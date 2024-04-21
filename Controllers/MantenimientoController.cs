using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Biblioteca.Library;
using static Modelo;

namespace Biblioteca.Controllers
{
    [Authorize]
    public class MantenimientoController : Controller
    {
        public IActionResult Usuarios()
        {
            Modelo query = new Modelo();
            ViewBag.query = query.BuscarTodo("Vista_Usuarios");
            ViewBag.roles = new HtmlString(MyLib.llenar("rol", "id_rol","rol","rol","id_rol"));
            ViewBag.persona = new HtmlString(MyLib.llenar("persona", "id_persona", "primer_nombre", "id_persona", "primer_nombre"));
            return View();
        }
        public IActionResult Persona()
        {
            Modelo query = new Modelo();
            ViewBag.query = query.BuscarTodo("Vista_Personas");
            return View();
        }
        public IActionResult Carreras()
        {
            Modelo query = new Modelo();
            ViewBag.query = query.BuscarTodo("Vista_Carreras");
            return View();
        }

        public IActionResult Facultades()
        {
            Modelo query = new Modelo();
            ViewBag.query = query.BuscarTodo("Vista_Facultades");
            return View();
        }
        public IActionResult Roles()
        {
            Modelo query = new Modelo();
            ViewBag.query = query.BuscarTodo("Vista_Roles");

            return View();
        }

        public IActionResult Estados()
        {
            Modelo query = new Modelo();
            ViewBag.query = query.BuscarTodo("Vista_Estado");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AgregarUsuario()
        {
            string nickname = Request.Form["usuario"];
            string password = Request.Form["password"];
            string correo = Request.Form["correo"];
            string rol = Request.Form["rol"];
            string estado = Request.Form["estado"];
            string persona = Request.Form["persona"];

            string[] datos = { "nickname:" + nickname, "password:" + password, "correo:" + correo, "id_rol:" + rol, "id_estado:" + estado, "id_persona:" + persona };
            Modelo com = new Modelo();
            try
            {
                com.Agregar("usuario", datos);
                ViewBag.msg = new HtmlString(@"<div class=""alert alert-success alert-dismissible fade show col-8 text-center"">
                        <button type=""button"" class=""close"" data-dismiss=""alert"">&times;</button>
                        <i class=""icon fas fa-check""></i>El registro ha sido ingresado con éxito.
                        </div>");
            }
            catch
            {
                ViewBag.msg = new HtmlString(@"<div class=""alert alert-danger alert-dismissible fade show col-8 text-center"">
                        <button type=""button"" class=""close"" data-dismiss=""alert"">&times;</button>
                        <i class=""icon fas fa-times-circle""></i>Error al procesar los datos.
                        </div>");
            }

            ViewBag.query = com.BuscarTodo("Vista_Usuarios");
            ViewBag.roles = new HtmlString(MyLib.llenar("rol", "id_rol", "rol", "rol", "id_rol"));
            ViewBag.persona = new HtmlString(MyLib.llenar("persona", "id_persona", "primer_nombre", "id_persona", "primer_nombre"));
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

            ViewBag.query = com.BuscarTodo("Vista_Roles");
            return View("Roles");

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

            ViewBag.query = com.BuscarTodo("Vista_Estado");
            return View("Estados");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Restablecer()
        {
            string r_password = Request.Form["password_r"];
            string idUsuario = Request.Form["id_usuario_r"];
            string[] datos = { "password:" + r_password };

            Modelo com = new Modelo();
            try
            {
                com.Actualizar("usuario", datos, r_password, idUsuario);
                ViewBag.msg = "La contraseña ha sido restablecida con éxito";
            }
            catch (Exception ex)
            {
                ViewBag.msg = "Error al restablecer la contraseña: " + ex.Message;
            }

            ViewBag.query = com.BuscarTodo("Vista_Usuarios");
            return View("Usuarios");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ModUsuario()
        {

            string idUsuario = Request.Form["m_id_usuario"];
            string m_usuario = Request.Form["m_usuario"];
            string m_email = Request.Form["m_email"];
            string m_rol = Request.Form["rol"];
            string m_estado = Request.Form["m_estado"];
            string m_id_persona = Request.Form["m_persona"];

            Modelo com = new Modelo();

            try
            {

                string[] datos = { "nickname:" + m_usuario, "correo:" + m_email, "id_rol:" + m_rol, "id_estado:" + m_estado, "id_persona:" + m_id_persona };


                com.Actualizar("usuario", datos, "id_usuario", idUsuario);

                ViewBag.msg = "Los datos han sido actualizados con éxito";
            }
            catch (Exception ex)
            {
                ViewBag.msg = "Error al actualizar datos: " + ex.Message;
            }


            ViewBag.query = com.BuscarTodo("Vista_Usuarios");
            ViewBag.roles = new HtmlString(MyLib.llenar("rol", "id_rol", "rol", "rol", "id_rol"));
            ViewBag.persona = new HtmlString(MyLib.llenar("persona", "id_persona", "primer_nombre", "id_persona", "primer_nombre"));

            return View("Usuarios");


        }

        public IActionResult ObtenerUser(string tabla, string id)
        {

            Modelo query = new Modelo();
            var usuario = query.ObtenerUser(tabla, id);

            if (usuario != null && usuario.Read())
            {
                var user = new
                {
                    nickname = usuario["nickname"].ToString(),
                    correo = usuario["correo"].ToString(),
                    id_rol = usuario["id_rol"].ToString(),
                    id_estado = usuario["id_estado"].ToString(),
                    persona = usuario["id_persona"].ToString()
                };

                return Json(user);

            }
            else
            {
                return Json(null);
            }
        }
    }

}
