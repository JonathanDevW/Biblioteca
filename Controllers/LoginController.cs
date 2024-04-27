using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;

namespace Biblioteca.Controllers
{
    public class LoginController : Controller
    {
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

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Dictionary<string, string> jData)
        {
            string username = jData["usuario"];
            string password = jData["password"];

            try
            {
                Modelo query = new Modelo();
                var q = query.Login(username, password);

                if (q != null && q.Read())
                {
                    int bloqueado = Convert.ToInt32(q["Bloqueado"]);

                    if (bloqueado == 3)
                    {
                        return Json(new { message = "Error! El usuario está bloqueado." });
                    }
                    else
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

                            return Json(new { RedirectUrl = Url.Action("Index", "Home") });
                    }
                }
                else
                {
                    // Handle invalid credentials
                    return Json(new { message = "Credenciales inválidas." });
                }
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                return Json(new { message = "Error interno. Inténtelo nuevamente." });
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> Login([FromBody] Dictionary<string, string> jData)
        //{
        //    string username = jData["usuario"];
        //    string password = jData["password"];
        //    Modelo query = new Modelo();
        //    var (reader, bloqueado) = query.Login(username, password);

        //    if (reader != null)
        //    {
        //        if (!bloqueado)
        //        {
        //            var claims = new List<Claim>
        //            {
        //                new Claim(ClaimTypes.Name, reader.GetString("nickname")),
        //                new Claim(ClaimTypes.NameIdentifier, reader.GetInt32("id_usuario").ToString()),
        //                new Claim(ClaimTypes.Role, reader.GetInt32("id_rol").ToString()),
        //                new Claim(ClaimTypes.GivenName, reader.GetInt32("id_persona").ToString()),
        //            };
        //            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        //            await HttpContext.SignInAsync(
        //                CookieAuthenticationDefaults.AuthenticationScheme,
        //                new ClaimsPrincipal(claimsIdentity));

        //            return Json(new { RedirectUrl = Url.Action("Index", "Home") });
        //        }
        //        else
        //        {
        //            return Json(new { message = "Error! Usuario bloqueado. Contacte al administrador." });
        //        }
        //    }
        //    else
        //    {
        //        return Json(new { message = "Error! Credenciales inválidas." });
        //    }
        //}
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}

