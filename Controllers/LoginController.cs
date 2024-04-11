﻿using Microsoft.AspNetCore.Authentication;
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
            Modelo query = new Modelo();
            var q = query.Login(username, password);

            if (q != null)
            {
                if (q.Read())
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, q.GetString("nickname")),
                        new Claim(ClaimTypes.NameIdentifier, q.GetInt32("id_usuario").ToString()),
                        new Claim(ClaimTypes.Role, q.GetInt32("id_rol").ToString()),
                        new Claim(ClaimTypes.GivenName, q.GetString("nombre")),
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));


                    return Json(new { RedirectUrl = Url.Action("Index", "Home") });

                }
                //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), auto

                else
                {
                    return Json(new { message = "Error! Credenciales inválidas" });
                }
            }
            else
            {
                return Json(new { message = "Error conexión, inténtelo nuevamente" });
            }
        }
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}
