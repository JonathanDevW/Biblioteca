//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Data;
//using System.Security.Claims;

//namespace Biblioteca.Controllers
//{
//    public class LoginController : Controller
//    {
//        public IActionResult Index()
//        {
//            return View();
//        }

//        public IActionResult Registro()
//        {
//            return View();
//        }

//        public IActionResult Recuperacion()
//        {
//            return View();
//        }

//        public IActionResult NewPassword()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Login([FromBody] Dictionary<string, string> jData)
//        {
//            string username = jData["usuario"];
//            string password = jData["password"];
//            Modelo query = new Modelo();
//            var q = query.Login(username, password);

//            if (q != null)
//            {
//                if (q.Read())
//                {

//                    var claims = new List<Claim>
//                    {
//                        new Claim(ClaimTypes.Name, q.GetString("nickname")),
//                        new Claim(ClaimTypes.NameIdentifier, q.GetInt32("id_usuario").ToString()),
//                        new Claim(ClaimTypes.Role, q.GetInt32("id_rol").ToString()),
//                        new Claim(ClaimTypes.GivenName, q.GetInt32("id_persona").ToString()),
//                    };
//                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

//                    await HttpContext.SignInAsync(
//                        CookieAuthenticationDefaults.AuthenticationScheme,
//                        new ClaimsPrincipal(claimsIdentity));


//                    return Json(new { RedirectUrl = Url.Action("Index", "Home") });

//                }
//                //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), auto

//                else
//                {
//                    return Json(new { message = "Error! Credenciales inválidas" });
//                }
//            }
//            else
//            {
//                return Json(new { message = "Error conexión, inténtelo nuevamente" });
//            }
//        }
//        public async Task<ActionResult> Logout()
//        {
//            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
//            return RedirectToAction("Index", "Login");
//        }
//    }
//}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Biblioteca.Controllers
{
    public class LoginController : Controller
    {
        private const int MAX_INTENTOS_FALLIDOS = 3;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }

        public IActionResult Recuperacion()
        {
            return View();
        }

        public IActionResult NewPassword()
        {
            return View();
        }

        private void SetLoginAttemptsToCookie(int attempts)
        {
            // Establecer el valor de los intentos en la cookie
            Response.Cookies.Append("LoginAttempts", attempts.ToString(), new CookieOptions
            {

            });
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Dictionary<string, string> jData)
        {
            try
            {
                string username = jData["usuario"];
                string password = jData["password"];

                Modelo query = new Modelo();
                var q = query.Login(username, password);

                if (q != null && q.Read())
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, q.GetString("nickname")),
                new Claim(ClaimTypes.NameIdentifier, q.GetInt32("id_usuario").ToString()),
                new Claim(ClaimTypes.Role, q.GetInt32("id_rol").ToString()),
                new Claim(ClaimTypes.GivenName, q.GetInt32("id_persona").ToString()),
            };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));

                    // Limpiar los intentos fallidos cuando se inicia sesión correctamente
                    ClearLoginAttemptsCookie();

                    return Json(new { RedirectUrl = Url.Action("Index", "Home") });
                }
                else
                {
                    int intentosRestantes = GetLoginAttemptsFromCookie();
                    intentosRestantes--;

                    if (intentosRestantes > 0)
                    {
                        // Actualizar el número de intentos restantes en la cookie
                        SetLoginAttemptsToCookie(intentosRestantes);

                        return Json(new { message = "Error! Credenciales inválidas. Le quedan " + intentosRestantes + " intentos." });
                    }
                    else
                    {
                        if (intentosRestantes == 0)
                        {
                            // Bloquear usuario
                            string idUsuario = username;
                            string idEstadoBloqueado = "bloqueado"; // Obtener el ID del estado bloqueado de tu lógica
                            BloquearUsuario(idUsuario, idEstadoBloqueado);

                            // Limpiar los intentos fallidos ya que el usuario está bloqueado
                            ClearLoginAttemptsCookie();

                            return Json(new { message = "Error! Demasiados intentos fallidos. El usuario está bloqueado." });

                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción aquí, por ejemplo, registrarla y devolver un error genérico
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor.");
            }
        }

        private void ClearLoginAttemptsCookie()
        {
            // Eliminar la cookie de intentos de inicio de sesión
            Response.Cookies.Delete("LoginAttempts");
        }



        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

        private int GetLoginAttemptsFromCookie()
        {
            string cookieValue = Request.Cookies["LoginAttempts"];
            if (string.IsNullOrEmpty(cookieValue))
            {
                return MAX_INTENTOS_FALLIDOS;
            }
            else
            {
                return int.Parse(cookieValue);
            }
        }

        public void BloquearUsuario(string idUsuario, string idEstado)
        {
            BaseDatos com = new BaseDatos(); // Instancia a la base de datos
            com.Conectar(); // Se conecta a la instancia
            string sql = "UPDATE usuarios SET id_estado = '" + idEstado + "' WHERE id_usuario = '" + idUsuario + "'";

            com.CrearComando(sql);
            com.EjecutarComando();
            com.Desconectar();
        }

    }
}